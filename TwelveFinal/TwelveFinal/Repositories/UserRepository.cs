using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Create(User user);
        Task<User> Get(Guid Id);
        Task<bool> Update(User user);
        Task<bool> Delete(Guid Id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly TFContext tFContext;
        public UserRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(User user)
        {
            UserDAO userDAO = new UserDAO
            {
                Id = user.Id,
                IsAdmin = false,
                Username = user.Username,
                Password = user.Password,
            };

            tFContext.User.Add(userDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.User.Where(g => g.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<User> Get(Guid Id)
        {
            User user = await tFContext.User.Where(u => u.Id.Equals(Id)).Select(u => new User
            {
                Id = u.Id,
                IsAdmin = u.IsAdmin,
                Username = u.Username,
                Password = u.Password,
                Forms = u.Forms.Select(f => new Form
                {
                    Id = f.Id,
                    NumberForm = f.NumberForm,
                    DepartmentCode = f.DepartmentCode,
                    Date = f.Date,
                    PersonalInformation = new PersonalInformation
                    {
                        Id = f.Id,
                        FullName = f.FullName,
                        Gender = f.Gender,
                        Dob = f.Dob,
                        PlaceOfBirth = f.PlaceOfBirth,
                        Ethnic = f.Ethnic,
                        Identify = f.Identify,
                        Address = f.Address,
                        TownId = f.TownId,
                        TownCode = f.Town.Code,
                        TownName = f.Town.Name,
                        DistrictCode = f.Town.District.Code,
                        DistrictName = f.Town.District.Name,
                        ProvinceCode = f.Town.District.Province.Code,
                        ProvinceName = f.Town.District.Province.Name,
                        IsPermanentResidenceMore18 = f.IsPermanentResidenceMore18,
                        IsPermanentResidenceSpecialMore18 = f.IsPermanentResidenceSpecialMore18,
                        HighSchoolGrade10Id = f.HighSchoolGrade10Id,
                        HighSchoolGrade10Code = f.HighSchoolGrade10.Code,
                        HighSchoolGrade10Name = f.HighSchoolGrade10.Name,
                        HighSchoolGrade10DistrictCode = f.HighSchoolGrade10.District.Code,
                        HighSchoolGrade10DistrictName = f.HighSchoolGrade10.District.Name,
                        HighSchoolGrade10ProvinceCode = f.HighSchoolGrade10.District.Province.Code,
                        HighSchoolGrade10ProvinceName = f.HighSchoolGrade10.District.Province.Name,
                        HighSchoolGrade11Id = f.HighSchoolGrade11Id,
                        HighSchoolGrade11Code = f.HighSchoolGrade11.Code,
                        HighSchoolGrade11Name = f.HighSchoolGrade11.Name,
                        HighSchoolGrade11DistrictCode = f.HighSchoolGrade11.District.Code,
                        HighSchoolGrade11DistrictName = f.HighSchoolGrade11.District.Name,
                        HighSchoolGrade11ProvinceCode = f.HighSchoolGrade11.District.Province.Code,
                        HighSchoolGrade11ProvinceName = f.HighSchoolGrade11.District.Province.Name,
                        HighSchoolGrade12Id = f.HighSchoolGrade12Id,
                        HighSchoolGrade12Code = f.HighSchoolGrade12.Code,
                        HighSchoolGrade12Name = f.HighSchoolGrade12.Name,
                        HighSchoolGrade12DistrictCode = f.HighSchoolGrade12.District.Code,
                        HighSchoolGrade12DistrictName = f.HighSchoolGrade12.District.Name,
                        HighSchoolGrade12ProvinceCode = f.HighSchoolGrade12.District.Province.Code,
                        HighSchoolGrade12ProvinceName = f.HighSchoolGrade12.District.Province.Name,
                        Grade12Name = f.Grade12Name,
                        Phone = f.Phone,
                        Email = f.Email,
                    },
                    RegisterInformation = new RegisterInformation
                    {
                        Id = f.Id,
                        ResultForUniversity = f.ResultForUniversity,
                        StudyAtHighSchool = f.StudyAtHighSchool,
                        Graduated = f.Graduated,
                        ClusterContestId = f.ClusterContestId,
                        ClusterContestCode = f.ClusterContest.Code,
                        ClusterContestName = f.ClusterContest.Name,
                        RegisterPlaceOfExamId = f.RegisterPlaceOfExamId,
                        RegisterPlaceOfExamCode = f.RegisterPlaceOfExam.Code,
                        RegisterPlaceOfExamName = f.RegisterPlaceOfExam.Name,
                        Biology = f.Biology,
                        Chemistry = f.Chemistry,
                        CivicEducation = f.CivicEducation,
                        Geography = f.Geography,
                        History = f.History,
                        Languages = f.Languages,
                        Literature = f.Literature,
                        Maths = f.Maths,
                        NaturalSciences = f.NaturalSciences,
                        Physics = f.Physics,
                        SocialSciences = f.SocialSciences
                    },
                    GraduationInformation = new GraduationInformation
                    {
                        Id = f.Id,
                        ExceptLanguages = f.ExceptLanguages,
                        Mark = f.Mark,
                        ReserveBiology = f.ReserveBiology,
                        ReserveChemistry = f.ReserveChemistry,
                        ReserveCivicEducation = f.ReserveCivicEducation,
                        ReserveGeography = f.ReserveGeography,
                        ReserveHistory = f.ReserveHistory,
                        ReserveLanguages = f.ReserveLanguages,
                        ReserveLiterature = f.ReserveLiterature,
                        ReserveMaths = f.ReserveMaths,
                        ReservePhysics = f.ReservePhysics
                    },
                    UniversityAdmission = new UniversityAdmission
                    {
                        Id = f.Id,
                        Area = f.Area,
                        PriorityType = f.PriorityType,
                        GraduateYear = f.GraduateYear,
                        Connected = f.Connected,
                        University_Majorses = f.University_Majors.Select(m => new University_Majors
                        {
                            Id = m.Id,
                            Benchmark = m.Benchmark,
                            Year = m.Year,
                            MajorsId = m.MajorsId,
                            MajorsCode = m.Majors.Code,
                            MajorsName = m.Majors.Name,
                            UniversityId = m.UniversityId,
                            UniversityCode = m.University.Code,
                            UniversityName = m.University.Name,
                            SubjectGroupType = m.SubjectGroupType
                        }).ToList()
                    }
                }).ToList()
            }).FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> Update(User user)
        {
            await tFContext.User.Where(u => u.Id.Equals(user.Id)).UpdateFromQueryAsync(u => new UserDAO
            {
                Password = u.Password,
            });

            return true;
        }
    }
}
