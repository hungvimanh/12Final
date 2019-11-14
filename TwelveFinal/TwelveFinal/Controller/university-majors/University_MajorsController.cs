using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Services.MUniversity_Majors_Majors;
using TwelveFinal.Services.MSubjectGroup;

namespace TwelveFinal.Controller.university_majors
{
    public class University_MajorsRoute : Root
    {
        public const string Default = Base + "/university-majors";
        public const string Create = Default + "/create";
        public const string Get = Default + "/get";
        public const string List = Default + "/list";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string DropListSubjectGroup = Default + "/drop-list-subject-group";
    }   
    public class University_MajorsController : ApiController
    {
        private IUniversity_MajorsService university_MajorsService;
        private ISubjectGroupService SubjectGrupService;
        public University_MajorsController(IUniversity_MajorsService university_MajorsService, ISubjectGroupService SubjectGrupService)
        {
            this.university_MajorsService = university_MajorsService;
            this.SubjectGrupService = SubjectGrupService;
        }

        [Route(University_MajorsRoute.Create), HttpPost]
        public async Task<ActionResult<University_MajorsDTO>> Create([FromBody] University_MajorsDTO university_MajorsDTO)
        {
            if (university_MajorsDTO == null) university_MajorsDTO = new University_MajorsDTO();

            University_Majors university_Majors = ConvertDTOtoBO(university_MajorsDTO);
            university_Majors = await university_MajorsService.Create(university_Majors);

            university_MajorsDTO = new University_MajorsDTO
            {
                MajorsId = university_Majors.MajorsId,
                MajorsCode = university_Majors.MajorsCode,
                MajorsName = university_Majors.MajorsName,
                Benchmark = university_Majors.Benchmark,
                UniversityId = university_Majors.UniversityId,
                UniversityCode = university_Majors.UniversityCode,
                UniversityName = university_Majors.UniversityName,
                UniversityAddress = university_Majors.UniversityAddress,
                SubjectGroupId = university_Majors.SubjectGroupId,
                SubjectGroupCode = university_Majors.SubjectGroupCode,
                SubjectGroupName = university_Majors.SubjectGroupName,
                Year = university_Majors.Year,
                Quantity = university_MajorsDTO.Quantity,
                Descreption = university_Majors.Descreption,
                Errors = university_Majors.Errors
            };
            if (university_Majors.HasError)
                return BadRequest(university_MajorsDTO);
            return Ok(university_MajorsDTO);
        }

        [Route(University_MajorsRoute.Update), HttpPost]
        public async Task<ActionResult<University_MajorsDTO>> Update([FromBody] University_MajorsDTO university_MajorsDTO)
        {
            if (university_MajorsDTO == null) university_MajorsDTO = new University_MajorsDTO();

            University_Majors university_Majors = ConvertDTOtoBO(university_MajorsDTO);
            university_Majors = await university_MajorsService.Update(university_Majors);

            university_MajorsDTO = new University_MajorsDTO
            {
                MajorsId = university_Majors.MajorsId,
                MajorsCode = university_Majors.MajorsCode,
                MajorsName = university_Majors.MajorsName,
                Benchmark = university_Majors.Benchmark,
                UniversityId = university_Majors.UniversityId,
                UniversityCode = university_Majors.UniversityCode,
                UniversityName = university_Majors.UniversityName,
                UniversityAddress = university_Majors.UniversityAddress,
                SubjectGroupId = university_Majors.SubjectGroupId,
                SubjectGroupCode = university_Majors.SubjectGroupCode,
                SubjectGroupName = university_Majors.SubjectGroupName,
                Year = university_Majors.Year,
                Quantity = university_MajorsDTO.Quantity,
                Descreption = university_Majors.Descreption,
                Errors = university_Majors.Errors
            };
            if (university_Majors.HasError)
                return BadRequest(university_MajorsDTO);
            return Ok(university_MajorsDTO);
        }

        [Route(University_MajorsRoute.Get), HttpPost]
        public async Task<University_MajorsDTO> Get([FromBody] University_MajorsDTO university_MajorsDTO)
        {
            if (university_MajorsDTO == null) university_MajorsDTO = new University_MajorsDTO();
            if (university_MajorsDTO.UniversityId == null) return null;

            University_Majors university_Majors = ConvertDTOtoBO(university_MajorsDTO);
            university_Majors = await university_MajorsService.Update(university_Majors);

            return university_Majors == null ? null : new University_MajorsDTO()
            {
                MajorsId = university_Majors.MajorsId,
                MajorsCode = university_Majors.MajorsCode,
                MajorsName = university_Majors.MajorsName,
                Benchmark = university_Majors.Benchmark,
                UniversityId = university_Majors.UniversityId,
                UniversityCode = university_Majors.UniversityCode,
                UniversityName = university_Majors.UniversityName,
                UniversityAddress = university_Majors.UniversityAddress,
                SubjectGroupId = university_Majors.SubjectGroupId,
                SubjectGroupCode = university_Majors.SubjectGroupCode,
                SubjectGroupName = university_Majors.SubjectGroupName,
                Year = university_Majors.Year,
                Quantity = university_MajorsDTO.Quantity,
                Descreption = university_Majors.Descreption,
                Errors = university_Majors.Errors
            };
        }

