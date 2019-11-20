using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MUniversity;
using TwelveFinal.Services.MUniversity_Majors_Majors;

namespace TwelveFinal.Controller.university
{
    public class UniversityController : ApiController
    {
        private IUniversityService UniversityService;
        private IUniversity_MajorsService University_MajorsService;

        public UniversityController(IUniversityService UniversityService, IUniversity_MajorsService University_MajorsService)
        {
            this.UniversityService = UniversityService;
            this.University_MajorsService = University_MajorsService;
        }

        #region Create
        [Route(AdminRoute.CreateUniversity), HttpPost]
        public async Task<ActionResult<UniversityDTO>> Create([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null) universityDTO = new UniversityDTO();

            University university = ConvertDTOtoBO(universityDTO);
            university = await UniversityService.Create(university);
            
            universityDTO = new UniversityDTO
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address,
                Website = university.Website,
                Errors = university.Errors
            };
            if (university.HasError)
                return BadRequest(universityDTO);
            return Ok(universityDTO);
        }
        #endregion

        #region Update
        [Route(AdminRoute.UpdateUniversity), HttpPost]
        public async Task<ActionResult<UniversityDTO>> Update([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null) universityDTO = new UniversityDTO();

            University university = ConvertDTOtoBO(universityDTO);
            university = await UniversityService.Update(university);

            universityDTO = new UniversityDTO
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address,
                Website = university.Website,
                Errors = university.Errors
            };
            if (university.HasError)
                return BadRequest(universityDTO);
            return Ok(universityDTO);
        }
        #endregion

        #region Read
        [AllowAnonymous]
        [Route(CommonRoute.GetUniversity), HttpPost]
        public async Task<UniversityDTO> Get([FromBody] UniversityFilterDTO universityFilterDTO)
        {
            if (universityFilterDTO == null) universityFilterDTO = new UniversityFilterDTO();

            University university = new University { Id = universityFilterDTO.Id ?? Guid.Empty };
            university = await UniversityService.Get(university.Id);

            return university == null ? null : new UniversityDTO()
            {
                Id = university.Id,
                Code = university.Code,
                Name = university.Name,
                Address = university.Address,
                Website = university.Website,
                University_Majors = university.University_Majors.Select( u => new University_MajorsDTO
                {
                    Id = u.Id,
                    MajorsId = u.MajorsId,
                    MajorsCode = u.MajorsCode,
                    MajorsName = u.MajorsName,
                    UniversityId = u.UniversityId,
                    UniversityCode = u.UniversityCode,
                    UniversityName = u.UniversityName,
                    UniversityAddress = u.UniversityAddress,
                }).ToList(),
                Errors = university.Errors
            };
        }

        [AllowAnonymous]
        [Route(CommonRoute.ListUniversity), HttpPost]
        public async Task<List<UniversityDTO>> List([FromBody] UniversityFilterDTO universityFilterDTO)
        {
            UniversityFilter universityFilter = new UniversityFilter
            {
                Code = new StringFilter { StartsWith = universityFilterDTO.Code },
                Name = new StringFilter { Contains = universityFilterDTO.Name },
                Skip = universityFilterDTO.Skip,
                Take = int.MaxValue,
                OrderType = universityFilterDTO.OrderType,
                OrderBy = UniversityOrder.Code
            };

            List<University> universities = await UniversityService.List(universityFilter);

            List<UniversityDTO> universityDTOs = universities.Select(u => new UniversityDTO
            {
                Id = u.Id,
                Address = u.Address,
                Code = u.Code,
                Name = u.Name,
                Website = u.Website
            }).ToList();

            return universityDTOs;
        }
        #endregion

        #region Delete
        [Route(AdminRoute.DeleteUniversity), HttpPost]
        public async Task<ActionResult<UniversityDTO>> Delete([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null) universityDTO = new UniversityDTO();

            University university = new University { Id = universityDTO.Id ?? default};
            university = await UniversityService.Delete(university);

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
        #endregion

        private University ConvertDTOtoBO(UniversityDTO universityDTO)
        {
            University university = new University
            {
                Id = universityDTO.Id ?? Guid.Empty,
                Code = universityDTO.Code,
                Name = universityDTO.Name,
                Address = universityDTO.Address,
                Website = universityDTO.Website
            };
            return university;
        }
    }
}
