using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Services.MMajors;

namespace TwelveFinal.Controller.majors
{
    public class MajorsRoute
    {
        private const string Default = "api/TF/majors";
        public const string Create = Default + "/create";
        public const string Get = Default + "/get";
        public const string List = Default + "/list";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
    }

    [ApiController]
    public class MajorsController
    {
        private MajorsService majorsService;

        public MajorsController(MajorsService majorsService)
        {
            this.majorsService = majorsService;
        }

        [Route(MajorsRoute.Create), HttpPost]
        public async Task<MajorsDTO> Create([FromBody] MajorsDTO MajorsDTO)
        {
            if (MajorsDTO == null) MajorsDTO = new MajorsDTO();

            Majors Majors = ConvertDTOtoBO(MajorsDTO);
            Majors = await majorsService.Create(Majors);

            MajorsDTO = new MajorsDTO
            {
                Id = Majors.Id,
                Code = Majors.Code,
                Name = Majors.Name,
            };
            if (Majors.IsValidated)
                return MajorsDTO;
            else
            {
                MajorsDTO.Id = null;
                throw new BadRequestException(MajorsDTO);
            }
        }

        [Route(MajorsRoute.Update), HttpPost]
        public async Task<MajorsDTO> Update([FromBody] MajorsDTO MajorsDTO)
        {
            if (MajorsDTO == null) MajorsDTO = new MajorsDTO();

            Majors Majors = ConvertDTOtoBO(MajorsDTO);
            Majors = await majorsService.Update(Majors);

            MajorsDTO = new MajorsDTO
            {
                Id = Majors.Id,
                Code = Majors.Code,
                Name = Majors.Name,
            };
            if (Majors.IsValidated)
                return MajorsDTO;
            else
            {
                throw new BadRequestException(MajorsDTO);
            }
        }

        [Route(MajorsRoute.Get), HttpPost]
        public async Task<MajorsDTO> Get([FromBody] MajorsDTO MajorsDTO)
        {
            if (MajorsDTO == null) MajorsDTO = new MajorsDTO();
            if (MajorsDTO.Id == null) return null;

            Majors Majors = ConvertDTOtoBO(MajorsDTO);
            Majors = await majorsService.Update(Majors);

            return Majors == null ? null : new MajorsDTO()
            {
                Id = Majors.Id,
                Code = Majors.Code,
                Name = Majors.Name,
            };
        }

        [Route(MajorsRoute.List), HttpPost]
        public async Task<List<MajorsDTO>> List([FromBody] MajorsFilterDTO MajorsFilterDTO)
        {
            MajorsFilter MajorsFilter = new MajorsFilter
            {
                Id = MajorsFilterDTO.Id,
                Code = MajorsFilterDTO.Code,
                Name = MajorsFilterDTO.Name,
                Skip = 0,
                Take = int.MaxValue
            };

            List<Majors> universities = await majorsService.List(MajorsFilter);

            List<MajorsDTO> MajorsDTOs = universities.Select(u => new MajorsDTO
            {
                Id = u.Id,
                Code = u.Code,
                Name = u.Name
            }).ToList();

            return MajorsDTOs;
        }

        [Route(MajorsRoute.List), HttpPost]
        public async Task<MajorsDTO> Delete([FromBody] MajorsDTO MajorsDTO)
        {
            if (MajorsDTO == null) MajorsDTO = new MajorsDTO();

            Majors Majors = ConvertDTOtoBO(MajorsDTO);
            Majors = await majorsService.Delete(Majors);

            MajorsDTO = new MajorsDTO
            {
                Id = Majors.Id,
                Code = Majors.Code,
                Name = Majors.Name,
            };
            if (Majors.IsValidated)
                return MajorsDTO;
            else
            {
                throw new BadRequestException(MajorsDTO);
            }
        }

        private Majors ConvertDTOtoBO(MajorsDTO MajorsDTO)
        {
            Majors Majors = new Majors
            {
                Id = MajorsDTO.Id ?? Guid.Empty,
                Code = MajorsDTO.Code,
                Name = MajorsDTO.Name,
            };
            return Majors;
        }
    }
}
