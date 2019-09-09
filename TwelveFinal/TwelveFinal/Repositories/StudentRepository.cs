using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IStudentRepository
    {
        Task<bool> Create(Student student);
        Task<Student> Get(Guid Id);
        Task<bool> Update(Student student);
        Task<bool> Delete(Guid Id);
    }
    public class StudentRepository : IStudentRepository
    {
        private readonly TFContext tFContext;
        public StudentRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(Student student)
        {
            StudentDAO studentDAO = new StudentDAO
            {
                Id = student.Id,
                FullName = student.FullName,
                Address = student.Address,
                Dob = student.Dob,
                Email = student.Email,
                Gender = student.Gender,
                HighSchoolId = student.HighSchoolId,
                Identify = student.Identify,
                IsPermanentResidenceMore18 = student.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = student.IsPermanentResidenceSpecialMore18,
                Nation = student.Nation,
                Phone = student.Phone,
                TownId = student.TownId
            };

            tFContext.Student.Add(studentDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Form.Where(f => f.PersonalInfomartionId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.Student.Where(b => b.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Student> Get(Guid Id)
        {
            Student student = await tFContext.Student.Where(s => s.Id.Equals(Id)).Select(s => new Student
            {
                Id = s.Id,
                FullName = s.FullName,
                Address = s.Address,
                Dob = s.Dob,
                Email = s.Email,
                Gender = s.Gender,
                HighSchoolId = s.HighSchoolId,
                Identify = s.Identify,
                IsPermanentResidenceMore18 = s.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = s.IsPermanentResidenceSpecialMore18,
                Nation = s.Nation,
                Phone = s.Phone,
                TownId = s.TownId,
            }).FirstOrDefaultAsync();

            return student;
        }

        public async Task<bool> Update(Student student)
        {
            await tFContext.Student.Where(s => s.Id.Equals(student.Id)).UpdateFromQueryAsync(s => new StudentDAO
            {
                Id = student.Id,
                FullName = student.FullName,
                Address = student.Address,
                Dob = student.Dob,
                Email = student.Email,
                Gender = student.Gender,
                HighSchoolId = student.HighSchoolId,
                Identify = student.Identify,
                IsPermanentResidenceMore18 = student.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = student.IsPermanentResidenceSpecialMore18,
                Nation = student.Nation,
                Phone = student.Phone,
                TownId = student.TownId,
            });

            return true;
        }
    }
}
