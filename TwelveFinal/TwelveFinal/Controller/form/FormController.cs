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
using TwelveFinal.Services.MHighSchool;
using TwelveFinal.Services.MPriorityType;
using TwelveFinal.Services.MProvince;
using TwelveFinal.Services.MTown;
using TwelveFinal.Services.MUniversity_Majors_Majors;

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
                UserId = form.UserId,
                
                FullName = form.FullName,
                Dob = form.Dob,
                Gender = form.Gender,
                Identify = form.Identify,
                PlaceOfBirth = form.PlaceOfBirth,
                TownId = form.TownId,
                TownCode = form.TownCode,
                TownName = form.TownName,
                DistrictCode = form.DistrictCode,
                DistrictName  = form.DistrictName,
                ProvinceCode = form.ProvinceCode,
                ProvinceName = form.ProvinceName,
                Address = form.Address,
                Phone = form.Phone,
                Email = form.Email,
                Ethnic = form.Ethnic,
                HighSchoolGrade10Id = form.HighSchoolGrade10Id,
                HighSchoolGrade10Code = form.HighSchoolGrade10Code,
                HighSchoolGrade10Name = form.HighSchoolGrade10Name,
                HighSchoolGrade11Id = form.HighSchoolGrade11Id,
                HighSchoolGrade11Code = form.HighSchoolGrade11Code,
                HighSchoolGrade11Name = form.HighSchoolGrade11Name,
                HighSchoolGrade12Id = form.HighSchoolGrade12Id,
                HighSchoolGrade12Code = form.HighSchoolGrade12Code,
                HighSchoolGrade12Name = form.HighSchoolGrade12Name,
                IsPermanentResidenceMore18 = form.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 =form.IsPermanentResidenceSpecialMore18,
                
                ResultForUniversity = form.ResultForUniversity,
                StudyAtHighSchool = form.StudyAtHighSchool,
                ClusterContestId = form.ClusterContestId,
                ClusterContestCode = form.ClusterContestCode,
                ClusterContestName = form.ClusterContestName,
                RegisterPlaceOfExamId = form.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = form.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = form.RegisterPlaceOfExamName,
                Biology = form.Biology,
                Chemistry = form.Chemistry,
                CivicEducation = form.CivicEducation,
                Geography =form.Geography,
                History = form.History,
                Languages = form.Languages,
                Literature = form.Literature,
                Maths = form.Maths,
                NaturalSciences = form.NaturalSciences,
                Physics = form.Physics,
                SocialSciences = form.SocialSciences,
                Graduated = form.Graduated,
                
                ExceptLanguages = form.ExceptLanguages,
                Mark = form.Mark,
                ReserveBiology = form.ReserveBiology,
                ReserveChemistry = form.ReserveChemistry,
                ReserveCivicEducation = form.ReserveCivicEducation,
                ReserveGeography = form.ReserveGeography,
                ReserveHistory = form.ReserveHistory,
                ReserveLanguages = form.ReserveLanguages,
                ReserveLiterature = form.ReserveLiterature,
                ReserveMaths = form.ReserveMaths,
                ReservePhysics = form.ReservePhysics,
                
                Area = form.Area,
                PriorityType =form.PriorityType,
                Aspirations = formDTO.Aspirations.Select(m => new AspirationDTO
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
        public async Task<FormDTO> Get([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = new Form { Id = formDTO.Id };

            form = await FormService.Get(form.Id);
            return new FormDTO
            {
                Id = form.Id,
                UserId = form.UserId,

                FullName = form.FullName,
                Dob = form.Dob,
                Gender = form.Gender,
                Identify = form.Identify,
                PlaceOfBirth = form.PlaceOfBirth,
                TownId = form.TownId,
                TownCode = form.TownCode,
                TownName = form.TownName,
                DistrictCode = form.DistrictCode,
                DistrictName = form.DistrictName,
                ProvinceCode = form.ProvinceCode,
                ProvinceName = form.ProvinceName,
                Address = form.Address,
                Phone = form.Phone,
                Email = form.Email,
                Ethnic = form.Ethnic,
                HighSchoolGrade10Id = form.HighSchoolGrade10Id,
                HighSchoolGrade10Code = form.HighSchoolGrade10Code,
                HighSchoolGrade10Name = form.HighSchoolGrade10Name,
                HighSchoolGrade11Id = form.HighSchoolGrade11Id,
                HighSchoolGrade11Code = form.HighSchoolGrade11Code,
                HighSchoolGrade11Name = form.HighSchoolGrade11Name,
                HighSchoolGrade12Id = form.HighSchoolGrade12Id,
                HighSchoolGrade12Code = form.HighSchoolGrade12Code,
                HighSchoolGrade12Name = form.HighSchoolGrade12Name,
                IsPermanentResidenceMore18 = form.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = form.IsPermanentResidenceSpecialMore18,

                ResultForUniversity = form.ResultForUniversity,
                StudyAtHighSchool = form.StudyAtHighSchool,
                ClusterContestId = form.ClusterContestId,
                ClusterContestCode = form.ClusterContestCode,
                ClusterContestName = form.ClusterContestName,
                RegisterPlaceOfExamId = form.RegisterPlaceOfExamId,
                RegisterPlaceOfExamCode = form.RegisterPlaceOfExamCode,
                RegisterPlaceOfExamName = form.RegisterPlaceOfExamName,
                Biology = form.Biology,
                Chemistry = form.Chemistry,
                CivicEducation = form.CivicEducation,
                Geography = form.Geography,
                History = form.History,
                Languages = form.Languages,
                Literature = form.Literature,
                Maths = form.Maths,
                NaturalSciences = form.NaturalSciences,
                Physics = form.Physics,
                SocialSciences = form.SocialSciences,
                Graduated = form.Graduated,

                ExceptLanguages = form.ExceptLanguages,
                Mark = form.Mark,
                ReserveBiology = form.ReserveBiology,
                ReserveChemistry = form.ReserveChemistry,
                ReserveCivicEducation = form.ReserveCivicEducation,
                ReserveGeography = form.ReserveGeography,
                ReserveHistory = form.ReserveHistory,
                ReserveLanguages = form.ReserveLanguages,
                ReserveLiterature = form.ReserveLiterature,
                ReserveMaths = form.ReserveMaths,
                ReservePhysics = form.ReservePhysics,

                Area = form.Area,
                PriorityType = form.PriorityType,
                Aspirations = form.Aspirations.Select(m => new AspirationDTO
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
                Id = formDTO.Id,
                UserId = formDTO.UserId,

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
                HighSchoolGrade11Id = formDTO.HighSchoolGrade11Id,
                HighSchoolGrade11Code = formDTO.HighSchoolGrade11Code,
                HighSchoolGrade11Name = formDTO.HighSchoolGrade11Name,
                HighSchoolGrade12Id = formDTO.HighSchoolGrade12Id,
                HighSchoolGrade12Code = formDTO.HighSchoolGrade12Code,
                HighSchoolGrade12Name = formDTO.HighSchoolGrade12Name,
                Phone = formDTO.Phone,
                Email = formDTO.Email,
                Address = formDTO.Address,

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
                SocialSciences = formDTO.SocialSciences,

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

                Area = formDTO.Area,
                PriorityType = formDTO.PriorityType,
                Aspirations = formDTO.Aspirations.Select(m => new Aspiration
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
            };
            return form;
        }
    }
}
