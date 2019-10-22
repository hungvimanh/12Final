using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MSubjectGroup;

namespace TwelveFinal.Controller.subject_group
{
    public class SubjectGrupRoute
    {
        public const string Default = "api/TF/subject-group";
        public const string Create = Default + "/create";
        public const string Get = Default + "/get";
        public const string List = Default + "/list";
        public const string Update = Default + "/update";
        public const string Delete = Default+  "/delete";
    }

    [ApiController]
    public class SubjectGrupController : ApiController
    {
        private ISubjectGroupService SubjectGrupService;
        public SubjectGrupController(ISubjectGroupService SubjectGrupService)
        {
            this.SubjectGrupService = SubjectGrupService;
        }

        [Route(SubjectGrupRoute.Create), HttpPost]
        public async Task<ActionResult<SubjectGroupDTO>> Create([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            SubjectGroup subjectGroup = ConvertDTOtoBO(subjectGroupDTO);

            subjectGroup = await SubjectGrupService.Create(subjectGroup);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name,
                Errors = subjectGroup.Errors
            };

            if (subjectGroup.IsValidated)
                return Ok(subjectGroupDTO);
            else
            {
                return BadRequest(subjectGroupDTO);
            }
        }

        [Route(SubjectGrupRoute.Get), HttpPost]
        public async Task<SubjectGroupDTO> Get([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            SubjectGroup subjectGroup = new SubjectGroup { Id = subjectGroupDTO.Id ?? default};

            subjectGroup = await SubjectGrupService.Get(subjectGroup.Id);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name,
                Errors = subjectGroup.Errors
            };
            return subjectGroupDTO;
        }

        [Route(SubjectGrupRoute.List), HttpPost]
        public async Task<List<SubjectGroupDTO>> List([FromBody] SubjectGroupFilterDTO subjectGroupFilterDTO)
        {
            SubjectGroupFilter filter = new SubjectGroupFilter
            {
                Id = subjectGroupFilterDTO.Id,
                Code = subjectGroupFilterDTO.Code,
                Name = subjectGroupFilterDTO.Name,
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

        [Route(SubjectGrupRoute.Update), HttpPost]
        public async Task<ActionResult<SubjectGroupDTO>> Update([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            SubjectGroup subjectGroup = ConvertDTOtoBO(subjectGroupDTO);

            subjectGroup = await SubjectGrupService.Update(subjectGroup);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name,
                Errors = subjectGroup.Errors
            };

            if (subjectGroup.IsValidated)
                return Ok(subjectGroupDTO);
            else
            {
                return BadRequest(subjectGroupDTO);
            }
        }

        [Route(SubjectGrupRoute.Delete), HttpPost]
        public async Task<ActionResult<SubjectGroupDTO>> Delete([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            SubjectGroup subjectGroup = new SubjectGroup { Id = subjectGroupDTO.Id ?? default };

            subjectGroup = await SubjectGrupService.Delete(subjectGroup);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name,
                Errors = subjectGroup.Errors
            };

            if (subjectGroup.IsValidated)
                return Ok(subjectGroupDTO);
            else
            {
                return BadRequest(subjectGroupDTO);
            }
        }

        private SubjectGroup ConvertDTOtoBO(SubjectGroupDTO subjectGroupDTO)
        {
            SubjectGroup subjectGroup = new SubjectGroup
            {
                Id = subjectGroupDTO.Id ?? Guid.Empty,
                Code = subjectGroupDTO.Code,
                Name = subjectGroupDTO.Name
            };

            return subjectGroup;
        }
    }
}
