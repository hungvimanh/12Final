using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MForm;
using TwelveFinal.Services.MGraduation;
using TwelveFinal.Services.MPersonal;
using TwelveFinal.Services.MRegister;
using TwelveFinal.Services.MUniversityAdmission;

namespace TwelveFinal.Controller.form
{
    public class FormRoute
    {
        private const string Default = "api/TF";
        public const string Create = Default + "/create";
        public const string Get = Default + "/get";
        public const string UpdatePersonal = Default + "/update-personal";
        public const string UpdateRegister = Default + "/update-register";
        public const string UpdateGraduate = Default + "/update-graduate";
        public const string UpdateUniversity = Default + "/update-university";
        public const string Delete = Default + "/delete";
    }
    [ApiController]
    public class FormController
    {
        private IFormService FormService;
        private IPersonalInformationService PeronalInformationService;
        private IRegisterInformationService RegisterInformationService;
        private IGraduationInformationService GraduationInformationService;
        private IUniversityAdmissionService UniversityAdmissionService;
        public FormController(
            IFormService formService,
            IPersonalInformationService peronalInformationService,
            IRegisterInformationService registerInformationService,
            IGraduationInformationService graduationInformationService,
            IUniversityAdmissionService universityAdmissionService
            )
        {
            FormService = formService;
            PeronalInformationService = peronalInformationService;
            RegisterInformationService = registerInformationService;
            GraduationInformationService = graduationInformationService;
            UniversityAdmissionService = universityAdmissionService;
        }

