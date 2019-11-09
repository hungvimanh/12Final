﻿using Microsoft.EntityFrameworkCore;
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
        Task<List<Student>> List(StudentFilter studentFilter);
        Task<int> Count(StudentFilter studentFilter);
        Task<bool> BulkInsert(List<Student> students);
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

        private IQueryable<StudentDAO> DynamicFilter(IQueryable<StudentDAO> query, StudentFilter studentFilter)
        {
            if (studentFilter == null)
                return query.Where(q => 1 == 0);

            if (studentFilter.Id != null)
                query = query.Where(q => q.Id, studentFilter.Id);
            if (studentFilter.Name != null)
                query = query.Where(q => q.Name, studentFilter.Name);
            if (studentFilter.Identify != null)
                query = query.Where(q => q.Identify, studentFilter.Identify);
            if (studentFilter.Email != null)
                query = query.Where(q => q.Email, studentFilter.Email);
            if (studentFilter.ProvinceId != null)
                query = query.Where(q => q.Town.District.ProvinceId, studentFilter.ProvinceId);
            if (studentFilter.HighSchoolId != null)
                query = query.Where(q => q.HighSchoolId, studentFilter.HighSchoolId);
            if (studentFilter.Dob != null)
                query = query.Where(q => q.Dob, studentFilter.Dob);
            if (studentFilter.Gender.HasValue)
                query = query.Where(q => q.Gender.Equals(studentFilter.Gender.Value));
            if (studentFilter.Status.HasValue)
                query = query.Where(q => q.Status.Equals(studentFilter.Status.Value));
            return query;
        }
        private IQueryable<StudentDAO> DynamicOrder(IQueryable<StudentDAO> query, StudentFilter studentFilter)
        {
            switch (studentFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (studentFilter.OrderBy)
                    {
                        case StudentOrder.Identify:
                            query = query.OrderBy(q => q.Identify);
                            break;
                        case StudentOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (studentFilter.OrderBy)
                    {
                        case StudentOrder.Identify:
                            query = query.OrderByDescending(q => q.Identify);
                            break;
                        case StudentOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        default:
                            query = query.OrderByDescending(q => q.CX);
                            break;
                    }
                    break;
                default:
                    query = query.OrderBy(q => q.CX);
                    break;
            }
            query = query.Skip(studentFilter.Skip).Take(studentFilter.Take);
            return query;
        }
        private async Task<List<Student>> DynamicSelect(IQueryable<StudentDAO> query)
        {

            List<Student> highSchools = await query.Select(q => new Student()
            {
                Id = q.Id,
                Name = q.Name,
                Identify = q.Identify,
                TownId = q.TownId,
                TownCode = q.Town.Code,
                TownName = q.Town.Name,
                DistrictId = q.Town.DistrictId,
                DistrictCode = q.Town.District.Code,
                DistrictName = q.Town.District.Name,
                ProvinceId = q.Town.District.ProvinceId,
                ProvinceCode = q.Town.District.Province.Code,
                ProvinceName = q.Town.District.Province.Name,
                Address = q.Address,
                HighSchoolId = q.HighSchoolId,
                HighSchoolCode = q.HighSchool.Code,
                HighSchoolName = q.HighSchool.Name,
                Dob = q.Dob,
                Email = q.Email,
                Gender = q.Gender,
                Phone = q.Phone,
                EthnicId = q.EthnicId,
                EthnicCode = q.Ethnic.Code,
                EthnicName = q.Ethnic.Name,
                PlaceOfBirth = q.PlaceOfBirth,

                Biology = q.Biology,
                Chemistry = q.Chemistry,
                CivicEducation = q.CivicEducation,
                Geography = q.Geography,
                History = q.History,
                Languages = q.Languages,
                Literature = q.Literature,
                Maths = q.Maths,
                Physics = q.Physics,
                Status = q.Status
            }).ToListAsync();
            return highSchools;
        }

        public async Task<bool> Create(Student student)
        {
            StudentDAO studentDAO = new StudentDAO
            {
                Id = student.Id,
                Address = student.Address,
                Dob = student.Dob,
                Email = student.Email,
                Gender = student.Gender,
                EthnicId = student.EthnicId,
                HighSchoolId = student.HighSchoolId,
                Name = student.Name,
                Phone = student.Phone,
                PlaceOfBirth = student.PlaceOfBirth,
                TownId = student.TownId,
                Identify = student.Identify,

                Biology = student.Biology,
                Chemistry = student.Chemistry,
                CivicEducation = student.CivicEducation,
                Geography = student.Geography,
                History = student.History,
                Languages = student.Languages,
                Literature = student.Literature,
                Maths = student.Maths,
                Physics = student.Physics,
                Status = false,
            };

            tFContext.Student.Add(studentDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BulkInsert(List<Student> students)
        {
            List<StudentDAO> studentDAOs = students.Select(s => new StudentDAO
            {
                Id = s.Id,
                Address = s.Address,
                Dob = s.Dob,
                Email = s.Email,
                Gender = s.Gender,
                EthnicId = s.EthnicId,
                HighSchoolId = s.HighSchoolId,
                Name = s.Name,
                Phone = s.Phone,
                PlaceOfBirth = s.PlaceOfBirth,
                TownId = s.TownId,
                Identify = s.Identify,

                Biology = s.Biology,
                Chemistry = s.Chemistry,
                CivicEducation = s.CivicEducation,
                Geography = s.Geography,
                History = s.History,
                Languages = s.Languages,
                Literature = s.Literature,
                Maths = s.Maths,
                Physics = s.Physics,
                Status = false
            }).ToList();

            tFContext.Student.AddRange(studentDAOs);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.User.Where(u => u.StudentId.Equals(Id)).UpdateFromQueryAsync(s => new UserDAO { StudentId = Guid.Empty});
            await tFContext.Student.Where(s => s.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Student> Get(Guid Id)
        {
            Student student = await tFContext.Student.Where(s => s.Id.Equals(Id)).Select(s => new Student
            {
                Id = s.Id,
                Address = s.Address,
                Dob = s.Dob,
                Email = s.Email,
                Gender = s.Gender,
                EthnicId = s.EthnicId,
                EthnicCode = s.Ethnic.Code,
                EthnicName = s.Ethnic.Name,
                HighSchoolId = s.HighSchoolId,
                HighSchoolCode = s.HighSchool.Code,
                HighSchoolName = s.HighSchool.Name,
                Name = s.Name,
                Phone = s.Phone,
                PlaceOfBirth = s.PlaceOfBirth,
                TownId = s.TownId,
                TownCode = s.Town.Code,
                TownName = s.Town.Name,
                DistrictId = s.Town.DistrictId,
                DistrictCode = s.Town.Code,
                DistrictName = s.Town.Name,
                ProvinceId = s.Town.District.ProvinceId,
                ProvinceCode = s.Town.District.Province.Code,
                ProvinceName = s.Town.District.Province.Name,
                Identify = s.Identify,

                Biology = s.Biology,
                Chemistry = s.Chemistry,
                CivicEducation = s.CivicEducation,
                Geography = s.Geography,
                History = s.History,
                Languages = s.Languages,
                Literature = s.Literature,
                Maths = s.Maths,
                Physics = s.Physics,
                Status = s.Status
            }).FirstOrDefaultAsync();
            return student;
        }

        public async Task<List<Student>> List(StudentFilter studentFilter)
        {
            if (studentFilter == null) return new List<Student>();
            IQueryable<StudentDAO> studentDAOs = tFContext.Student;
            studentDAOs = DynamicFilter(studentDAOs, studentFilter);
            studentDAOs = DynamicOrder(studentDAOs, studentFilter);
            var students = await DynamicSelect(studentDAOs);
            return students;
        }

        public async Task<int> Count(StudentFilter studentFilter)
        {
            IQueryable<StudentDAO> studentDAOs = tFContext.Student;
            studentDAOs = DynamicFilter(studentDAOs, studentFilter);
            return await studentDAOs.CountAsync();
        }
        public async Task<bool> Update(Student student)
        {
            await tFContext.Student.Where(s => s.Id.Equals(student.Id)).UpdateFromQueryAsync(s => new StudentDAO
            {
                Address = s.Address,
                Dob = s.Dob,
                Email = s.Email,
                Gender = s.Gender,
                EthnicId = s.EthnicId,
                HighSchoolId = s.HighSchoolId,
                Name = s.Name,
                Phone = s.Phone,
                PlaceOfBirth = s.PlaceOfBirth,
                TownId = s.TownId,
                Identify = s.Identify,

                Biology = s.Biology,
                Chemistry = s.Chemistry,
                CivicEducation = s.CivicEducation,
                Geography = s.Geography,
                History = s.History,
                Languages = s.Languages,
                Literature = s.Literature,
                Maths = s.Maths,
                Physics = s.Physics
            });
            return true;
        }
    }
}