        [Route(University_MajorsRoute.List), HttpPost]
        public async Task<List<University_MajorsDTO>> List([FromBody] University_MajorsFilterDTO university_MajorsFilterDTO)
        {
            University_MajorsFilter university_MajorsFilter = new University_MajorsFilter
            {
                UniversityId = university_MajorsFilterDTO.UniversityId,
                UniversityCode = new StringFilter { StartsWith = university_MajorsFilterDTO.UniversityCode },
                UniversityName = new StringFilter { Contains = university_MajorsFilterDTO.UniversityName },
                MajorsId = university_MajorsFilterDTO.MajorsId,
                MajorsCode = new StringFilter { StartsWith = university_MajorsFilterDTO.MajorsCode },
                MajorsName = new StringFilter { Contains = university_MajorsFilterDTO.MajorsName },
                SubjectGroupId = university_MajorsFilterDTO.SubjectGroupId,
                SubjectGroupCode = new StringFilter { StartsWith = university_MajorsFilterDTO.SubjectGroupCode },
                BenchmarkHigh = new DoubleFilter { LessEqual = university_MajorsFilterDTO.BenchmarkHigh },
                BenchmarkLow = new DoubleFilter { GreaterEqual = university_MajorsFilterDTO.BenchmarkLow },
                Year = university_MajorsFilterDTO.Year,
                Skip = university_MajorsFilterDTO.Skip,
                Take = university_MajorsFilterDTO.Take,
                OrderBy = university_MajorsFilterDTO.OrderBy,
                OrderType = OrderType.DESC
            };

            List<University_Majors> universities = await university_MajorsService.List(university_MajorsFilter);

            List<University_MajorsDTO> university_MajorsDTOs = universities.Select(u => new University_MajorsDTO
            {                                                                                       
                MajorsId = u.MajorsId,
                MajorsCode = u.MajorsCode,
                MajorsName = u.MajorsName,
                Benchmark = u.Benchmark,
                UniversityId = u.UniversityId,
                UniversityCode = u.UniversityCode,
                UniversityName = u.UniversityName,
                UniversityAddress = u.UniversityAddress,
                SubjectGroupId = u.SubjectGroupId,
                SubjectGroupCode = u.SubjectGroupCode,
                SubjectGroupName = u.SubjectGroupName,
                Year = u.Year,
                Quantity = u.Quantity,
                Descreption = u.Descreption
            }).ToList();

            return university_MajorsDTOs;
        }

        [Route(University_MajorsRoute.Delete), HttpPost]
        public async Task<ActionResult<University_MajorsDTO>> Delete([FromBody] University_MajorsDTO university_MajorsDTO)
        {
            if (university_MajorsDTO == null) university_MajorsDTO = new University_MajorsDTO();

            University_Majors university_Majors = ConvertDTOtoBO(university_MajorsDTO);
            university_Majors = await university_MajorsService.Delete(university_Majors);

            university_MajorsDTO = new University_MajorsDTO
            {
                MajorsId = university_Majors.MajorsId,
                MajorsCode = university_Majors.MajorsCode,
                MajorsName = university_Majors.MajorsName,
                Benchmark = university_Majors.Benchmark,
                UniversityId = university_Majors.UniversityId,
                UniversityCode = university_Majors.UniversityCode,
                UniversityName = university_Majors.UniversityName,
                UniversityAddress = university_Majors.UniversityAddress,
                SubjectGroupId = university_Majors.SubjectGroupId,
                SubjectGroupCode = university_Majors.SubjectGroupCode,
                SubjectGroupName = university_Majors.SubjectGroupName,
                Year = university_Majors.Year,
                Quantity = university_MajorsDTO.Quantity,
                Descreption = university_Majors.Descreption,
                Errors = university_Majors.Errors
            };
            if (university_Majors.HasError)
                return BadRequest(university_MajorsDTO);
            return Ok(university_MajorsDTO);
        }

        [Route(University_MajorsRoute.DropListSubjectGroup), HttpPost]
        public async Task<List<SubjectGroupDTO>> DropListSubjectGroup([FromBody] SubjectGroupFilterDTO subjectGroupFilterDTO)
        {
            SubjectGroupFilter filter = new SubjectGroupFilter
            {
                Code = new StringFilter { StartsWith = subjectGroupFilterDTO.Code },
                Skip = subjectGroupFilterDTO.Skip,
                Take = subjectGroupFilterDTO.Take
            };

            List<SubjectGroup> subjectGroups = await SubjectGrupService.List(filter);

            List<SubjectGroupDTO> subjectGroupDTOs = subjectGroups.Select(s => new SubjectGroupDTO
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name
            }).ToList();

            return subjectGroupDTOs;
        }

        private University_Majors ConvertDTOtoBO(University_MajorsDTO university_MajorsDTO)
        {
            University_Majors University_Majors = new University_Majors
            {
                MajorsId = university_MajorsDTO.MajorsId,
                MajorsCode = university_MajorsDTO.MajorsCode,
                MajorsName = university_MajorsDTO.MajorsName,
                Benchmark = university_MajorsDTO.Benchmark,
                UniversityId = university_MajorsDTO.UniversityId,
                UniversityCode = university_MajorsDTO.UniversityCode,
                UniversityName = university_MajorsDTO.UniversityName,
                UniversityAddress = university_MajorsDTO.UniversityAddress,
                SubjectGroupId = university_MajorsDTO.SubjectGroupId,
                SubjectGroupCode = university_MajorsDTO.SubjectGroupCode,
                SubjectGroupName = university_MajorsDTO.SubjectGroupName,
                Year = university_MajorsDTO.Year,
                Quantity = university_MajorsDTO.Quantity,
                Descreption = university_MajorsDTO.Descreption
            };
            return University_Majors;
        }
    }
}