        [Route(FormRoute.Create), HttpPost]
        public async Task<FormDTO> Create([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = await ConvertDTOtoBO(formDTO);

            form = await FormService.Create(form);

            formDTO = new FormDTO
            {
                Id = form.Id,
                NumberForm = form.NumberForm,
                DepartmentCode = form.DepartmentCode,
                Date = form.Date,
                UserId = form.UserId,
                
                FullName = form.PersonalInformation.FullName,
                Dob = form.PersonalInformation.Dob,
                Gender = form.PersonalInformation.Gender,
                Identify = form.PersonalInformation.Identify,
                PlaceOfBirth = form.PersonalInformation.PlaceOfBirth,
                TownId = form.PersonalInformation.TownId,
                TownCode = form.PersonalInformation.TownCode,
                TownName = form.PersonalInformation.TownName,
                DistrictCode = form.PersonalInformation.DistrictCode,
                DistrictName  = form.PersonalInformation.DistrictName,
                ProvinceCode = form.PersonalInformation.ProvinceCode,
                ProvinceName = form.PersonalInformation.ProvinceName,
                Address = form.PersonalInformation.Address,
                Phone = form.PersonalInformation.Phone,
                Email = form.PersonalInformation.Email,
                Ethnic = form.PersonalInformation.Ethnic,
                Grade12Name = form.PersonalInformation.Grade12Name,
                HighSchoolGrade10Id = form.PersonalInformation.HighSchoolGrade10Id,
                HighSchoolGrade10Code = form.PersonalInformation.HighSchoolGrade10Code,
                HighSchoolGrade10Name = form.PersonalInformation.HighSchoolGrade10Name,
                HighSchoolGrade10DistrictCode =form.PersonalInformation.HighSchoolGrade10DistrictCode,
                HighSchoolGrade10DistrictName = form.PersonalInformation.HighSchoolGrade10DistrictName,
                HighSchoolGrade10ProvinceCode = form.PersonalInformation.HighSchoolGrade10ProvinceCode,
                HighSchoolGrade10ProvinceName = form.PersonalInformation.HighSchoolGrade10ProvinceName,
                HighSchoolGrade11Id = form.PersonalInformation.HighSchoolGrade11Id,
                HighSchoolGrade11Code = form.PersonalInformation.HighSchoolGrade11Code,
                HighSchoolGrade11Name = form.PersonalInformation.HighSchoolGrade11Name,
                HighSchoolGrade11DistrictCode = form.PersonalInformation.HighSchoolGrade11DistrictCode,
                HighSchoolGrade11DistrictName = form.PersonalInformation.HighSchoolGrade11DistrictName,
                HighSchoolGrade11ProvinceCode = form.PersonalInformation.HighSchoolGrade11ProvinceCode,
                HighSchoolGrade11ProvinceName = form.PersonalInformation.HighSchoolGrade11ProvinceName,
                HighSchoolGrade12Id = form.PersonalInformation.HighSchoolGrade12Id,
                HighSchoolGrade12Code = form.PersonalInformation.HighSchoolGrade12Code,
                HighSchoolGrade12Name = form.PersonalInformation.HighSchoolGrade12Name,
                HighSchoolGrade12DistrictCode = form.PersonalInformation.HighSchoolGrade12DistrictCode,
                HighSchoolGrade12DistrictName = form.PersonalInformation.HighSchoolGrade12DistrictName,
                HighSchoolGrade12ProvinceCode = form.PersonalInformation.HighSchoolGrade12ProvinceCode,
                HighSchoolGrade12ProvinceName = form.PersonalInformation.HighSchoolGrade12ProvinceName,
                IsPermanentResidenceMore18 = form.PersonalInformation.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 =form.PersonalInformation.IsPermanentResidenceSpecialMore18,
                
                ResultForUniversity = form.RegisterInformation.ResultForUniversity,
                StudyAtHighSchool = form.RegisterInformation.StudyAtHighSchool,
                ClusterContestId = form.RegisterInformation.ClusterContestId,
                ClusterContestCode = form.RegisterInformation.ClusterContestCode,
                ClusterContestName = form.RegisterInformation.ClusterContestName,
                RegisterPlaceOfExamId = form.RegisterInformation.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = form.RegisterInformation.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = form.RegisterInformation.RegisterPlaceOfExamName,
                Biology = form.RegisterInformation.Biology,
                Chemistry = form.RegisterInformation.Chemistry,
                CivicEducation = form.RegisterInformation.CivicEducation,
                Geography =form.RegisterInformation.Geography,
                History = form.RegisterInformation.History,
                Languages = form.RegisterInformation.Languages,
                Literature = form.RegisterInformation.Literature,
                Maths = form.RegisterInformation.Maths,
                NaturalSciences = form.RegisterInformation.NaturalSciences,
                Physics = form.RegisterInformation.Physics,
                SocialSciences = form.RegisterInformation.SocialSciences,
                Graduated = form.RegisterInformation.Graduated,
                
                ExceptLanguages = form.GraduationInformation.ExceptLanguages,
                Mark = form.GraduationInformation.Mark,
                ReserveBiology = form.GraduationInformation.ReserveBiology,
                ReserveChemistry = form.GraduationInformation.ReserveChemistry,
                ReserveCivicEducation = form.GraduationInformation.ReserveCivicEducation,
                ReserveGeography = form.GraduationInformation.ReserveGeography,
                ReserveHistory = form.GraduationInformation.ReserveHistory,
                ReserveLanguages = form.GraduationInformation.ReserveLanguages,
                ReserveLiterature = form.GraduationInformation.ReserveLiterature,
                ReserveMaths = form.GraduationInformation.ReserveMaths,
                ReservePhysics = form.GraduationInformation.ReservePhysics,
                
                Area = form.UniversityAdmission.Area,
                Connected =form.UniversityAdmission.Connected,
                GraduateYear =form.UniversityAdmission.GraduateYear,
                PriorityType =form.UniversityAdmission.PriorityType,
                FormDetails = formDTO.FormDetails.Select(m => new FormDetailDTO
                {
                    Id = m.Id,
                    FormId = m.FormId,
                    MajorsCode = m.MajorsCode,
                    MajorsId = m.MajorsId,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName
                }).ToList()
            };

            if (form.IsValidated)
            {
                return formDTO;
            }
            else
            {
                formDTO.Id = null;
                throw new BadRequestException(formDTO);
            }
        }

        [Route(FormRoute.Get), HttpPost]
        public async Task<FormDTO> Get([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = await ConvertDTOtoBO(formDTO);

            form = await FormService.Get(form.Id);
            return new FormDTO
            {
                Id = form.Id,
                NumberForm = form.NumberForm,
                DepartmentCode = form.DepartmentCode,
                Date = form.Date,
                UserId = form.UserId,

                FullName = form.PersonalInformation.FullName,
                Dob = form.PersonalInformation.Dob,
                Gender = form.PersonalInformation.Gender,
                Identify = form.PersonalInformation.Identify,
                PlaceOfBirth = form.PersonalInformation.PlaceOfBirth,
                TownId = form.PersonalInformation.TownId,
                TownCode = form.PersonalInformation.TownCode,
                TownName = form.PersonalInformation.TownName,
                DistrictCode = form.PersonalInformation.DistrictCode,
                DistrictName = form.PersonalInformation.DistrictName,
                ProvinceCode = form.PersonalInformation.ProvinceCode,
                ProvinceName = form.PersonalInformation.ProvinceName,
                Address = form.PersonalInformation.Address,
                Phone = form.PersonalInformation.Phone,
                Email = form.PersonalInformation.Email,
                Ethnic = form.PersonalInformation.Ethnic,
                Grade12Name = form.PersonalInformation.Grade12Name,
                HighSchoolGrade10Id = form.PersonalInformation.HighSchoolGrade10Id,
                HighSchoolGrade10Code = form.PersonalInformation.HighSchoolGrade10Code,
                HighSchoolGrade10Name = form.PersonalInformation.HighSchoolGrade10Name,
                HighSchoolGrade10DistrictCode = form.PersonalInformation.HighSchoolGrade10DistrictCode,
                HighSchoolGrade10DistrictName = form.PersonalInformation.HighSchoolGrade10DistrictName,
                HighSchoolGrade10ProvinceCode = form.PersonalInformation.HighSchoolGrade10ProvinceCode,
                HighSchoolGrade10ProvinceName = form.PersonalInformation.HighSchoolGrade10ProvinceName,
                HighSchoolGrade11Id = form.PersonalInformation.HighSchoolGrade11Id,
                HighSchoolGrade11Code = form.PersonalInformation.HighSchoolGrade11Code,
                HighSchoolGrade11Name = form.PersonalInformation.HighSchoolGrade11Name,
                HighSchoolGrade11DistrictCode = form.PersonalInformation.HighSchoolGrade11DistrictCode,
                HighSchoolGrade11DistrictName = form.PersonalInformation.HighSchoolGrade11DistrictName,
                HighSchoolGrade11ProvinceCode = form.PersonalInformation.HighSchoolGrade11ProvinceCode,
                HighSchoolGrade11ProvinceName = form.PersonalInformation.HighSchoolGrade11ProvinceName,
                HighSchoolGrade12Id = form.PersonalInformation.HighSchoolGrade12Id,
                HighSchoolGrade12Code = form.PersonalInformation.HighSchoolGrade12Code,
                HighSchoolGrade12Name = form.PersonalInformation.HighSchoolGrade12Name,
                HighSchoolGrade12DistrictCode = form.PersonalInformation.HighSchoolGrade12DistrictCode,
                HighSchoolGrade12DistrictName = form.PersonalInformation.HighSchoolGrade12DistrictName,
                HighSchoolGrade12ProvinceCode = form.PersonalInformation.HighSchoolGrade12ProvinceCode,
                HighSchoolGrade12ProvinceName = form.PersonalInformation.HighSchoolGrade12ProvinceName,
                IsPermanentResidenceMore18 = form.PersonalInformation.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = form.PersonalInformation.IsPermanentResidenceSpecialMore18,

                ResultForUniversity = form.RegisterInformation.ResultForUniversity,
                StudyAtHighSchool = form.RegisterInformation.StudyAtHighSchool,
                ClusterContestId = form.RegisterInformation.ClusterContestId,
                ClusterContestCode = form.RegisterInformation.ClusterContestCode,
                ClusterContestName = form.RegisterInformation.ClusterContestName,
                RegisterPlaceOfExamId = form.RegisterInformation.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = form.RegisterInformation.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = form.RegisterInformation.RegisterPlaceOfExamName,
                Biology = form.RegisterInformation.Biology,
                Chemistry = form.RegisterInformation.Chemistry,
                CivicEducation = form.RegisterInformation.CivicEducation,
                Geography = form.RegisterInformation.Geography,
                History = form.RegisterInformation.History,
                Languages = form.RegisterInformation.Languages,
                Literature = form.RegisterInformation.Literature,
                Maths = form.RegisterInformation.Maths,
                NaturalSciences = form.RegisterInformation.NaturalSciences,
                Physics = form.RegisterInformation.Physics,
                SocialSciences = form.RegisterInformation.SocialSciences,
                Graduated = form.RegisterInformation.Graduated,

                ExceptLanguages = form.GraduationInformation.ExceptLanguages,
                Mark = form.GraduationInformation.Mark,
                ReserveBiology = form.GraduationInformation.ReserveBiology,
                ReserveChemistry = form.GraduationInformation.ReserveChemistry,
                ReserveCivicEducation = form.GraduationInformation.ReserveCivicEducation,
                ReserveGeography = form.GraduationInformation.ReserveGeography,
                ReserveHistory = form.GraduationInformation.ReserveHistory,
                ReserveLanguages = form.GraduationInformation.ReserveLanguages,
                ReserveLiterature = form.GraduationInformation.ReserveLiterature,
                ReserveMaths = form.GraduationInformation.ReserveMaths,
                ReservePhysics = form.GraduationInformation.ReservePhysics,

                Area = form.UniversityAdmission.Area,
                Connected = form.UniversityAdmission.Connected,
                GraduateYear = form.UniversityAdmission.GraduateYear,
                PriorityType = form.UniversityAdmission.PriorityType,
                FormDetails = formDTO.FormDetails.Select(m => new FormDetailDTO
                {
                    Id = m.Id,
                    FormId = m.FormId,
                    MajorsCode = m.MajorsCode,
                    MajorsId = m.MajorsId,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName
                }).ToList()
            };
        }

        [Route(FormRoute.UpdatePersonal), HttpPost]
        public async Task<FormDTO> UpdatePersonal([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = await ConvertDTOtoBO(formDTO);
            PersonalInformation personal = new PersonalInformation
            {
                FullName = formDTO.FullName,
                Dob = formDTO.Dob,
                Gender = formDTO.Gender,
                Identify = formDTO.Identify,
                PlaceOfBirth = formDTO.PlaceOfBirth,
                TownId = formDTO.TownId,
                TownCode = formDTO.TownCode,
                TownName = formDTO.TownName,
                DistrictCode = formDTO.DistrictCode,
                DistrictName = formDTO.DistrictName,
                ProvinceCode = formDTO.ProvinceCode,
                ProvinceName = formDTO.ProvinceName,
                Address = formDTO.Address,
                Phone = formDTO.Phone,
                Email = formDTO.Email,
                Ethnic = formDTO.Ethnic,
                Grade12Name = formDTO.Grade12Name,
                HighSchoolGrade10Id = formDTO.HighSchoolGrade10Id,
                HighSchoolGrade10Code = formDTO.HighSchoolGrade10Code,
                HighSchoolGrade10Name = formDTO.HighSchoolGrade10Name,
                HighSchoolGrade10DistrictCode = formDTO.HighSchoolGrade10DistrictCode,
                HighSchoolGrade10DistrictName = formDTO.HighSchoolGrade10DistrictName,
                HighSchoolGrade10ProvinceCode = formDTO.HighSchoolGrade10ProvinceCode,
                HighSchoolGrade10ProvinceName = formDTO.HighSchoolGrade10ProvinceName,
                HighSchoolGrade11Id = formDTO.HighSchoolGrade11Id,
                HighSchoolGrade11Code = formDTO.HighSchoolGrade11Code,
                HighSchoolGrade11Name = formDTO.HighSchoolGrade11Name,
                HighSchoolGrade11DistrictCode = formDTO.HighSchoolGrade11DistrictCode,
                HighSchoolGrade11DistrictName = formDTO.HighSchoolGrade11DistrictName,
                HighSchoolGrade11ProvinceCode = formDTO.HighSchoolGrade11ProvinceCode,
                HighSchoolGrade11ProvinceName = formDTO.HighSchoolGrade11ProvinceName,
                HighSchoolGrade12Id = formDTO.HighSchoolGrade12Id,
                HighSchoolGrade12Code = formDTO.HighSchoolGrade12Code,
                HighSchoolGrade12Name = formDTO.HighSchoolGrade12Name,
                HighSchoolGrade12DistrictCode = formDTO.HighSchoolGrade12DistrictCode,
                HighSchoolGrade12DistrictName = formDTO.HighSchoolGrade12DistrictName,
                HighSchoolGrade12ProvinceCode = formDTO.HighSchoolGrade12ProvinceCode,
                HighSchoolGrade12ProvinceName = formDTO.HighSchoolGrade12ProvinceName,
                IsPermanentResidenceMore18 = formDTO.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = formDTO.IsPermanentResidenceSpecialMore18,
            };

            form = await FormService.Update(form);
            personal = await PeronalInformationService.Update(personal);

            formDTO = new FormDTO
            {
                Id = form.Id,
                NumberForm = form.NumberForm,
                DepartmentCode = form.DepartmentCode,
                Date = form.Date,
                UserId = form.UserId,

                FullName = personal.FullName,
                Dob = personal.Dob,
                Gender = personal.Gender,
                Identify = personal.Identify,
                PlaceOfBirth = personal.PlaceOfBirth,
                TownId = personal.TownId,
                TownCode = personal.TownCode,
                TownName = personal.TownName,
                DistrictCode = personal.DistrictCode,
                DistrictName = personal.DistrictName,
                ProvinceCode = personal.ProvinceCode,
                ProvinceName = personal.ProvinceName,
                Address = personal.Address,
                Phone = personal.Phone,
                Email = personal.Email,
                Ethnic = personal.Ethnic,
                Grade12Name = personal.Grade12Name,
                HighSchoolGrade10Id = personal.HighSchoolGrade10Id,
                HighSchoolGrade10Code = personal.HighSchoolGrade10Code,
                HighSchoolGrade10Name = personal.HighSchoolGrade10Name,
                HighSchoolGrade10DistrictCode = personal.HighSchoolGrade10DistrictCode,
                HighSchoolGrade10DistrictName = personal.HighSchoolGrade10DistrictName,
                HighSchoolGrade10ProvinceCode = personal.HighSchoolGrade10ProvinceCode,
                HighSchoolGrade10ProvinceName = personal.HighSchoolGrade10ProvinceName,
                HighSchoolGrade11Id = personal.HighSchoolGrade11Id,
                HighSchoolGrade11Code = personal.HighSchoolGrade11Code,
                HighSchoolGrade11Name = personal.HighSchoolGrade11Name,
                HighSchoolGrade11DistrictCode = personal.HighSchoolGrade11DistrictCode,
                HighSchoolGrade11DistrictName = personal.HighSchoolGrade11DistrictName,
                HighSchoolGrade11ProvinceCode = personal.HighSchoolGrade11ProvinceCode,
                HighSchoolGrade11ProvinceName = personal.HighSchoolGrade11ProvinceName,
                HighSchoolGrade12Id = personal.HighSchoolGrade12Id,
                HighSchoolGrade12Code = personal.HighSchoolGrade12Code,
                HighSchoolGrade12Name = personal.HighSchoolGrade12Name,
                HighSchoolGrade12DistrictCode = personal.HighSchoolGrade12DistrictCode,
                HighSchoolGrade12DistrictName = personal.HighSchoolGrade12DistrictName,
                HighSchoolGrade12ProvinceCode = personal.HighSchoolGrade12ProvinceCode,
                HighSchoolGrade12ProvinceName = personal.HighSchoolGrade12ProvinceName,
                IsPermanentResidenceMore18 = personal.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = personal.IsPermanentResidenceSpecialMore18,

                ResultForUniversity = form.RegisterInformation.ResultForUniversity,
                StudyAtHighSchool = form.RegisterInformation.StudyAtHighSchool,
                ClusterContestId = form.RegisterInformation.ClusterContestId,
                ClusterContestCode = form.RegisterInformation.ClusterContestCode,
                ClusterContestName = form.RegisterInformation.ClusterContestName,
                RegisterPlaceOfExamId = form.RegisterInformation.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = form.RegisterInformation.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = form.RegisterInformation.RegisterPlaceOfExamName,
                Biology = form.RegisterInformation.Biology,
                Chemistry = form.RegisterInformation.Chemistry,
                CivicEducation = form.RegisterInformation.CivicEducation,
                Geography = form.RegisterInformation.Geography,
                History = form.RegisterInformation.History,
                Languages = form.RegisterInformation.Languages,
                Literature = form.RegisterInformation.Literature,
                Maths = form.RegisterInformation.Maths,
                NaturalSciences = form.RegisterInformation.NaturalSciences,
                Physics = form.RegisterInformation.Physics,
                SocialSciences = form.RegisterInformation.SocialSciences,
                Graduated = form.RegisterInformation.Graduated,

                ExceptLanguages = form.GraduationInformation.ExceptLanguages,
                Mark = form.GraduationInformation.Mark,
                ReserveBiology = form.GraduationInformation.ReserveBiology,
                ReserveChemistry = form.GraduationInformation.ReserveChemistry,
                ReserveCivicEducation = form.GraduationInformation.ReserveCivicEducation,
                ReserveGeography = form.GraduationInformation.ReserveGeography,
                ReserveHistory = form.GraduationInformation.ReserveHistory,
                ReserveLanguages = form.GraduationInformation.ReserveLanguages,
                ReserveLiterature = form.GraduationInformation.ReserveLiterature,
                ReserveMaths = form.GraduationInformation.ReserveMaths,
                ReservePhysics = form.GraduationInformation.ReservePhysics,

                Area = form.UniversityAdmission.Area,
                Connected = form.UniversityAdmission.Connected,
                GraduateYear = form.UniversityAdmission.GraduateYear,
                PriorityType = form.UniversityAdmission.PriorityType,
                FormDetails = formDTO.FormDetails.Select(m => new FormDetailDTO
                {
                    Id = m.Id,
                    FormId = m.FormId,
                    MajorsCode = m.MajorsCode,
                    MajorsId = m.MajorsId,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName
                }).ToList()
            };

            if (form.IsValidated)
            {
                return formDTO;
            }
            else
            {
                throw new BadRequestException(formDTO);
            }
        }

        [Route(FormRoute.UpdateRegister), HttpPost]
        public async Task<FormDTO> UpdateRegister([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = await ConvertDTOtoBO(formDTO);
            RegisterInformation register = new RegisterInformation
            {
                ResultForUniversity = formDTO.ResultForUniversity,
                StudyAtHighSchool = formDTO.StudyAtHighSchool,
                ClusterContestId = formDTO.ClusterContestId,
                ClusterContestCode = formDTO.ClusterContestCode,
                ClusterContestName = formDTO.ClusterContestName,
                RegisterPlaceOfExamId = formDTO.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = formDTO.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = formDTO.RegisterPlaceOfExamName,
                Biology = formDTO.Biology,
                Chemistry = formDTO.Chemistry,
                CivicEducation = formDTO.CivicEducation,
                Geography = formDTO.Geography,
                History = formDTO.History,
                Languages = formDTO.Languages,
                Literature = formDTO.Literature,
                Maths = formDTO.Maths,
                NaturalSciences = formDTO.NaturalSciences,
                Physics = formDTO.Physics,
                SocialSciences = formDTO.SocialSciences,
                Graduated = formDTO.Graduated,
            };

            form = await FormService.Update(form);
            register = await RegisterInformationService.Update(register);

            formDTO = new FormDTO
            {
                Id = form.Id,
                NumberForm = form.NumberForm,
                DepartmentCode = form.DepartmentCode,
                Date = form.Date,
                UserId = form.UserId,

                FullName = form.PersonalInformation.FullName,
                Dob = form.PersonalInformation.Dob,
                Gender = form.PersonalInformation.Gender,
                Identify = form.PersonalInformation.Identify,
                PlaceOfBirth = form.PersonalInformation.PlaceOfBirth,
                TownId = form.PersonalInformation.TownId,
                TownCode = form.PersonalInformation.TownCode,
                TownName = form.PersonalInformation.TownName,
                DistrictCode = form.PersonalInformation.DistrictCode,
                DistrictName = form.PersonalInformation.DistrictName,
                ProvinceCode = form.PersonalInformation.ProvinceCode,
                ProvinceName = form.PersonalInformation.ProvinceName,
                Address = form.PersonalInformation.Address,
                Phone = form.PersonalInformation.Phone,
                Email = form.PersonalInformation.Email,
                Ethnic = form.PersonalInformation.Ethnic,
                Grade12Name = form.PersonalInformation.Grade12Name,
                HighSchoolGrade10Id = form.PersonalInformation.HighSchoolGrade10Id,
                HighSchoolGrade10Code = form.PersonalInformation.HighSchoolGrade10Code,
                HighSchoolGrade10Name = form.PersonalInformation.HighSchoolGrade10Name,
                HighSchoolGrade10DistrictCode = form.PersonalInformation.HighSchoolGrade10DistrictCode,
                HighSchoolGrade10DistrictName = form.PersonalInformation.HighSchoolGrade10DistrictName,
                HighSchoolGrade10ProvinceCode = form.PersonalInformation.HighSchoolGrade10ProvinceCode,
                HighSchoolGrade10ProvinceName = form.PersonalInformation.HighSchoolGrade10ProvinceName,
                HighSchoolGrade11Id = form.PersonalInformation.HighSchoolGrade11Id,
                HighSchoolGrade11Code = form.PersonalInformation.HighSchoolGrade11Code,
                HighSchoolGrade11Name = form.PersonalInformation.HighSchoolGrade11Name,
                HighSchoolGrade11DistrictCode = form.PersonalInformation.HighSchoolGrade11DistrictCode,
                HighSchoolGrade11DistrictName = form.PersonalInformation.HighSchoolGrade11DistrictName,
                HighSchoolGrade11ProvinceCode = form.PersonalInformation.HighSchoolGrade11ProvinceCode,
                HighSchoolGrade11ProvinceName = form.PersonalInformation.HighSchoolGrade11ProvinceName,
                HighSchoolGrade12Id = form.PersonalInformation.HighSchoolGrade12Id,
                HighSchoolGrade12Code = form.PersonalInformation.HighSchoolGrade12Code,
                HighSchoolGrade12Name = form.PersonalInformation.HighSchoolGrade12Name,
                HighSchoolGrade12DistrictCode = form.PersonalInformation.HighSchoolGrade12DistrictCode,
                HighSchoolGrade12DistrictName = form.PersonalInformation.HighSchoolGrade12DistrictName,
                HighSchoolGrade12ProvinceCode = form.PersonalInformation.HighSchoolGrade12ProvinceCode,
                HighSchoolGrade12ProvinceName = form.PersonalInformation.HighSchoolGrade12ProvinceName,
                IsPermanentResidenceMore18 = form.PersonalInformation.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = form.PersonalInformation.IsPermanentResidenceSpecialMore18,

                ResultForUniversity = register.ResultForUniversity,
                StudyAtHighSchool = register.StudyAtHighSchool,
                ClusterContestId = register.ClusterContestId,
                ClusterContestCode = register.ClusterContestCode,
                ClusterContestName = register.ClusterContestName,
                RegisterPlaceOfExamId = register.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = register.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = register.RegisterPlaceOfExamName,
                Biology = register.Biology,
                Chemistry = register.Chemistry,
                CivicEducation = register.CivicEducation,
                Geography = register.Geography,
                History = register.History,
                Languages = register.Languages,
                Literature = register.Literature,
                Maths = register.Maths,
                NaturalSciences = register.NaturalSciences,
                Physics = register.Physics,
                SocialSciences = register.SocialSciences,
                Graduated = register.Graduated,

                ExceptLanguages = form.GraduationInformation.ExceptLanguages,
                Mark = form.GraduationInformation.Mark,
                ReserveBiology = form.GraduationInformation.ReserveBiology,
                ReserveChemistry = form.GraduationInformation.ReserveChemistry,
                ReserveCivicEducation = form.GraduationInformation.ReserveCivicEducation,
                ReserveGeography = form.GraduationInformation.ReserveGeography,
                ReserveHistory = form.GraduationInformation.ReserveHistory,
                ReserveLanguages = form.GraduationInformation.ReserveLanguages,
                ReserveLiterature = form.GraduationInformation.ReserveLiterature,
                ReserveMaths = form.GraduationInformation.ReserveMaths,
                ReservePhysics = form.GraduationInformation.ReservePhysics,

                Area = form.UniversityAdmission.Area,
                Connected = form.UniversityAdmission.Connected,
                GraduateYear = form.UniversityAdmission.GraduateYear,
                PriorityType = form.UniversityAdmission.PriorityType,
                FormDetails = formDTO.FormDetails.Select(m => new FormDetailDTO
                {
                    Id = m.Id,
                    FormId = m.FormId,
                    MajorsCode = m.MajorsCode,
                    MajorsId = m.MajorsId,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName
                }).ToList()
            };

            if (form.IsValidated)
            {
                return formDTO;
            }
            else
            {
                throw new BadRequestException(formDTO);
            }
        }

        [Route(FormRoute.UpdateGraduate), HttpPost]
        public async Task<FormDTO> UpdateGraduate([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = await ConvertDTOtoBO(formDTO);
            GraduationInformation graduation = new GraduationInformation
            {
                ExceptLanguages = formDTO.ExceptLanguages,
                Mark = formDTO.Mark,
                ReserveBiology = formDTO.ReserveBiology,
                ReserveChemistry = formDTO.ReserveChemistry,
                ReserveCivicEducation = formDTO.ReserveCivicEducation,
                ReserveGeography = formDTO.ReserveGeography,
                ReserveHistory = formDTO.ReserveHistory,
                ReserveLanguages = formDTO.ReserveLanguages,
                ReserveLiterature = formDTO.ReserveLiterature,
                ReserveMaths = formDTO.ReserveMaths,
                ReservePhysics = formDTO.ReservePhysics,
            };

            form = await FormService.Update(form);
            graduation = await GraduationInformationService.Update(graduation);

            formDTO = new FormDTO
            {
                Id = form.Id,
                NumberForm = form.NumberForm,
                DepartmentCode = form.DepartmentCode,
                Date = form.Date,
                UserId = form.UserId,

                FullName = form.PersonalInformation.FullName,
                Dob = form.PersonalInformation.Dob,
                Gender = form.PersonalInformation.Gender,
                Identify = form.PersonalInformation.Identify,
                PlaceOfBirth = form.PersonalInformation.PlaceOfBirth,
                TownId = form.PersonalInformation.TownId,
                TownCode = form.PersonalInformation.TownCode,
                TownName = form.PersonalInformation.TownName,
                DistrictCode = form.PersonalInformation.DistrictCode,
                DistrictName = form.PersonalInformation.DistrictName,
                ProvinceCode = form.PersonalInformation.ProvinceCode,
                ProvinceName = form.PersonalInformation.ProvinceName,
                Address = form.PersonalInformation.Address,
                Phone = form.PersonalInformation.Phone,
                Email = form.PersonalInformation.Email,
                Ethnic = form.PersonalInformation.Ethnic,
                Grade12Name = form.PersonalInformation.Grade12Name,
                HighSchoolGrade10Id = form.PersonalInformation.HighSchoolGrade10Id,
                HighSchoolGrade10Code = form.PersonalInformation.HighSchoolGrade10Code,
                HighSchoolGrade10Name = form.PersonalInformation.HighSchoolGrade10Name,
                HighSchoolGrade10DistrictCode = form.PersonalInformation.HighSchoolGrade10DistrictCode,
                HighSchoolGrade10DistrictName = form.PersonalInformation.HighSchoolGrade10DistrictName,
                HighSchoolGrade10ProvinceCode = form.PersonalInformation.HighSchoolGrade10ProvinceCode,
                HighSchoolGrade10ProvinceName = form.PersonalInformation.HighSchoolGrade10ProvinceName,
                HighSchoolGrade11Id = form.PersonalInformation.HighSchoolGrade11Id,
                HighSchoolGrade11Code = form.PersonalInformation.HighSchoolGrade11Code,
                HighSchoolGrade11Name = form.PersonalInformation.HighSchoolGrade11Name,
                HighSchoolGrade11DistrictCode = form.PersonalInformation.HighSchoolGrade11DistrictCode,
                HighSchoolGrade11DistrictName = form.PersonalInformation.HighSchoolGrade11DistrictName,
                HighSchoolGrade11ProvinceCode = form.PersonalInformation.HighSchoolGrade11ProvinceCode,
                HighSchoolGrade11ProvinceName = form.PersonalInformation.HighSchoolGrade11ProvinceName,
                HighSchoolGrade12Id = form.PersonalInformation.HighSchoolGrade12Id,
                HighSchoolGrade12Code = form.PersonalInformation.HighSchoolGrade12Code,
                HighSchoolGrade12Name = form.PersonalInformation.HighSchoolGrade12Name,
                HighSchoolGrade12DistrictCode = form.PersonalInformation.HighSchoolGrade12DistrictCode,
                HighSchoolGrade12DistrictName = form.PersonalInformation.HighSchoolGrade12DistrictName,
                HighSchoolGrade12ProvinceCode = form.PersonalInformation.HighSchoolGrade12ProvinceCode,
                HighSchoolGrade12ProvinceName = form.PersonalInformation.HighSchoolGrade12ProvinceName,
                IsPermanentResidenceMore18 = form.PersonalInformation.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = form.PersonalInformation.IsPermanentResidenceSpecialMore18,

                ResultForUniversity = form.RegisterInformation.ResultForUniversity,
                StudyAtHighSchool = form.RegisterInformation.StudyAtHighSchool,
                ClusterContestId = form.RegisterInformation.ClusterContestId,
                ClusterContestCode = form.RegisterInformation.ClusterContestCode,
                ClusterContestName = form.RegisterInformation.ClusterContestName,
                RegisterPlaceOfExamId = form.RegisterInformation.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = form.RegisterInformation.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = form.RegisterInformation.RegisterPlaceOfExamName,
                Biology = form.RegisterInformation.Biology,
                Chemistry = form.RegisterInformation.Chemistry,
                CivicEducation = form.RegisterInformation.CivicEducation,
                Geography = form.RegisterInformation.Geography,
                History = form.RegisterInformation.History,
                Languages = form.RegisterInformation.Languages,
                Literature = form.RegisterInformation.Literature,
                Maths = form.RegisterInformation.Maths,
                NaturalSciences = form.RegisterInformation.NaturalSciences,
                Physics = form.RegisterInformation.Physics,
                SocialSciences = form.RegisterInformation.SocialSciences,
                Graduated = form.RegisterInformation.Graduated,

                ExceptLanguages = graduation.ExceptLanguages,
                Mark = graduation.Mark,
                ReserveBiology = graduation.ReserveBiology,
                ReserveChemistry = graduation.ReserveChemistry,
                ReserveCivicEducation = graduation.ReserveCivicEducation,
                ReserveGeography = graduation.ReserveGeography,
                ReserveHistory = graduation.ReserveHistory,
                ReserveLanguages = graduation.ReserveLanguages,
                ReserveLiterature = graduation.ReserveLiterature,
                ReserveMaths = graduation.ReserveMaths,
                ReservePhysics = graduation.ReservePhysics,

                Area = form.UniversityAdmission.Area,
                Connected = form.UniversityAdmission.Connected,
                GraduateYear = form.UniversityAdmission.GraduateYear,
                PriorityType = form.UniversityAdmission.PriorityType,
                FormDetails = formDTO.FormDetails.Select(m => new FormDetailDTO
                {
                    Id = m.Id,
                    FormId = m.FormId,
                    MajorsCode = m.MajorsCode,
                    MajorsId = m.MajorsId,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName
                }).ToList()
            };

            if (form.IsValidated)
            {
                return formDTO;
            }
            else
            {
                throw new BadRequestException(formDTO);
            }
        }

        [Route(FormRoute.UpdateUniversity), HttpPost]
        public async Task<FormDTO> UpdateUniversity([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = await ConvertDTOtoBO(formDTO);
            UniversityAdmission university = new UniversityAdmission
            {
                Area = formDTO.Area,
                Connected = formDTO.Connected,
                GraduateYear = formDTO.GraduateYear,
                PriorityType = formDTO.PriorityType,
                FormDetails = formDTO.FormDetails.Select(m => new FormDetail
                {
                    Id = m.Id ?? Guid.Empty,
                    FormId = m.FormId,
                    MajorsCode = m.MajorsCode,
                    MajorsId = m.MajorsId,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName
                }).ToList()
            };

            form = await FormService.Update(form);
            university = await UniversityAdmissionService.Update(university);

            formDTO = new FormDTO
            {
                Id = form.Id,
                NumberForm = form.NumberForm,
                DepartmentCode = form.DepartmentCode,
                Date = form.Date,
                UserId = form.UserId,

                FullName = form.PersonalInformation.FullName,
                Dob = form.PersonalInformation.Dob,
                Gender = form.PersonalInformation.Gender,
                Identify = form.PersonalInformation.Identify,
                PlaceOfBirth = form.PersonalInformation.PlaceOfBirth,
                TownId = form.PersonalInformation.TownId,
                TownCode = form.PersonalInformation.TownCode,
                TownName = form.PersonalInformation.TownName,
                DistrictCode = form.PersonalInformation.DistrictCode,
                DistrictName = form.PersonalInformation.DistrictName,
                ProvinceCode = form.PersonalInformation.ProvinceCode,
                ProvinceName = form.PersonalInformation.ProvinceName,
                Address = form.PersonalInformation.Address,
                Phone = form.PersonalInformation.Phone,
                Email = form.PersonalInformation.Email,
                Ethnic = form.PersonalInformation.Ethnic,
                Grade12Name = form.PersonalInformation.Grade12Name,
                HighSchoolGrade10Id = form.PersonalInformation.HighSchoolGrade10Id,
                HighSchoolGrade10Code = form.PersonalInformation.HighSchoolGrade10Code,
                HighSchoolGrade10Name = form.PersonalInformation.HighSchoolGrade10Name,
                HighSchoolGrade10DistrictCode = form.PersonalInformation.HighSchoolGrade10DistrictCode,
                HighSchoolGrade10DistrictName = form.PersonalInformation.HighSchoolGrade10DistrictName,
                HighSchoolGrade10ProvinceCode = form.PersonalInformation.HighSchoolGrade10ProvinceCode,
                HighSchoolGrade10ProvinceName = form.PersonalInformation.HighSchoolGrade10ProvinceName,
                HighSchoolGrade11Id = form.PersonalInformation.HighSchoolGrade11Id,
                HighSchoolGrade11Code = form.PersonalInformation.HighSchoolGrade11Code,
                HighSchoolGrade11Name = form.PersonalInformation.HighSchoolGrade11Name,
                HighSchoolGrade11DistrictCode = form.PersonalInformation.HighSchoolGrade11DistrictCode,
                HighSchoolGrade11DistrictName = form.PersonalInformation.HighSchoolGrade11DistrictName,
                HighSchoolGrade11ProvinceCode = form.PersonalInformation.HighSchoolGrade11ProvinceCode,
                HighSchoolGrade11ProvinceName = form.PersonalInformation.HighSchoolGrade11ProvinceName,
                HighSchoolGrade12Id = form.PersonalInformation.HighSchoolGrade12Id,
                HighSchoolGrade12Code = form.PersonalInformation.HighSchoolGrade12Code,
                HighSchoolGrade12Name = form.PersonalInformation.HighSchoolGrade12Name,
                HighSchoolGrade12DistrictCode = form.PersonalInformation.HighSchoolGrade12DistrictCode,
                HighSchoolGrade12DistrictName = form.PersonalInformation.HighSchoolGrade12DistrictName,
                HighSchoolGrade12ProvinceCode = form.PersonalInformation.HighSchoolGrade12ProvinceCode,
                HighSchoolGrade12ProvinceName = form.PersonalInformation.HighSchoolGrade12ProvinceName,
                IsPermanentResidenceMore18 = form.PersonalInformation.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = form.PersonalInformation.IsPermanentResidenceSpecialMore18,

                ResultForUniversity = form.RegisterInformation.ResultForUniversity,
                StudyAtHighSchool = form.RegisterInformation.StudyAtHighSchool,
                ClusterContestId = form.RegisterInformation.ClusterContestId,
                ClusterContestCode = form.RegisterInformation.ClusterContestCode,
                ClusterContestName = form.RegisterInformation.ClusterContestName,
                RegisterPlaceOfExamId = form.RegisterInformation.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = form.RegisterInformation.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = form.RegisterInformation.RegisterPlaceOfExamName,
                Biology = form.RegisterInformation.Biology,
                Chemistry = form.RegisterInformation.Chemistry,
                CivicEducation = form.RegisterInformation.CivicEducation,
                Geography = form.RegisterInformation.Geography,
                History = form.RegisterInformation.History,
                Languages = form.RegisterInformation.Languages,
                Literature = form.RegisterInformation.Literature,
                Maths = form.RegisterInformation.Maths,
                NaturalSciences = form.RegisterInformation.NaturalSciences,
                Physics = form.RegisterInformation.Physics,
                SocialSciences = form.RegisterInformation.SocialSciences,
                Graduated = form.RegisterInformation.Graduated,

                ExceptLanguages = form.GraduationInformation.ExceptLanguages,
                Mark = form.GraduationInformation.Mark,
                ReserveBiology = form.GraduationInformation.ReserveBiology,
                ReserveChemistry = form.GraduationInformation.ReserveChemistry,
                ReserveCivicEducation = form.GraduationInformation.ReserveCivicEducation,
                ReserveGeography = form.GraduationInformation.ReserveGeography,
                ReserveHistory = form.GraduationInformation.ReserveHistory,
                ReserveLanguages = form.GraduationInformation.ReserveLanguages,
                ReserveLiterature = form.GraduationInformation.ReserveLiterature,
                ReserveMaths = form.GraduationInformation.ReserveMaths,
                ReservePhysics = form.GraduationInformation.ReservePhysics,

                Area = formDTO.Area,
                Connected = formDTO.Connected,
                GraduateYear = formDTO.GraduateYear,
                PriorityType = formDTO.PriorityType,
                FormDetails = formDTO.FormDetails.Select(m => new FormDetailDTO
                {
                    Id = m.Id,
                    FormId = m.FormId,
                    MajorsCode = m.MajorsCode,
                    MajorsId = m.MajorsId,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName
                }).ToList()
            };

            if (form.IsValidated)
            {
                return formDTO;
            }
            else
            {
                throw new BadRequestException(formDTO);
            }
        }

        public async Task<FormDTO> Delete([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();

            Form form = await ConvertDTOtoBO(formDTO);

            form = await FormService.Delete(form);

            formDTO = new FormDTO
            {
                Id = form.Id,
                NumberForm = form.NumberForm,
                DepartmentCode = form.DepartmentCode,
                Date = form.Date,
                UserId = form.UserId,

                FullName = form.PersonalInformation.FullName,
                Dob = form.PersonalInformation.Dob,
                Gender = form.PersonalInformation.Gender,
                Identify = form.PersonalInformation.Identify,
                PlaceOfBirth = form.PersonalInformation.PlaceOfBirth,
                TownId = form.PersonalInformation.TownId,
                TownCode = form.PersonalInformation.TownCode,
                TownName = form.PersonalInformation.TownName,
                DistrictCode = form.PersonalInformation.DistrictCode,
                DistrictName = form.PersonalInformation.DistrictName,
                ProvinceCode = form.PersonalInformation.ProvinceCode,
                ProvinceName = form.PersonalInformation.ProvinceName,
                Address = form.PersonalInformation.Address,
                Phone = form.PersonalInformation.Phone,
                Email = form.PersonalInformation.Email,
                Ethnic = form.PersonalInformation.Ethnic,
                Grade12Name = form.PersonalInformation.Grade12Name,
                HighSchoolGrade10Id = form.PersonalInformation.HighSchoolGrade10Id,
                HighSchoolGrade10Code = form.PersonalInformation.HighSchoolGrade10Code,
                HighSchoolGrade10Name = form.PersonalInformation.HighSchoolGrade10Name,
                HighSchoolGrade10DistrictCode = form.PersonalInformation.HighSchoolGrade10DistrictCode,
                HighSchoolGrade10DistrictName = form.PersonalInformation.HighSchoolGrade10DistrictName,
                HighSchoolGrade10ProvinceCode = form.PersonalInformation.HighSchoolGrade10ProvinceCode,
                HighSchoolGrade10ProvinceName = form.PersonalInformation.HighSchoolGrade10ProvinceName,
                HighSchoolGrade11Id = form.PersonalInformation.HighSchoolGrade11Id,
                HighSchoolGrade11Code = form.PersonalInformation.HighSchoolGrade11Code,
                HighSchoolGrade11Name = form.PersonalInformation.HighSchoolGrade11Name,
                HighSchoolGrade11DistrictCode = form.PersonalInformation.HighSchoolGrade11DistrictCode,
                HighSchoolGrade11DistrictName = form.PersonalInformation.HighSchoolGrade11DistrictName,
                HighSchoolGrade11ProvinceCode = form.PersonalInformation.HighSchoolGrade11ProvinceCode,
                HighSchoolGrade11ProvinceName = form.PersonalInformation.HighSchoolGrade11ProvinceName,
                HighSchoolGrade12Id = form.PersonalInformation.HighSchoolGrade12Id,
                HighSchoolGrade12Code = form.PersonalInformation.HighSchoolGrade12Code,
                HighSchoolGrade12Name = form.PersonalInformation.HighSchoolGrade12Name,
                HighSchoolGrade12DistrictCode = form.PersonalInformation.HighSchoolGrade12DistrictCode,
                HighSchoolGrade12DistrictName = form.PersonalInformation.HighSchoolGrade12DistrictName,
                HighSchoolGrade12ProvinceCode = form.PersonalInformation.HighSchoolGrade12ProvinceCode,
                HighSchoolGrade12ProvinceName = form.PersonalInformation.HighSchoolGrade12ProvinceName,
                IsPermanentResidenceMore18 = form.PersonalInformation.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = form.PersonalInformation.IsPermanentResidenceSpecialMore18,

                ResultForUniversity = form.RegisterInformation.ResultForUniversity,
                StudyAtHighSchool = form.RegisterInformation.StudyAtHighSchool,
                ClusterContestId = form.RegisterInformation.ClusterContestId,
                ClusterContestCode = form.RegisterInformation.ClusterContestCode,
                ClusterContestName = form.RegisterInformation.ClusterContestName,
                RegisterPlaceOfExamId = form.RegisterInformation.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = form.RegisterInformation.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = form.RegisterInformation.RegisterPlaceOfExamName,
                Biology = form.RegisterInformation.Biology,
                Chemistry = form.RegisterInformation.Chemistry,
                CivicEducation = form.RegisterInformation.CivicEducation,
                Geography = form.RegisterInformation.Geography,
                History = form.RegisterInformation.History,
                Languages = form.RegisterInformation.Languages,
                Literature = form.RegisterInformation.Literature,
                Maths = form.RegisterInformation.Maths,
                NaturalSciences = form.RegisterInformation.NaturalSciences,
                Physics = form.RegisterInformation.Physics,
                SocialSciences = form.RegisterInformation.SocialSciences,
                Graduated = form.RegisterInformation.Graduated,

                ExceptLanguages = form.GraduationInformation.ExceptLanguages,
                Mark = form.GraduationInformation.Mark,
                ReserveBiology = form.GraduationInformation.ReserveBiology,
                ReserveChemistry = form.GraduationInformation.ReserveChemistry,
                ReserveCivicEducation = form.GraduationInformation.ReserveCivicEducation,
                ReserveGeography = form.GraduationInformation.ReserveGeography,
                ReserveHistory = form.GraduationInformation.ReserveHistory,
                ReserveLanguages = form.GraduationInformation.ReserveLanguages,
                ReserveLiterature = form.GraduationInformation.ReserveLiterature,
                ReserveMaths = form.GraduationInformation.ReserveMaths,
                ReservePhysics = form.GraduationInformation.ReservePhysics,

                Area = form.UniversityAdmission.Area,
                Connected = form.UniversityAdmission.Connected,
                GraduateYear = form.UniversityAdmission.GraduateYear,
                PriorityType = form.UniversityAdmission.PriorityType,
                FormDetails = form.UniversityAdmission.FormDetails.Select(m => new FormDetailDTO
                {
                    Id = m.Id,
                    FormId = m.FormId,
                    MajorsId = m.MajorsId,
                    MajorsCode = m.MajorsCode,
                    MajorsName = m.MajorsName,
                    UniversityId = m.UniversityId,
                    UniversityCode = m.UniversityCode,
                    UniversityName = m.UniversityName,
                    UniversityAddress = m.UniversityAddress,
                    SubjectGroupId = m.SubjectGroupId,
                    SubjectGroupCode = m.SubjectGroupCode,
                    SubjectGroupName = m.SubjectGroupName,
                }).ToList()
            };

            if (form.IsValidated)
                return formDTO;
            else throw new BadRequestException(formDTO);
        }

        private async Task<Form> ConvertDTOtoBO(FormDTO formDTO)
        {
            Form form = new Form
            {
                Id = formDTO.Id ?? Guid.Empty,
                Date = formDTO.Date,
                DepartmentCode = formDTO.DepartmentCode,
                NumberForm = formDTO.NumberForm,
                UserId = formDTO.UserId,

                PersonalInformation = new PersonalInformation
                {
                    Id = formDTO.Id ?? Guid.Empty,
                    FullName = formDTO.FullName,
                    Dob = formDTO.Dob,
                    Gender = formDTO.Gender,
                    PlaceOfBirth = formDTO.PlaceOfBirth,
                    Ethnic = formDTO.Ethnic,
                    Identify = formDTO.Identify,
                    TownId = formDTO.TownId,
                    TownCode = formDTO.TownCode,
                    TownName = formDTO.TownName,
                    DistrictCode = formDTO.DistrictCode,
                    DistrictName = formDTO.DistrictName,
                    ProvinceCode = formDTO.ProvinceCode,
                    ProvinceName = formDTO.ProvinceName,
                    IsPermanentResidenceMore18 = formDTO.IsPermanentResidenceMore18,
                    IsPermanentResidenceSpecialMore18 = formDTO.IsPermanentResidenceSpecialMore18,
                    HighSchoolGrade10Id = formDTO.HighSchoolGrade10Id,
                    HighSchoolGrade10Code = formDTO.HighSchoolGrade10Code,
                    HighSchoolGrade10Name = formDTO.HighSchoolGrade10Name,
                    HighSchoolGrade10DistrictCode = formDTO.HighSchoolGrade10DistrictCode,
                    HighSchoolGrade10DistrictName = formDTO.HighSchoolGrade10DistrictName,
                    HighSchoolGrade10ProvinceCode = formDTO.HighSchoolGrade10ProvinceCode,
                    HighSchoolGrade10ProvinceName = formDTO.HighSchoolGrade10ProvinceName,
                    HighSchoolGrade11Id = formDTO.HighSchoolGrade11Id,
                    HighSchoolGrade11Code = formDTO.HighSchoolGrade11Code,
                    HighSchoolGrade11Name = formDTO.HighSchoolGrade11Name,
                    HighSchoolGrade11DistrictCode = formDTO.HighSchoolGrade11DistrictCode,
                    HighSchoolGrade11DistrictName = formDTO.HighSchoolGrade11DistrictName,
                    HighSchoolGrade11ProvinceCode = formDTO.HighSchoolGrade11ProvinceCode,
                    HighSchoolGrade11ProvinceName = formDTO.HighSchoolGrade11ProvinceName,
                    HighSchoolGrade12Id = formDTO.HighSchoolGrade12Id,
                    HighSchoolGrade12Code = formDTO.HighSchoolGrade12Code,
                    HighSchoolGrade12Name = formDTO.HighSchoolGrade12Name,
                    HighSchoolGrade12DistrictCode = formDTO.HighSchoolGrade12DistrictCode,
                    HighSchoolGrade12DistrictName = formDTO.HighSchoolGrade12DistrictName,
                    HighSchoolGrade12ProvinceCode = formDTO.HighSchoolGrade12ProvinceCode,
                    HighSchoolGrade12ProvinceName = formDTO.HighSchoolGrade12ProvinceName,
                    Grade12Name = formDTO.Grade12Name,
                    Phone = formDTO.Phone,
                    Email = formDTO.Email,
                    Address = formDTO.Address
                },
                RegisterInformation = new RegisterInformation
                {
                    Id = formDTO.Id ?? Guid.Empty,
                    ResultForUniversity = formDTO.ResultForUniversity,
                    StudyAtHighSchool = formDTO.StudyAtHighSchool,
                    Graduated = formDTO.Graduated,
                    ClusterContestId = formDTO.ClusterContestId,
                    ClusterContestCode = formDTO.ClusterContestCode,
                    ClusterContestName = formDTO.ClusterContestName,
                    RegisterPlaceOfExamId = formDTO.RegisterPlaceOfExamId,
                    RegisterPlaceOfExamCode = formDTO.RegisterPlaceOfExamCode,
                    RegisterPlaceOfExamName = formDTO.RegisterPlaceOfExamName,
                    Biology = formDTO.Biology,
                    Chemistry = formDTO.Chemistry,
                    CivicEducation = formDTO.CivicEducation,
                    Geography = formDTO.Geography,
                    History = formDTO.History,
                    Languages = formDTO.Languages,
                    Literature = formDTO.Literature,
                    Maths = formDTO.Maths,
                    NaturalSciences = formDTO.NaturalSciences,
                    Physics = formDTO.Physics,
                    SocialSciences = formDTO.SocialSciences
                },
                GraduationInformation = new GraduationInformation
                {
                    Id = formDTO.Id ?? Guid.Empty,
                    ExceptLanguages = formDTO.ExceptLanguages,
                    Mark = formDTO.Mark,
                    ReserveBiology = formDTO.ReserveBiology,
                    ReserveChemistry = formDTO.ReserveChemistry,
                    ReserveCivicEducation = formDTO.ReserveCivicEducation,
                    ReserveGeography = formDTO.ReserveGeography,
                    ReserveHistory = formDTO.ReserveHistory,
                    ReserveLanguages = formDTO.ReserveLanguages,
                    ReserveLiterature = formDTO.ReserveLiterature,
                    ReserveMaths = formDTO.ReserveMaths,
                    ReservePhysics = formDTO.ReservePhysics
                },
                UniversityAdmission = new UniversityAdmission
                {
                    Id = formDTO.Id ?? Guid.Empty,
                    Area = formDTO.Area,
                    PriorityType = formDTO.PriorityType,
                    GraduateYear = formDTO.GraduateYear,
                    Connected = formDTO.Connected,
                    FormDetails = formDTO.FormDetails.Select(m => new FormDetail
                    {
                        Id = m.Id ?? Guid.Empty,
                        FormId = m.FormId,
                        MajorsId = m.MajorsId,
                        MajorsCode = m.MajorsCode,
                        MajorsName = m.MajorsName,
                        UniversityId = m.UniversityId,
                        UniversityCode = m.UniversityCode,
                        UniversityName = m.UniversityName,
                        UniversityAddress = m.UniversityAddress,
                        SubjectGroupId = m.SubjectGroupId,
                        SubjectGroupCode = m.SubjectGroupCode,
                        SubjectGroupName = m.SubjectGroupName,
                    }).ToList()
                }
            };
            return form;
        }
    }
}
