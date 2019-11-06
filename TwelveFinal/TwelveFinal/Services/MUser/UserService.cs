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
        Task<User> Register(User user);
        Task<User> Login(UserFilter userFilter);
        Task<User> EditProfile(User user);
        Task<User> ChangePassword(UserFilter userFilter, string newPassword);
        Task<bool> ForgotPassword(UserFilter userFilter);
        Task<List<User>> ImportExcel(byte[] file);
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
                    Password = GeneratePassword(),
                    Salt = salt,
                    Email = user.Email,
                    Gender = user.Gender,
                    Phone = user.Phone,
                    IsAdmin = false
                });

                await UOW.Commit();
                await SendEmail(user);
                return await UOW.UserRepository.Get(new UserFilter
                {
                    Identify = user.Identify,
                });
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<User> EditProfile(User user)
        {
            if (!await UserValidator.Update(user))
                return user;

            try
            {
                await UOW.Begin();
                await UOW.UserRepository.Update(user);
                await UOW.Commit();
                return user;
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
            SendEmail(user);
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

        public async Task<List<User>> ImportExcel(byte[] file)
        {
            List<User> users = await LoadFromExcel(file);
            using (UOW.Begin())
            {
                try
                {
                    foreach (var user in users)
                    {
                        string salt = Convert.ToBase64String(CryptographyExtentions.GenerateSalt());
                        user.Id = Guid.NewGuid();
                        user.Salt = salt;
                        user.Password = GeneratePassword();
                        user.IsAdmin = false;
                    }
                    
                    var result = this.UOW.UserRepository.BulkInsert(users);
                    await UOW.Commit();
                    users.ForEach(u => SendEmail(u));
                    return users;
                }
                catch (Exception ex)
                {
                    await UOW.Rollback();
                    throw ex;
                }
            }
        }

        private async Task<List<User>> LoadFromExcel(byte[] file)
        {
            List<User> excelTemplates = new List<User>();
            using (MemoryStream ms = new MemoryStream(file))
            using (var package = new ExcelPackage(ms))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    User excelTemplate = new User()
                    {
                        FullName = worksheet.Cells[i, 1].Value?.ToString(),
                        Dob = DateTime.Parse(worksheet.Cells[i, 2].Value?.ToString()),
                        Gender = worksheet.Cells[i, 3].Value?.Equals("1") ,
                        Ethnic = worksheet.Cells[i, 4].Value?.ToString(),
                        Identify = worksheet.Cells[i, 5].Value?.ToString(),
                        Phone = worksheet.Cells[i, 6].Value?.ToString(),
                        Email = worksheet.Cells[i, 7].Value?.ToString(),
                        Password = GeneratePassword()
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
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

        private async Task SendEmail(User user)
        {
            if (string.IsNullOrEmpty(user.Email)) return;
            string SendEmail = "hsntladykillah@gmail.com";
            string SendEmailPassword = "demo#123";

            var loginInfo = new NetworkCredential(SendEmail, SendEmailPassword);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            string body = "Tài khoản của bạn đã được đăng ký!\n";
            body += "Id: " + user.Identify + "\n";
            body += "Password: " + user.Password;
            try
            {
                msg.From = new MailAddress(SendEmail);
                msg.To.Add(new MailAddress(user.Email));
                msg.Subject = "Đăng ký tài khoản TwelveFinal!";
                msg.Body = body;
                msg.IsBodyHtml = true;

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error!");
            }
        }

        private string GeneratePassword()
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

        private static Guid CreateGuid(string name)
        {
            MD5 md5 = MD5.Create();
            Byte[] myStringBytes = ASCIIEncoding.Default.GetBytes(name);
            Byte[] hash = md5.ComputeHash(myStringBytes);
            return new Guid(hash);
        }

    }
}
