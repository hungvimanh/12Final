using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MUniversity;

namespace TwelveFinal.Controller.university
{
    public class UniversityRoute : Root
    {
        public const string Default = Base + "/university";
        public const string Create = Default + "/create";
        public const string Get = Default + "/get";
        public const string List = Default + "/list";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
    }

    public class UniversityController : ApiController
    {
        private IUniversityService universityService;

        public UniversityController(IUniversityService universityService)
        {
            this.universityService = universityService;
        }

        [Route(UniversityRoute.Create), HttpPost]
        public async Task<ActionResult<UniversityDTO>> Create([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null) universityDTO = new UniversityDTO();

            University university = ConvertDTOtoBO(universityDTO);
            university = await universityService.Create(university);

            universityDTO = new UniversityDTO
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address,
                Errors = university.Errors
            };
            if (university.HasError)
                return BadRequest(universityDTO);
            return Ok(universityDTO);
        }

        [Route(UniversityRoute.Update), HttpPost]
        public async Task<ActionResult<UniversityDTO>> Update([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null) universityDTO = new UniversityDTO();

            University university = ConvertDTOtoBO(universityDTO);
            university = await universityService.Update(university);

            universityDTO = new UniversityDTO
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address,
                Errors = university.Errors
            };
            if (university.HasError)
                return BadRequest(universityDTO);
            return Ok(universityDTO);
        }

        [Route(UniversityRoute.Get), HttpPost]
        public async Task<UniversityDTO> Get([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null) universityDTO = new UniversityDTO();

            University university = new University { Id = universityDTO.Id ?? default };
            university = await universityService.Get(university.Id);

            return university == null ? null : new UniversityDTO()
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address,
                Errors = university.Errors
            };
        }

        [Route(UniversityRoute.List), HttpPost]
        public async Task<List<UniversityDTO>> List([FromBody] UniversityFilterDTO universityFilterDTO)
        {
            UniversityFilter universityFilter = new UniversityFilter
            {
                Id = universityFilterDTO.Id,
                Address = universityFilterDTO.Address,
                Code = universityFilterDTO.Code,
                Name = universityFilterDTO.Name,
                Skip = universityFilterDTO.Skip,
                Take = universityFilterDTO.Take
            };

            List<University> universities = await universityService.List(universityFilter);

            List<UniversityDTO> universityDTOs = universities.Select(u => new UniversityDTO
            {
                Id = u.Id,
                Address = u.Address,
                Code = u.Code,
                Name = u.Name
            }).ToList();

            return universityDTOs;
        }

        [Route(UniversityRoute.Delete), HttpPost]
        public async Task<ActionResult<UniversityDTO>> Delete([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null) universityDTO = new UniversityDTO();

            University university = new University { Id = universityDTO.Id ?? default};
            university = await universityService.Delete(university);

            universityDTO = new UniversityDTO
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address,
                Errors = university.Errors
            };
            if (university.HasError)
                return BadRequest(universityDTO);
            return Ok(universityDTO);
        }

        private University ConvertDTOtoBO(UniversityDTO universityDTO)
        {
            University university = new University
            {
                Id = universityDTO.Id ?? Guid.Empty,
                Code = universityDTO.Code,
                Name = universityDTO.Name,
                Address = universityDTO.Address
            };
            return university;
        }
    }
}
