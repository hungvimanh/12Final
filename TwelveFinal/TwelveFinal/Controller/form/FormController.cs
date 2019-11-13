using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MDistrict;
using TwelveFinal.Services.MEthnic;
using TwelveFinal.Services.MForm;
using TwelveFinal.Services.MHighSchool;
using TwelveFinal.Services.MProvince;
using TwelveFinal.Services.MStudentService;
using TwelveFinal.Services.MTown;
using TwelveFinal.Services.MUniversity_Majors_Majors;

namespace TwelveFinal.Controller.form
{
    public class FormRoute : Root
    {
        public const string Default = Base + "/form";
        public const string Save = Default + "/save";
        public const string Get = Default + "/get";
        public const string ApproveAccept = Default + "/accept";
        public const string ApproveDeny = Default + "/deny";
        public const string DropListProvince = Default + "/province";
        public const string DropListDistrict = Default + "/district";
        public const string DropListTown = Default + "/town";
        public const string DropListHighSchool = Default + "/high-school";
        public const string DropListEthnic = Default + "/drop-list-ethnic";
        public const string DropListUnivesity_Majors = Default + "/drop-list-university-majors";
        public const string Delete = Default + "/delete";
    }

    public class FormController : ApiController
    {
        private IFormService FormService;
        private IProvinceService ProvinceService;
        private IDistrictService DistrictService;
        private ITownService TownService;
        private IHighSchoolService HighSchoolService;
        private IEthnicService EthnicService;
        private IStudentService StudentService;
        private IUniversity_MajorsService University_MajorsService;
        public FormController(
            IFormService formService,
            IProvinceService provinceService,
            IDistrictService districtService,
            ITownService townService,
            IHighSchoolService highSchoolService,
            IEthnicService ethnicService,
            IStudentService studentService,
            IUniversity_MajorsService university_MajorsService
            )
        {
            FormService = formService;
            ProvinceService = provinceService;
            DistrictService = districtService;
            TownService = townService;
            HighSchoolService = highSchoolService;
            EthnicService = ethnicService;
            StudentService = studentService;
            University_MajorsService = university_MajorsService;
        }

        #region Save
        [Route(FormRoute.Save), HttpPost]
        public async Task<ActionResult<FormDTO>> Save([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = await ConvertDTOtoBO(formDTO);
            Student student = new Student
            {
                Id = formDTO.StudentDTO.Id,
                Dob = formDTO.StudentDTO.Dob,
                Name = formDTO.StudentDTO.Name,
                Gender = formDTO.StudentDTO.Gender,
                Identify = formDTO.StudentDTO.Identify,
                Phone = formDTO.StudentDTO.Phone,
                Address = formDTO.StudentDTO.Address,
                EthnicId = formDTO.StudentDTO.EthnicId,
                HighSchoolId = formDTO.StudentDTO.HighSchoolId,
                PlaceOfBirth = formDTO.StudentDTO.PlaceOfBirth,
                ProvinceId = formDTO.StudentDTO.ProvinceId,
                DistrictId = formDTO.StudentDTO.DistrictId,
                TownId = formDTO.StudentDTO.TownId,
            };
            student = await StudentService.Update(student);
            form = await FormService.Save(form);

            formDTO = new FormDTO
            {
                Id = form.Id,
                StudentId = form.StudentId,
                
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
                PriorityType = form.PriorityType,
                Status = form.Status,
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
                }).ToList(),
                Errors = form.Errors
            };

            if (form.HasError)
            {
                return BadRequest(formDTO);
            }
            return Ok(formDTO);
        }
        #endregion

