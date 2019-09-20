using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MAreaService;
using TwelveFinal.Services.MEthnic;
using TwelveFinal.Services.MForm;
using TwelveFinal.Services.MGraduation;
using TwelveFinal.Services.MPersonal;
using TwelveFinal.Services.MPriorityType;
using TwelveFinal.Services.MRegister;
using TwelveFinal.Services.MUniversity_Majors_Majors;
using TwelveFinal.Services.MUniversityAdmission;

namespace TwelveFinal.Controller.form
{
    public class FormRoute
    {
        public const string Default = "api/TF/form";
        public const string Create = Default + "/create";
        public const string Get = Default + "/get";
        public const string CheckPersonal = Default + "/check-personal";
        public const string CheckRegister = Default + "/check-register";
        public const string CheckGraduate = Default + "/check-graduate";
        public const string CheckUniversity = Default + "/check-university";
        public const string DropListEthnic = Default + "/drop-list-ethnic";
        public const string DropListPriorityType = Default + "/drop-list-priority-type";
        public const string DropListArea = Default + "/drop-list-area";
        public const string DropListUnivesity_Majors = Default + "/drop-list-university-majors";
        public const string Delete = Default + "/delete";
    }

    public class FormController : ControllerBase
    {
        private IFormService FormService;
        private IAreaService AreaService;
        private IEthnicService EthnicService;
        private IPersonalInformationService PeronalInformationService;
        private IPriorityTypeService PriorityTypeService;
        private IRegisterInformationService RegisterInformationService;
        private IGraduationInformationService GraduationInformationService;
        private IUniversity_MajorsService University_MajorsService;
        private IUniversityAdmissionService UniversityAdmissionService;
        public FormController(
            IFormService formService,
            IAreaService areaService,
            IEthnicService ethnicService,
            IPersonalInformationService peronalInformationService,
            IPriorityTypeService priorityTypeService,
            IRegisterInformationService registerInformationService,
            IGraduationInformationService graduationInformationService,
            IUniversity_MajorsService university_MajorsService,
            IUniversityAdmissionService universityAdmissionService
            )
        {
            FormService = formService;
            AreaService = areaService;
            EthnicService = ethnicService;
            PeronalInformationService = peronalInformationService;
            PriorityTypeService = priorityTypeService;
            RegisterInformationService = registerInformationService;
            GraduationInformationService = graduationInformationService;
            University_MajorsService = university_MajorsService;
            UniversityAdmissionService = universityAdmissionService;
        }

