using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MAreaService;
using TwelveFinal.Services.MDistrict;
using TwelveFinal.Services.MEthnic;
using TwelveFinal.Services.MForm;
using TwelveFinal.Services.MGraduation;
using TwelveFinal.Services.MHighSchool;
using TwelveFinal.Services.MPersonal;
using TwelveFinal.Services.MPriorityType;
using TwelveFinal.Services.MProvince;
using TwelveFinal.Services.MRegister;
using TwelveFinal.Services.MTown;
using TwelveFinal.Services.MUniversity_Majors_Majors;
using TwelveFinal.Services.MUniversityAdmission;

namespace TwelveFinal.Controller.form
{
    public class FormRoute : Root
    {
        public const string Default = Base + "/form";
        public const string Save = Default + "/save";
        public const string Get = Default + "/get";
        public const string DropListProvince = Default + "/province";
        public const string DropListDistrict = Default + "/district";
        public const string DropListTown = Default + "/town";
        public const string DropListHighSchool = Default + "/high-school";
        public const string DropListEthnic = Default + "/drop-list-ethnic";
        public const string DropListPriorityType = Default + "/drop-list-priority-type";
        public const string DropListArea = Default + "/drop-list-area";
        public const string DropListUnivesity_Majors = Default + "/drop-list-university-majors";
        public const string Delete = Default + "/delete";
    }

    public class FormController : ApiController
    {
        private IFormService FormService;
        private IAreaService AreaService;
        private IProvinceService ProvinceService;
        private IDistrictService DistrictService;
        private ITownService TownService;
        private IHighSchoolService HighSchoolService;
        private IEthnicService EthnicService;
        private IPriorityTypeService PriorityTypeService;
        private IUniversity_MajorsService University_MajorsService;
        public FormController(
            IFormService formService,
            IAreaService areaService,
            IProvinceService provinceService,
            IDistrictService districtService,
            ITownService townService,
            IHighSchoolService highSchoolService,
            IEthnicService ethnicService,
            IPriorityTypeService priorityTypeService,
            IUniversity_MajorsService university_MajorsService
            )
        {
            FormService = formService;
            ProvinceService = provinceService;
            DistrictService = districtService;
            TownService = townService;
            HighSchoolService = highSchoolService;
            AreaService = areaService;
            EthnicService = ethnicService;
            PriorityTypeService = priorityTypeService;
            University_MajorsService = university_MajorsService;
        }

        #region Save
        [Route(FormRoute.Save), HttpPost]
        public async Task<ActionResult<FormDTO>> Save([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = await ConvertDTOtoBO(formDTO);

            form = await FormService.Save(form);

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
                }).ToList(),
                Errors = form.Errors
            };

            if (form.IsValidated)
            {
                return Ok(formDTO);
            }
            else
            {
                return BadRequest(formDTO);
            }
        }
        #endregion

        #region Get
        [Route(FormRoute.Get), HttpPost]
        public async Task<FormDTO> Get([FromBody] UserDTO userDTO)
        {
            if (userDTO == null) userDTO = new UserDTO();
            Form form = new Form { UserId = userDTO.Id };

            form = await FormService.Get(form.UserId);
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
                FormDetails = form.UniversityAdmission.FormDetails.Select(m => new FormDetailDTO
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

        #region DropListProvince
        [Route(FormRoute.DropListProvince), HttpPost]
        public async Task<List<ProvinceDTO>> ListProvince([FromBody] ProvinceFilterDTO provinceFilterDTO)
        {
            ProvinceFilter provinceFilter = new ProvinceFilter
            {
                Id = provinceFilterDTO.Id,
                Code = provinceFilterDTO.Code,
                Name = provinceFilterDTO.Name
            };

            var listProvince = await ProvinceService.List(provinceFilter);
            if (listProvince == null) return null;
            return listProvince.Select(p => new ProvinceDTO
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name
            }).ToList();
        }
        #endregion

        #region DropListDistrict
        [Route(FormRoute.DropListDistrict), HttpPost]
        public async Task<List<DistrictDTO>> ListDistrict([FromBody] DistrictFilterDTO districtFilterDTO)
        {
            DistrictFilter districtFilter = new DistrictFilter
            {
                Id = districtFilterDTO.Id,
                Code = districtFilterDTO.Code,
                Name = districtFilterDTO.Name,
                ProvinceId = districtFilterDTO.ProvinceId
            };

            var listDistrict = await DistrictService.List(districtFilter);
            if (listDistrict == null) return null;
            return listDistrict.Select(p => new DistrictDTO
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                ProvinceId = p.ProvinceId
            }).ToList();
        }
        #endregion

        #region DropListTown
        [Route(FormRoute.DropListTown), HttpPost]
        public async Task<List<TownDTO>> ListTown([FromBody] TownFilterDTO townFilterDTO)
        {
            TownFilter townFilter = new TownFilter
            {
                Id = townFilterDTO.Id,
                Code = townFilterDTO.Code,
                Name = townFilterDTO.Name,
                DistrictId = townFilterDTO.DistrictId
            };

            var listTown = await TownService.List(townFilter);
            if (listTown == null) return null;
            return listTown.Select(t => new TownDTO
            {
                Id = t.Id,
                Code = t.Code,
                Name = t.Name,
                DistrictId = t.DistrictId
            }).ToList();
        }
        #endregion

        #region DropListHighSchool
        [Route(FormRoute.DropListHighSchool), HttpPost]
        public async Task<List<HighSchoolDTO>> ListHighSchool([FromBody] HighSchoolFilterDTO highSchoolFilterDTO)
        {
            HighSchoolFilter highSchoolFilter = new HighSchoolFilter
            {
                Id = highSchoolFilterDTO.Id,
                Code = highSchoolFilterDTO.Code,
                Name = highSchoolFilterDTO.Name,
                DistrictId = highSchoolFilterDTO.DistrictId
            };

            var listHighSchool = await HighSchoolService.List(highSchoolFilter);
            if (listHighSchool == null) return null;
            return listHighSchool.Select(t => new HighSchoolDTO
            {
                Id = t.Id,
                Code = t.Code,
                Name = t.Name,
                DistrictId = t.DistrictId
            }).ToList();
        }
        #endregion

        #region DropListEthnic
        [Route(FormRoute.DropListEthnic), HttpPost]
        public async Task<List<EthnicDTO>> ListEthnic([FromBody] EthnicFilterDTO ethnicFilterDTO)
        {
            EthnicFilter ethnicFilter = new EthnicFilter
            {
                Id = ethnicFilterDTO.Id,
                Code = ethnicFilterDTO.Code,
                Name = ethnicFilterDTO.Name
            };

            var listEthnic = await EthnicService.List(ethnicFilter);
            if (listEthnic == null) return null;
            return listEthnic.Select(e => new EthnicDTO
            {
                Id = e.Id,
                Code = e.Code,
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
            }).OrderBy(p => p.Code).ToList();
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
