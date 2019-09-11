using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IFormRepository
    {
        //Task<List<Form>> List(Guid StudentId);
        Task<bool> Create(Form form);
        Task<Form> Get(Guid Id);
        Task<bool> Update(Form form);
        Task<bool> Delete(Guid Id);
    }
    public class FormRepository : IFormRepository
    {
        private readonly TFContext tFContext;
        public FormRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        public async Task<bool> Create(Form form)
        {
            FormDAO formDAO = new FormDAO
            {
                Id = form.Id,
                NumberForm = form.NumberForm,
                DepartmentCode = form.DepartmentCode,
                Date = form.Date,
                FullName = form.PersonalInformation.FullName,
                Dob = form.PersonalInformation.Dob,
                Gender = form.PersonalInformation.Gender,
                PlaceOfBirth = form.PersonalInformation.PlaceOfBirth,
                Ethnic = form.PersonalInformation.Ethnic,
                Identify = form.PersonalInformation.Identify,
                TownId = form.PersonalInformation.TownId,
                IsPermanentResidenceMore18 = form.PersonalInformation.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = form.PersonalInformation.IsPermanentResidenceSpecialMore18,
                HighSchoolGrade10Id = form.PersonalInformation.HighSchoolGrade10Id,
                HighSchoolGrade11Id = form.PersonalInformation.HighSchoolGrade11Id,
                HighSchoolGrade12Id = form.PersonalInformation.HighSchoolGrade12Id,
                Grade12Name = form.PersonalInformation.Grade12Name,
                Phone = form.PersonalInformation.Phone,
                Email = form.PersonalInformation.Email,
                Address = form.PersonalInformation.Address,
                
                ResultForUniversity = form.RegisterInformation.ResultForUniversity,
                StudyAtHighSchool = form.RegisterInformation.StudyAtHighSchool,
                Graduated = form.RegisterInformation.Graduated,
                ClusterContestId = form.RegisterInformation.ClusterContestId,
                RegisterPlaceOfExamId = form.RegisterInformation.RegisterPlaceOfExamId,
                Maths = form.RegisterInformation.Maths,
                Literature = form.RegisterInformation.Literature,
                Languages = form.RegisterInformation.Languages,
                NaturalSciences = form.RegisterInformation.NaturalSciences,
                SocialSciences = form.RegisterInformation.SocialSciences,
                Physics = form.RegisterInformation.Physics,
                Chemistry = form.RegisterInformation.Chemistry,
                Biology = form.RegisterInformation.Biology,
                History = form.RegisterInformation.History,
                Geography = form.RegisterInformation.Geography,
                CivicEducation = form.RegisterInformation.CivicEducation,

                ExceptLanguages = form.GraduationInformation.ExceptLanguages,
                Mark = form.GraduationInformation.Mark,
                ReserveMaths = form.GraduationInformation.ReserveMaths,
                ReserveLiterature = form.GraduationInformation.ReserveLiterature,
                ReserveLanguages = form.GraduationInformation.ReserveLanguages,
                ReservePhysics = form.GraduationInformation.ReservePhysics,
                ReserveChemistry = form.GraduationInformation.ReserveChemistry,
                ReserveBiology = form.GraduationInformation.ReserveBiology,
                ReserveHistory = form.GraduationInformation.ReserveHistory,
                ReserveGeography = form.GraduationInformation.ReserveGeography,
                ReserveCivicEducation = form.GraduationInformation.ReserveCivicEducation,

                PriorityType = form.UniversityAdmission.PriorityType,
                Area = form.UniversityAdmission.Area,
                GraduateYear = form.UniversityAdmission.GraduateYear,
                Connected = form.UniversityAdmission.Connected,
                University_Majors = form.UniversityAdmission.University_Majorses.Select(m => new University_MajorsDAO
                {
                    Id = m.Id,
                    FormId = m.FormId,
                    Benchmark = m.Benchmark,
                    MajorsId = m.MajorsId,
                    UniversityId = m.UniversityId,
                    SubjectGroupType = m.SubjectGroupType,
                    Year = m.Year
                }).ToList(),
                UserId = form.UserId
            };

            tFContext.Form.Add(formDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Form.Where(b => b.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Form> Get(Guid Id)
        {
            Form form = await tFContext.Form.Where(f => f.Id.Equals(Id)).Select(f => new Form
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
            }).FirstOrDefaultAsync();

            return form;
        }

        public async Task<bool> Update(Form form)
        {
            await tFContext.Form.Where(f => f.Id.Equals(form.Id)).UpdateFromQueryAsync(f => new FormDAO
            {
                NumberForm = form.NumberForm,
                DepartmentCode = form.DepartmentCode,
                Date = form.Date,
            });

            return true;
        }
    }
}