        #region Create
        [Route(FormRoute.Create), HttpPost]
        public async Task<FormDTO> Create([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = await ConvertDTOtoBO(formDTO);

            form = await FormService.Create(form);

            formDTO = new FormDTO
            {
                Id = form.Id,
                NumberForm = form.PersonalInformation.NumberForm,
                DepartmentCode = form.PersonalInformation.DepartmentCode,
                Date = form.PersonalInformation.Date,
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
        #endregion

        #region Get
        [Route(FormRoute.Get), HttpPost]
        public async Task<FormDTO> Get([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = await ConvertDTOtoBO(formDTO);

            form = await FormService.Get(form.Id);
            return new FormDTO
            {
                Id = form.Id,
                NumberForm = form.PersonalInformation.NumberForm,
                DepartmentCode = form.PersonalInformation.DepartmentCode,
                Date = form.PersonalInformation.Date,
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
        #endregion

        #region Personal
        [Route(FormRoute.CheckPersonal), HttpPost]
        public async Task<PersonalInformationDTO> UpdatePersonal([FromBody] PersonalInformationDTO personalInformationDTO)
        {
            if (personalInformationDTO == null) personalInformationDTO = new PersonalInformationDTO();
            
            PersonalInformation personal = new PersonalInformation
            {
                Id = personalInformationDTO.Id,
                DepartmentCode = personalInformationDTO.DepartmentCode,
                NumberForm = personalInformationDTO.NumberForm,
                Date = personalInformationDTO.Date,

                FullName = personalInformationDTO.FullName,
                Dob = personalInformationDTO.Dob,
                Gender = personalInformationDTO.Gender,
                Identify = personalInformationDTO.Identify,
                PlaceOfBirth = personalInformationDTO.PlaceOfBirth,
                TownId = personalInformationDTO.TownId,
                TownCode = personalInformationDTO.TownCode,
                TownName = personalInformationDTO.TownName,
                DistrictCode = personalInformationDTO.DistrictCode,
                DistrictName = personalInformationDTO.DistrictName,
                ProvinceCode = personalInformationDTO.ProvinceCode,
                ProvinceName = personalInformationDTO.ProvinceName,
                Address = personalInformationDTO.Address,
                Phone = personalInformationDTO.Phone,
                Email = personalInformationDTO.Email,
                Ethnic = personalInformationDTO.Ethnic,
                Grade12Name = personalInformationDTO.Grade12Name,
                HighSchoolGrade10Id = personalInformationDTO.HighSchoolGrade10Id,
                HighSchoolGrade10Code = personalInformationDTO.HighSchoolGrade10Code,
                HighSchoolGrade10Name = personalInformationDTO.HighSchoolGrade10Name,
                HighSchoolGrade10DistrictCode = personalInformationDTO.HighSchoolGrade10DistrictCode,
                HighSchoolGrade10DistrictName = personalInformationDTO.HighSchoolGrade10DistrictName,
                HighSchoolGrade10ProvinceCode = personalInformationDTO.HighSchoolGrade10ProvinceCode,
                HighSchoolGrade10ProvinceName = personalInformationDTO.HighSchoolGrade10ProvinceName,
                HighSchoolGrade11Id = personalInformationDTO.HighSchoolGrade11Id,
                HighSchoolGrade11Code = personalInformationDTO.HighSchoolGrade11Code,
                HighSchoolGrade11Name = personalInformationDTO.HighSchoolGrade11Name,
                HighSchoolGrade11DistrictCode = personalInformationDTO.HighSchoolGrade11DistrictCode,
                HighSchoolGrade11DistrictName = personalInformationDTO.HighSchoolGrade11DistrictName,
                HighSchoolGrade11ProvinceCode = personalInformationDTO.HighSchoolGrade11ProvinceCode,
                HighSchoolGrade11ProvinceName = personalInformationDTO.HighSchoolGrade11ProvinceName,
                HighSchoolGrade12Id = personalInformationDTO.HighSchoolGrade12Id,
                HighSchoolGrade12Code = personalInformationDTO.HighSchoolGrade12Code,
                HighSchoolGrade12Name = personalInformationDTO.HighSchoolGrade12Name,
                HighSchoolGrade12DistrictCode = personalInformationDTO.HighSchoolGrade12DistrictCode,
                HighSchoolGrade12DistrictName = personalInformationDTO.HighSchoolGrade12DistrictName,
                HighSchoolGrade12ProvinceCode = personalInformationDTO.HighSchoolGrade12ProvinceCode,
                HighSchoolGrade12ProvinceName = personalInformationDTO.HighSchoolGrade12ProvinceName,
                IsPermanentResidenceMore18 = personalInformationDTO.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = personalInformationDTO.IsPermanentResidenceSpecialMore18,
            };

            personal = await PeronalInformationService.Check(personal);

            personalInformationDTO = new PersonalInformationDTO
            {
                Id = personal.Id,
                NumberForm = personal.NumberForm,
                DepartmentCode = personal.DepartmentCode,
                Date = personal.Date,
                
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
                IsPermanentResidenceSpecialMore18 = personal.IsPermanentResidenceSpecialMore18
            };

            if (personal.IsValidated)
            {
                return personalInformationDTO;
            }
            else
            {
                throw new BadRequestException(personalInformationDTO);
            }
        }
        #endregion

        #region Register
        [Route(FormRoute.CheckRegister), HttpPost]
        public async Task<RegisterInformationDTO> UpdateRegister([FromBody] RegisterInformationDTO registerInformationDTO)
        {
            if (registerInformationDTO == null) registerInformationDTO = new RegisterInformationDTO();
            RegisterInformation register = new RegisterInformation
            {
                Id = registerInformationDTO.Id,
                ResultForUniversity = registerInformationDTO.ResultForUniversity,
                StudyAtHighSchool = registerInformationDTO.StudyAtHighSchool,
                ClusterContestId = registerInformationDTO.ClusterContestId,
                ClusterContestCode = registerInformationDTO.ClusterContestCode,
                ClusterContestName = registerInformationDTO.ClusterContestName,
                RegisterPlaceOfExamId = registerInformationDTO.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = registerInformationDTO.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = registerInformationDTO.RegisterPlaceOfExamName,
                Biology = registerInformationDTO.Biology,
                Chemistry = registerInformationDTO.Chemistry,
                CivicEducation = registerInformationDTO.CivicEducation,
                Geography = registerInformationDTO.Geography,
                History = registerInformationDTO.History,
                Languages = registerInformationDTO.Languages,
                Literature = registerInformationDTO.Literature,
                Maths = registerInformationDTO.Maths,
                NaturalSciences = registerInformationDTO.NaturalSciences,
                Physics = registerInformationDTO.Physics,
                SocialSciences = registerInformationDTO.SocialSciences,
                Graduated = registerInformationDTO.Graduated,
                
            };

            register = await RegisterInformationService.Check(register);

            registerInformationDTO = new RegisterInformationDTO
            {
                Id = register.Id,
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
                Graduated = register.Graduated
            };

            if (register.IsValidated)
            {
                return registerInformationDTO;
            }
            else
            {
                throw new BadRequestException(registerInformationDTO);
            }
        }
        #endregion

        #region Graduation
        [Route(FormRoute.CheckGraduate), HttpPost]
        public async Task<GraduationInformationDTO> UpdateGraduate([FromBody] GraduationInformationDTO graduationInformationDTO)
        {
            if (graduationInformationDTO == null) graduationInformationDTO = new GraduationInformationDTO();
            GraduationInformation graduation = new GraduationInformation
            {
                Id = graduationInformationDTO.Id,
                ExceptLanguages = graduationInformationDTO.ExceptLanguages,
                Mark = graduationInformationDTO.Mark,
                ReserveBiology = graduationInformationDTO.ReserveBiology,
                ReserveChemistry = graduationInformationDTO.ReserveChemistry,
                ReserveCivicEducation = graduationInformationDTO.ReserveCivicEducation,
                ReserveGeography = graduationInformationDTO.ReserveGeography,
                ReserveHistory = graduationInformationDTO.ReserveHistory,
                ReserveLanguages = graduationInformationDTO.ReserveLanguages,
                ReserveLiterature = graduationInformationDTO.ReserveLiterature,
                ReserveMaths = graduationInformationDTO.ReserveMaths,
                ReservePhysics = graduationInformationDTO.ReservePhysics,
            };

            graduation = await GraduationInformationService.Check(graduation);

            graduationInformationDTO = new GraduationInformationDTO
            {
                Id = graduation.Id,
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
                ReservePhysics = graduation.ReservePhysics
            };

            if (graduation.IsValidated)
            {
                return graduationInformationDTO;
            }
            else
            {
                throw new BadRequestException(graduationInformationDTO);
            }
        }
        #endregion

        #region University
        [Route(FormRoute.CheckUniversity), HttpPost]
        public async Task<UniversityAdmissionDTO> UpdateUniversity([FromBody] UniversityAdmissionDTO universityAdmissionDTO)
        {
            if (universityAdmissionDTO == null) universityAdmissionDTO = new UniversityAdmissionDTO();
            UniversityAdmission university = new UniversityAdmission
            {
                Id = universityAdmissionDTO.Id,
                Area = universityAdmissionDTO.Area,
                Connected = universityAdmissionDTO.Connected,
                GraduateYear = universityAdmissionDTO.GraduateYear,
                PriorityType = universityAdmissionDTO.PriorityType,
                FormDetails = universityAdmissionDTO.FormDetailDTOs.Select(m => new FormDetail
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

            university = await UniversityAdmissionService.Check(university);

            universityAdmissionDTO = new UniversityAdmissionDTO
            {
                Id = university.Id,
                Area = universityAdmissionDTO.Area,
                Connected = universityAdmissionDTO.Connected,
                GraduateYear = universityAdmissionDTO.GraduateYear,
                PriorityType = universityAdmissionDTO.PriorityType,
                FormDetailDTOs = universityAdmissionDTO.FormDetailDTOs.Select(m => new FormDetailDTO
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

            if (university.IsValidated)
            {
                return universityAdmissionDTO;
            }
            else
            {
                throw new BadRequestException(universityAdmissionDTO);
            }
        }
        #endregion

        #region DropListEthnic
        [Route(FormRoute.DropListEthnic), HttpPost]
        public async Task<List<EthnicDTO>> ListEthnic([FromBody] EthnicFilterDTO ethnicFilterDTO)
        {
            EthnicFilter ethnicFilter = new EthnicFilter
            {
                Id = ethnicFilterDTO.Id,
                Name = ethnicFilterDTO.Name
            };

            var listEthnic = await EthnicService.List(ethnicFilter);
            if (listEthnic == null) return null;
            return listEthnic.Select(e => new EthnicDTO
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
        }
        #endregion

        #region DropListPriorityType
        [Route(FormRoute.DropListPriorityType), HttpPost]
        public async Task<List<PriorityTypeDTO>> ListPriorityType([FromBody] PriorityTypeFilterDTO priorityTypeFilterDTO)
        {
            PriorityTypeFilter filter = new PriorityTypeFilter
            {
                Id = priorityTypeFilterDTO.Id,
                Code = priorityTypeFilterDTO.Code
            };

            var listPriorityType = await PriorityTypeService.List(filter);
            if (listPriorityType == null) return null;
            return listPriorityType.Select(p => new PriorityTypeDTO
            {
                Id = p.Id,
                Code = p.Code
            }).ToList();
        }
        #endregion

        #region DropListArea
        [Route(FormRoute.DropListArea), HttpPost]
        public async Task<List<AreaDTO>> ListArea([FromBody] AreaFilterDTO areaFilterDTO)
        {
            AreaFilter filter = new AreaFilter
            {
                Id = areaFilterDTO.Id,
                Code = areaFilterDTO.Code,
                Name = areaFilterDTO.Name
            };

            var listArea = await AreaService.List(filter);
            if (listArea == null) return null;
            return listArea.Select(a => new AreaDTO
            {
                Id = a.Id,
                Code = a.Code,
                Name = a.Name
            }).ToList();
        }
        #endregion

        #region DropListUnivesity_Majors
        [Route(FormRoute.DropListUnivesity_Majors), HttpPost]
        public async Task<List<University_MajorsDTO>> ListUnivesity_Majors([FromBody] University_MajorsFilterDTO university_majorsFilterDTO)
        {
            University_MajorsFilter filter = new University_MajorsFilter
            {
                UniversityId = university_majorsFilterDTO.UniversityId,
                UniversityCode = university_majorsFilterDTO.UniversityCode,
                MajorsId = university_majorsFilterDTO.MajorsId,
                MajorsCode = university_majorsFilterDTO.MajorsCode,
                SubjectGroupId = university_majorsFilterDTO.SubjectGroupId,
                SubjectGroupCode = university_majorsFilterDTO.SubjectGroupCode,
                Year = university_majorsFilterDTO.Year
            };

            var listMajors = await University_MajorsService.List(filter);
            if (listMajors == null) return null;
            return listMajors.Select(m => new University_MajorsDTO
            {
                UniversityId = m.UniversityId,
                UniversityCode = m.UniversityCode,
                UniversityName = m.UniversityName,
                UniversityAddress = m.UniversityAddress,
                MajorsId = m.MajorsId,
                MajorsCode = m.MajorsCode,
                MajorsName = m.MajorsName,
                SubjectGroupId = m.SubjectGroupId,
                SubjectGroupCode = m.SubjectGroupCode,
                SubjectGroupName = m.SubjectGroupName
            }).ToList();
        }
        #endregion

        #region Delete
        [Route(FormRoute.Delete), HttpPost]
        public async Task<FormDTO> Delete([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();

            Form form = await ConvertDTOtoBO(formDTO);

            form = await FormService.Delete(form);

            formDTO = new FormDTO
            {
                Id = form.Id,
                NumberForm = form.PersonalInformation.NumberForm,
                DepartmentCode = form.PersonalInformation.DepartmentCode,
                Date = form.PersonalInformation.Date,
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
        #endregion

        private async Task<Form> ConvertDTOtoBO(FormDTO formDTO)
        {
            Form form = new Form
            {
                Id = formDTO.Id ?? Guid.Empty,
                UserId = formDTO.UserId,

                PersonalInformation = new PersonalInformation
                {
                    Date = formDTO.Date,
                    DepartmentCode = formDTO.DepartmentCode,
                    NumberForm = formDTO.NumberForm,

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
