using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TwelveFinal.Common;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUser
{
    public interface IUserService : IServiceScoped
    {
        Task<User> Register(User user);
        Task<User> Login(UserFilter userFilter);
        Task<User> ChangePassword(UserFilter userFilter, string newPassword);
    }
    public class UserService : IUserService
    {
        private string Unauthorized = "Tài khoản hoặc mật khẩu không chính xác. Vui lòng thử lại";
        private string ChangePasswordFalse = "Đổi mật khẩu không thành công!";
        private AppSettings appSettings;
        private IDateTimeService DateTimeService;
        private readonly IUOW UOW;
        private IUserValidator UserValidator;
        public UserService(IUOW _UOW, IDateTimeService dateTimeService, IOptions<AppSettings> options, IUserValidator userValidator)
        {
            this.UOW = _UOW;
            this.DateTimeService = dateTimeService;
            this.appSettings = options.Value;
            UserValidator = userValidator;
        }

        public async Task<User> Register(User user)
        {
            if (!await UserValidator.Create(user))
                return user;

            try
            {
                await UOW.Begin();
                //Generate Salt Random
                string salt = Convert.ToBase64String(CryptographyExtentions.GenerateSalt());
                await UOW.UserRepository.Create(new User()
                {
                    FullName = user.FullName,
                    Id = CreateGuid(user.FullName),
                    Password = CryptographyExtentions.HashHMACSHA256(user.Password, salt),
                    Salt = salt,
                    Email = user.Email,
                    Gender = user.Gender,
                    Phone = user.Phone,
                    IsAdmin = false
                });

                await UOW.Commit();
                return await UOW.UserRepository.Get(new UserFilter
                {
                    FullName = user.FullName,
                });
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<User> Login(UserFilter userFilter)
        {
            User user = await this.Verify(userFilter);
            user = await this.GenerateJWT(user, appSettings.JWTSecret, appSettings.JWTLifeTime);
            return user;
        }
        public async Task<User> ChangePassword(UserFilter userFilter, string newPassword)
        {
            
            User user = await this.Verify(userFilter);
            user.Password = newPassword.HashHMACSHA256(user.Salt);

            if (!await UserValidator.Update(user))
                return user;
            bool IsValid = await this.UOW.UserRepository.Update(user);
            if (!IsValid)
            {
                throw new BadRequestException(ChangePasswordFalse);
            }
            return user;
        }

        public async Task<User> Verify(UserFilter userFilter)
        {
            User user = await UOW.UserRepository.Get(userFilter);
            if (user == null) throw new BadRequestException(Unauthorized);

            //compare password + salt
            if (!CompareSaltHashedPassword(user.Password, userFilter.Password, user.Salt))
            {
                throw new BadRequestException(Unauthorized);
            }
            return user;
        }

        public bool CompareSaltHashedPassword(string source, string password, string salt)
        {
            var hashedPassword = password.HashHMACSHA256(salt);
            if (!hashedPassword.Equals(source))
            {
                return false;
            }
            return true;
        }

        public async Task<User> GenerateJWT(User user, string Secret, int LifeTime)
        {
            if (string.IsNullOrEmpty(Secret) || LifeTime <= 0)
                throw new BadRequestException("Fail to generate token, please try again.");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim("IsAdmin", user.IsAdmin.ToString())
                }),
                Expires = this.DateTimeService.UtcNow.AddSeconds(LifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            user.Jwt = token;
            user.ExpiredTime = tokenDescriptor.Expires;
            return user;
        }

        private static Guid CreateGuid(string name)
        {
            MD5 md5 = MD5.Create();
            Byte[] myStringBytes = ASCIIEncoding.Default.GetBytes(name);
            Byte[] hash = md5.ComputeHash(myStringBytes);
            return new Guid(hash);
        }

    }
}
