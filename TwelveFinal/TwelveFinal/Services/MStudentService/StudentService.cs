using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TwelveFinal.Common;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MStudentService
{
    public interface IStudentService : IServiceScoped
    {
        Task<Student> Register(Student student);
        Task<Student> Update(Student student);
        Task<bool> ImportExcel(byte[] file);
        Task<Student> Get(Guid Id);
        Task<List<Student>> List(StudentFilter studentFilter);
    }
    public class StudentService : IStudentService
    {
        private readonly IUOW UOW;
        private IStudentValidator StudentValidator;
        public StudentService(IUOW UOW, IStudentValidator StudentValidator)
        {
            this.UOW = UOW;
            this.StudentValidator = StudentValidator;
        }
        public async Task<Student> Register(Student student)
        {
            if (!await StudentValidator.Create(student))
                return student;

            try
            {
                await UOW.Begin();
                student.Id = Guid.NewGuid();
                await UOW.StudentRepository.Create(student);

                //Generate Salt Random
                string salt = Convert.ToBase64String(CryptographyExtentions.GenerateSalt());
                User user = new User()
                {
                    Username = student.Identify,
                    Id = Guid.NewGuid(),
                    Password = CryptographyExtentions.GeneratePassword(),
                    Salt = salt,
                    IsAdmin = false,
                    StudentId = student.Id
                };
                await UOW.UserRepository.Create(user);

                await UOW.Commit();
                await Utils.RegisterMail(user);
                return await UOW.StudentRepository.Get(student.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Student> Update(Student student)
        {
            if (!await StudentValidator.Update(student))
                return student;

            try
            {
                await UOW.Begin();
                await UOW.StudentRepository.Update(student);
                await UOW.Commit();
                return await UOW.StudentRepository.Get(student.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<bool> ImportExcel(byte[] file)
        {
            List<Student> students = await LoadFromExcel(file);
            try
            {
                await UOW.Begin();
                List<User> users = new List<User>();
                students.ForEach(s => s.Id = Guid.NewGuid());
                foreach (var student in students)
                {
                    string salt = Convert.ToBase64String(CryptographyExtentions.GenerateSalt());
                    User user = new User()
                    {
                        Username = student.Identify,
                        Id = Guid.NewGuid(),
                        Password = CryptographyExtentions.GeneratePassword(),
                        Salt = salt,
                        IsAdmin = false,
                        StudentId = student.Id
                    };
                    users.Add(user);
                }

                await UOW.StudentRepository.BulkInsert(students);
                await UOW.UserRepository.BulkInsert(users);
                await UOW.Commit();
                users.ForEach(u => Utils.RegisterMail(u));
                return true;
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw ex;
            }
        }

        public async Task<Student> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            return await UOW.StudentRepository.Get(Id);
        }

        public async Task<List<Student>> List(StudentFilter studentFilter)
        {
            return await UOW.StudentRepository.List(studentFilter);
        }

        public async Task<double> Graduation(Student student)
        {
            double total = 0;
            var NaturalSciences = (student.Physics + student.Chemistry + student.Biology) / 3;
            var SocialSciences = (student.History + student.Geography + student.CivicEducation) / 3;
            if(NaturalSciences.HasValue && SocialSciences.HasValue)
            {
                total = NaturalSciences.Value > SocialSciences.Value ? NaturalSciences.Value : SocialSciences.Value;
            }

            if(NaturalSciences.HasValue && !SocialSciences.HasValue)
            {
                total = NaturalSciences.Value;
            }

            if (!NaturalSciences.HasValue && SocialSciences.HasValue)
            {
                total = SocialSciences.Value;
            }

            total = (total + student.Maths.Value + student.Literature.Value + student.Languages.Value) / 4;
            if (!student.EthnicCode.Equals("01")) total += 1;
            //switch (student.ar)
            //{
            //    default:
            //        break;
            //}
            return total;
        }

        private async Task<List<Student>> LoadFromExcel(byte[] file)
        {
            List<Student> excelTemplates = new List<Student>();
            using (MemoryStream ms = new MemoryStream(file))
            using (var package = new ExcelPackage(ms))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    Student excelTemplate = new Student()
                    {
                        Name = worksheet.Cells[i, 1].Value?.ToString(),
                        Dob = DateTime.Parse(worksheet.Cells[i, 2].Value?.ToString()),
                        Gender = worksheet.Cells[i, 3].Value.Equals("1"),
                        Identify = worksheet.Cells[i, 4].Value?.ToString(),
                        Phone = worksheet.Cells[i, 5].Value?.ToString(),
                        Email = worksheet.Cells[i, 6].Value?.ToString(),
                        
                    };
                    excelTemplates.Add(excelTemplate);
                }
            }
            return excelTemplates;
        }

        

        
    }
}