        #region Get
        [Route(FormRoute.Get), HttpPost]
        public async Task<FormDTO> Get([FromBody] StudentDTO studentDTO)
        {
            if (studentDTO == null) studentDTO = new StudentDTO();
            Student student = new Student { Id = studentDTO.Id };
            student = await StudentService.Get(student.Id);

            Form form = new Form { StudentId = student.Id };

            form = await FormService.Get(form.StudentId);
            return new FormDTO
            {
                Id = form.Id,
                StudentId = form.StudentId,
                StudentDTO = new StudentDTO
                {
                    Id = student.Id,
                    Address = student.Address,
                    Dob = student.Dob,
                    Gender = student.Gender,
                    Email = student.Email,
                    Identify = student.Identify,
                    PlaceOfBirth = student.PlaceOfBirth,
                    Name = student.Name,
                    Phone = student.Phone,
                    EthnicId = student.EthnicId,
                    EthnicName = student.EthnicName,
                    EthnicCode = student.EthnicCode,
                    HighSchoolId = student.HighSchoolId,
                    HighSchoolName = student.HighSchoolName,
                    HighSchoolCode = student.HighSchoolCode,
                    TownId = student.TownId,
                    TownCode = student.TownCode,
                    TownName = student.TownName,
                    DistrictId = student.DistrictId,
                    DistrictCode = student.DistrictCode,
                    DistrictName = student.DistrictName,
                    ProvinceId = student.ProvinceId,
                    ProvinceCode = student.ProvinceCode,
                    ProvinceName = student.ProvinceName
                },
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
                Status = form.Status,
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

        #region Accept
        [Route(FormRoute.ApproveAccept), HttpPost]
        public async Task<ActionResult<bool>> ApproveAccept([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = new Form { Id = formDTO.Id };
            form = await FormService.ApproveAccept(form);
            if (form.Errors != null && form.Errors.Count > 0)
            {
                return BadRequest(form.Errors);
            }

            return Ok(true);
        }
        #endregion

        #region Deny
        [Route(FormRoute.ApproveDeny), HttpPost]
        public async Task<ActionResult<bool>> ApproveDeny([FromBody] FormDTO formDTO)
        {
            if (formDTO == null) formDTO = new FormDTO();
            Form form = new Form { Id = formDTO.Id };
            form = await FormService.ApproveDeny(form);
            if (form.Errors != null && form.Errors.Count > 0)
            {
                return BadRequest(form.Errors);
            }

            return Ok(true);
        }
        #endregion

        #region DropListProvince
        [Route(FormRoute.DropListProvince), HttpPost]
        public async Task<List<ProvinceDTO>> ListProvince([FromBody] ProvinceFilterDTO provinceFilterDTO)
        {
            ProvinceFilter provinceFilter = new ProvinceFilter
            {
                Id = new GuidFilter { Equal = provinceFilterDTO.Id },
                Code = new StringFilter { StartsWith = provinceFilterDTO.Code },
                Name = new StringFilter { StartsWith = provinceFilterDTO.Name }
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
                Id = new GuidFilter { Equal = districtFilterDTO.Id },
                Code = new StringFilter { StartsWith = districtFilterDTO.Code },
                Name = new StringFilter { StartsWith = districtFilterDTO.Name },
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
                Id = new GuidFilter { Equal = townFilterDTO.Id },
                Code = new StringFilter { StartsWith = townFilterDTO.Code },
                Name = new StringFilter { StartsWith = townFilterDTO.Name },
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
                Id = new GuidFilter { Equal = highSchoolFilterDTO.Id },
                Code =new StringFilter { StartsWith = highSchoolFilterDTO.Code },
                Name =new StringFilter { Contains = highSchoolFilterDTO.Name },
                ProvinceId = highSchoolFilterDTO.ProvinceId
            };

            var listHighSchool = await HighSchoolService.List(highSchoolFilter);
            if (listHighSchool == null) return null;
            return listHighSchool.Select(t => new HighSchoolDTO
            {
                Id = t.Id,
                Code = t.Code,
                Name = t.Name,
                ProvinceId = t.ProvinceId
            }).ToList();
        }
        #endregion

        #region DropListEthnic
        [Route(FormRoute.DropListEthnic), HttpPost]
        public async Task<List<EthnicDTO>> ListEthnic([FromBody] EthnicFilterDTO ethnicFilterDTO)
        {
            EthnicFilter ethnicFilter = new EthnicFilter
            {
                Id = new GuidFilter { Equal = ethnicFilterDTO.Id },
                Code =new StringFilter { StartsWith = ethnicFilterDTO.Code },
                Name =new StringFilter { StartsWith = ethnicFilterDTO.Name }
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

        #region DropListUnivesity_Majors
        [Route(FormRoute.DropListUnivesity_Majors), HttpPost]
        public async Task<List<University_MajorsDTO>> ListUnivesity_Majors([FromBody] University_MajorsFilterDTO university_majorsFilterDTO)
        {
            University_MajorsFilter filter = new University_MajorsFilter
            {
                UniversityId = university_majorsFilterDTO.UniversityId,
                UniversityCode = new StringFilter { StartsWith = university_majorsFilterDTO.UniversityCode },
                UniversityName = new StringFilter { Contains = university_majorsFilterDTO.UniversityName },
                MajorsId = university_majorsFilterDTO.MajorsId ,
                MajorsCode = new StringFilter { StartsWith = university_majorsFilterDTO.MajorsCode },
                MajorsName = new StringFilter { Contains = university_majorsFilterDTO.MajorsName },
                SubjectGroupId = university_majorsFilterDTO.SubjectGroupId,
                SubjectGroupCode = new StringFilter { StartsWith = university_majorsFilterDTO.SubjectGroupCode },
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
                StudentId = formDTO.StudentId,

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
