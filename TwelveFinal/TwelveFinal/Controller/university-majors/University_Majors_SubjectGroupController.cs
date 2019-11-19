using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Services.MUniversity_Majors;
using TwelveFinal.Entities;

namespace TwelveFinal.Controller.university_majors
{
    public class University_Majors_SubjectGroupController : ApiController
    {
        private IUniversity_Majors_SubjectGroupService University_Majors_SubjectGroupService;
        public University_Majors_SubjectGroupController(IUniversity_Majors_SubjectGroupService University_Majors_SubjectGroupService)
        {
            this.University_Majors_SubjectGroupService = University_Majors_SubjectGroupService;
        }

        #region Read
        [AllowAnonymous]
        [Route(CommonRoute.GetUniversity_Majors_SubjectGroup), HttpPost]
        public async Task<University_Majors_SubjectGroupDTO> Get([FromBody] University_Majors_SubjectGroupDTO University_Majors_SubjectGroupDTO)
        {
            if (University_Majors_SubjectGroupDTO == null) University_Majors_SubjectGroupDTO = new University_Majors_SubjectGroupDTO();

            University_Majors_SubjectGroup University_Majors_Subject = new University_Majors_SubjectGroup { Id = University_Majors_SubjectGroupDTO.Id };
            University_Majors_Subject = await University_Majors_SubjectGroupService.Get(University_Majors_Subject.Id);

            return University_Majors_Subject == null ? null : new University_Majors_SubjectGroupDTO()
            {
                Id = University_Majors_Subject.Id,
                MajorsId = University_Majors_Subject.MajorsId,
                MajorsCode = University_Majors_Subject.MajorsCode,
                MajorsName = University_Majors_Subject.MajorsName,
                UniversityId = University_Majors_Subject.UniversityId,
                UniversityCode = University_Majors_Subject.UniversityCode,
                UniversityName = University_Majors_Subject.UniversityName,
                Year = University_Majors_Subject.Year,
                SubjectGroupId = University_Majors_Subject.SubjectGroupId,
                SubjectGroupCode = University_Majors_Subject.SubjectGroupCode,
                SubjectGroupName = University_Majors_Subject.SubjectGroupName,
                Benchmark = University_Majors_Subject.Benchmark,
                Quantity = University_Majors_Subject.Quantity,
                University_MajorsId = University_Majors_Subject.University_MajorsId,
                Note = University_Majors_Subject.Note
            };
        }

        [AllowAnonymous]
        [Route(CommonRoute.ListUniversity_Majors_SubjectGroup), HttpPost]
        public async Task<List<University_Majors_SubjectGroupDTO>> List([FromBody] University_Majors_SubjectGroupFilterDTO University_Majors_SubjectFilterDTO)
        {
            University_Majors_SubjectGroupFilter University_Majors_SubjectFilter = new University_Majors_SubjectGroupFilter
            {
                University_MajorsId = University_Majors_SubjectFilterDTO.University_MajorsId,
                UniversityId = University_Majors_SubjectFilterDTO.UniversityId,
                UniversityCode = new StringFilter { StartsWith = University_Majors_SubjectFilterDTO.UniversityCode },
                UniversityName = new StringFilter { Contains = University_Majors_SubjectFilterDTO.UniversityName },
                MajorsId = University_Majors_SubjectFilterDTO.MajorsId,
                MajorsCode = new StringFilter { StartsWith = University_Majors_SubjectFilterDTO.MajorsCode },
                MajorsName = new StringFilter { Contains = University_Majors_SubjectFilterDTO.MajorsName },
                SubjectGroupId = University_Majors_SubjectFilterDTO.SubjectGroupId,
                SubjectGroupCode = new StringFilter { StartsWith = University_Majors_SubjectFilterDTO .SubjectGroupCode },
                BenchmarkHigh = new DoubleFilter { LessEqual = University_Majors_SubjectFilterDTO.BenchmarkHigh},
                BenchmarkLow = new DoubleFilter { GreaterEqual = University_Majors_SubjectFilterDTO.BenchmarkLow },
                Year = new StringFilter { Equal = University_Majors_SubjectFilterDTO.Year },
                Skip = University_Majors_SubjectFilterDTO.Skip,
                Take = int.MaxValue,
                OrderType = OrderType.DESC,
                OrderBy = University_Majors_SubjectGroupOrder.SubjectGroupCode
            };

            List<University_Majors_SubjectGroup> University_Majors_Subjects = await University_Majors_SubjectGroupService.List(University_Majors_SubjectFilter);

            List<University_Majors_SubjectGroupDTO> University_Majors_SubjectDTOs = University_Majors_Subjects.Select(u => new University_Majors_SubjectGroupDTO
            {
                Id = u.Id,
                MajorsId = u.MajorsId,
                MajorsCode = u.MajorsCode,
                MajorsName = u.MajorsName,
                UniversityId = u.UniversityId,
                UniversityCode = u.UniversityCode,
                UniversityName = u.UniversityName,
                University_MajorsId = u.University_MajorsId,
                SubjectGroupId = u.SubjectGroupId,
                SubjectGroupCode = u.SubjectGroupCode,
                SubjectGroupName = u.SubjectGroupName,
                Benchmark = u.Benchmark,
                Quantity = u.Quantity,
                Note = u.Note,
                Year = u.Year
            }).ToList();

            return University_Majors_SubjectDTOs;
        }
        #endregion
    }
}
