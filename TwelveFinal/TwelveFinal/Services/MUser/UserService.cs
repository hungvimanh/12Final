using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        
        Task<User> Login(UserFilter userFilter);
        Task<User> ChangePassword(UserFilter userFilter, string newPassword);
        Task<bool> ForgotPassword(UserFilter userFilter);
        
        //Task<byte[]> Export();
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
            bool IsValid = await this.UOW.UserRepository.ChangePassword(user);
            if (!IsValid)
            {
                throw new BadRequestException(ChangePasswordFalse);
            }
            return user;
        }

        public async Task<bool> ForgotPassword(UserFilter userFilter)
        {
            User user = await UOW.UserRepository.Get(userFilter);
            if (user == null) throw new BadRequestException("Id không tồn tại");
            if (!userFilter.Email.Equals(user.Email)) throw new BadRequestException("Email không đúng!");
            user.Password = GeneratePassword();
            await UOW.UserRepository.ChangePassword(user);
            await Utils.RecoveryPasswordMail(user);
            return true;
        }

        private async Task<User> Verify(UserFilter userFilter)
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

        private bool CompareSaltHashedPassword(string source, string password, string salt)
        {
            var hashedPassword = password.HashHMACSHA256(salt);
            if (!hashedPassword.Equals(source))
            {
                return false;
            }
            return true;
        }

        private async Task<User> GenerateJWT(User user, string Secret, int LifeTime)
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
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(nameof(user.StudentId), user.StudentId.ToString()),
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

        

        //public async Task<byte[]> Export()
        //{
        //    UserFilter employeeFilter = new UserFilter()
        //    {
        //        Skip = 0,
        //        Take = int.MaxValue,
        //        OrderBy = EmployeeOrder.Code,
        //        Disabled = false,
        //        Selects = EmployeeSelect.ALL,
        //    };

        //    List<Employee> employees = await UOW.EmployeeRepository.List(employeeFilter);
        //    using (ExcelPackage excel = new ExcelPackage())
        //    {
        //        var employeeHeaders = new List<string[]>()
        //        {
        //            new string[] { "STT", "Mã nhân viên", "Tên nhân viên", "Ngày sinh", "Chức vụ", "Cấp bậc",
        //                "Trạng thái", "CMND", "Mã số thuế", "Lương", "Hệ số lương", "Lương bảo hiểm", "Người phụ thuộc" }
        //        };

        //        List<object[]> data = new List<object[]>();
        //        for (int i = 0; i < employees.Count; i++)
        //        {
        //            data.Add(new object[] {
        //                i + 1,
        //                employees[i].Code,
        //                employees[i].Name,
        //                employees[i].Dob,
        //                employees[i].JobLevel,
        //                employees[i].JobTitleName,
        //                employees[i].StatusName,
        //                employees[i].IdentityNumber,
        //                employees[i].TaxCode,
        //                employees[i].Salary,
        //                employees[i].SalaryRatio,
        //                employees[i].InsuranceSalary,
        //                employees[i].NumberDependentPerson
        //            });
        //        }

        //        excel.GenerateWorksheet(EmployeeSheet, employeeHeaders, data);

        //        return excel.GetAsByteArray();
        //    }
        //}

        

        public static string GeneratePassword()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

    }
}
