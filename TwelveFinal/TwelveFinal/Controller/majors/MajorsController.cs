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
    public class MajorsRoute : Root
    {
        public const string Default = Base + "/majors";
        public const string Create = Default + "/create";
        public const string Get = Default + "/get";
        public const string List = Default + "/list";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
    }

    public class MajorsController : ApiController
    {
        private IMajorsService MajorsService;

        public MajorsController(IMajorsService majorsService)
        {
            this.MajorsService = majorsService;
        }

        [Route(MajorsRoute.Create), HttpPost]
        public async Task<ActionResult<MajorsDTO>> Create([FromBody] MajorsDTO majorsDTO)
        {
            if (majorsDTO == null) majorsDTO = new MajorsDTO();

            Majors majors = ConvertDTOtoBO(majorsDTO);
            majors = await MajorsService.Create(majors);

            majorsDTO = new MajorsDTO
            {
                Id = majors.Id,
                Code = majors.Code,
                Name = majors.Name,
                Errors = majors.Errors
            };
            if (majors.HasError)
                return BadRequest(majorsDTO);
            return Ok(majorsDTO);
        }

        [Route(MajorsRoute.Update), HttpPost]
        public async Task<ActionResult<MajorsDTO>> Update([FromBody] MajorsDTO majorsDTO)
        {
            if (majorsDTO == null) majorsDTO = new MajorsDTO();

            Majors majors = ConvertDTOtoBO(majorsDTO);
            majors = await MajorsService.Update(majors);

            majorsDTO = new MajorsDTO
            {
                Id = majors.Id,
                Code = majors.Code,
                Name = majors.Name,
                Errors = majors.Errors
            };
            if (majors.HasError)
                return BadRequest(majorsDTO);
            return Ok(majorsDTO);
        }

        [Route(MajorsRoute.Get), HttpPost]
        public async Task<MajorsDTO> Get([FromBody] MajorsDTO MajorsDTO)
        {
            if (MajorsDTO == null) MajorsDTO = new MajorsDTO();

            Majors majors = ConvertDTOtoBO(MajorsDTO);
            majors = await MajorsService.Get(majors.Id);

            return majors == null ? null : new MajorsDTO()
            {
                Id = majors.Id,
                Code = majors.Code,
                Name = majors.Name,
                Errors = majors.Errors
            };
        }

        [Route(MajorsRoute.List), HttpPost]
        public async Task<List<MajorsDTO>> List([FromBody] MajorsFilterDTO majorsFilterDTO)
        {
            MajorsFilter majorsFilter = new MajorsFilter
            {
                Code = new StringFilter { StartsWith = majorsFilterDTO.Code },
                Name = new StringFilter { Contains = majorsFilterDTO.Name },
                Skip = majorsFilterDTO.Skip,
                Take = majorsFilterDTO.Take
            };

            List<Majors> universities = await MajorsService.List(majorsFilter);

            List<MajorsDTO> majorsDTOs = universities.Select(u => new MajorsDTO
            {
                Id = u.Id,
                Code = u.Code,
                Name = u.Name
            }).ToList();

            return majorsDTOs;
        }

        [Route(MajorsRoute.Delete), HttpPost]
        public async Task<ActionResult<MajorsDTO>> Delete([FromBody] MajorsDTO majorsDTO)
        {
            if (majorsDTO == null) majorsDTO = new MajorsDTO();

            Majors majors = ConvertDTOtoBO(majorsDTO);
            majors = await MajorsService.Delete(majors);

            majorsDTO = new MajorsDTO
            {
                Id = majors.Id,
                Code = majors.Code,
                Name = majors.Name,
                Errors = majors.Errors
            };
            if (majors.HasError)
                return BadRequest(majorsDTO);
            return Ok(majorsDTO);
        }

        private Majors ConvertDTOtoBO(MajorsDTO majorsDTO)
        {
            Majors majors = new Majors
            {
                Id = majorsDTO.Id ?? Guid.Empty,
                Code = majorsDTO.Code,
                Name = majorsDTO.Name,
            };
            return majors;
        }
    }
}
