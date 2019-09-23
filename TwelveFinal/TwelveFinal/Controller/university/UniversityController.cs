using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MMajors;
using TwelveFinal.Services.MUniversity;

namespace TwelveFinal.Controller.university
{
    public class UniversityRoute
    {
        public const string Default = "api/TF/university";
        public const string Create = Default + "/create";
        public const string Get = Default + "/get";
        public const string List = Default + "/list";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
    }

    [ApiController]
    public class UniversityController : ControllerBase
    {
        private IUniversityService universityService;
        private IMajorsService majorsService;

        public UniversityController(IUniversityService universityService, IMajorsService majorsService)
        {
            this.majorsService = majorsService;
            this.universityService = universityService;
        }

        [Route(UniversityRoute.Create), HttpPost]
        public async Task<UniversityDTO> Create([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null) universityDTO = new UniversityDTO();

            University university = ConvertDTOtoBO(universityDTO);
            university = await universityService.Create(university);

            universityDTO = new UniversityDTO
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address
            };
            if (university.IsValidated)
                return universityDTO;
            else
            {
                universityDTO.Id = null;
                throw new BadRequestException(universityDTO);
            }
        }

        [Route(UniversityRoute.Update), HttpPost]
        public async Task<UniversityDTO> Update([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null) universityDTO = new UniversityDTO();

            University university = ConvertDTOtoBO(universityDTO);
            university = await universityService.Update(university);

            universityDTO = new UniversityDTO
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address
            };
            if (university.IsValidated)
                return universityDTO;
            else
            {
                throw new BadRequestException(universityDTO);
            }
        }

        [Route(UniversityRoute.Get), HttpPost]
        public async Task<UniversityDTO> Get([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null) universityDTO = new UniversityDTO();
            if (universityDTO.Id == null) return null;

            University university = ConvertDTOtoBO(universityDTO);
            university = await universityService.Get(university.Id);

            return university == null ? null : new UniversityDTO()
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address
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
                Skip = 0,
                Take = int.MaxValue
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
        public async Task<UniversityDTO> Delete([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null) universityDTO = new UniversityDTO();

            University university = ConvertDTOtoBO(universityDTO);
            university = await universityService.Delete(university);

            universityDTO = new UniversityDTO
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address
            };
            if (university.IsValidated)
                return universityDTO;
            else
            {
                throw new BadRequestException(universityDTO);
            }
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
