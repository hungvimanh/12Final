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
        private const string Default = "api/TF/subject-group";
        public const string Create = Default + "/create";
        public const string Get = Default + "/get";
        public const string Update = Default + "/update";
        public const string Delete = Default+  "/delete";
    }

    [ApiController]
    public class SubjectGrupController
    {
        private ISubjectGroupService SubjectGrupService;
        public SubjectGrupController(ISubjectGroupService SubjectGrupService)
        {
            this.SubjectGrupService = SubjectGrupService;
        }

        [Route(SubjectGrupRoute.Create), HttpPost]
        public async Task<SubjectGroupDTO> Create([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            if (subjectGroupDTO.Id == null || subjectGroupDTO.Id == Guid.Empty) return null;

            SubjectGroup subjectGroup = ConvertDTOtoBO(subjectGroupDTO);

            subjectGroup = await SubjectGrupService.Create(subjectGroup);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name
            };

            if (subjectGroup.IsValidated)
                return subjectGroupDTO;
            else
            {
                subjectGroupDTO.Id = null;
                throw new BadRequestException(subjectGroupDTO);
            }
        }

        [Route(SubjectGrupRoute.Get), HttpPost]
        public async Task<SubjectGroupDTO> Get([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            if (subjectGroupDTO.Id == null || subjectGroupDTO.Id == Guid.Empty) return null;

            SubjectGroup subjectGroup = ConvertDTOtoBO(subjectGroupDTO);

            subjectGroup = await SubjectGrupService.Get(subjectGroup.Id);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name
            };
            return subjectGroupDTO;
        }

        [Route(SubjectGrupRoute.Update), HttpPost]
        public async Task<SubjectGroupDTO> Update([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            if (subjectGroupDTO.Id == null || subjectGroupDTO.Id == Guid.Empty) return null;

            SubjectGroup subjectGroup = ConvertDTOtoBO(subjectGroupDTO);

            subjectGroup = await SubjectGrupService.Update(subjectGroup);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name
            };

            if (subjectGroup.IsValidated)
                return subjectGroupDTO;
            else
            {
                throw new BadRequestException(subjectGroupDTO);
            }
        }

        [Route(SubjectGrupRoute.Delete), HttpPost]
        public async Task<SubjectGroupDTO> Delete([FromBody] SubjectGroupDTO subjectGroupDTO)
        {
            if (subjectGroupDTO == null) subjectGroupDTO = new SubjectGroupDTO();

            if (subjectGroupDTO.Id == null || subjectGroupDTO.Id == Guid.Empty) return null;

            SubjectGroup subjectGroup = ConvertDTOtoBO(subjectGroupDTO);

            subjectGroup = await SubjectGrupService.Delete(subjectGroup);
            subjectGroupDTO = new SubjectGroupDTO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name
            };

            if (subjectGroup.IsValidated)
                return subjectGroupDTO;
            else
            {
                throw new BadRequestException(subjectGroupDTO);
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
