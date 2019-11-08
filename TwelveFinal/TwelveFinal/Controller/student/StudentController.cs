using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MStudentService;

namespace TwelveFinal.Controller.student
{
    public class StudentControllerRoute : Root
    {
        public const string Default = Base + "/student";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Import = Default + "/import";
        public const string List = Default + "/list";
    }
    public class StudentController : ApiController
    {
        private IStudentService StuedntService;
        public StudentController(IStudentService StuedntService)
        {
            this.StuedntService = StuedntService;
        }

        #region Create
        [Route(StudentControllerRoute.Create), HttpPost]
        public async Task<ActionResult<RegisterDTO>> Create([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null) registerDTO = new RegisterDTO();

            Student user = new Student
            {
                Dob = registerDTO.Dob,
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                Gender = registerDTO.Gender,
                Identify = registerDTO.Identify,
                Phone = registerDTO.Phone
            };
            user = await StuedntService.Register(user);

            registerDTO = new RegisterDTO
            {
                Dob = registerDTO.Dob,
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                Gender = registerDTO.Gender,
                Identify = registerDTO.Identify,
                Phone = registerDTO.Phone,
                Errors = user.Errors
            };
            if (user.IsValidated)
                return registerDTO;
            else
            {
                return BadRequest(registerDTO);
            }
        }
        #endregion

        #region Update
        [Route(StudentControllerRoute.Update), HttpPost]
        public async Task<ActionResult<StudentDTO>> Update([FromBody] StudentDTO studentDTO)
        {
            if (studentDTO == null) studentDTO = new StudentDTO();

            Student user = new Student
            {
                Id = studentDTO.Id,
                Dob = studentDTO.Dob,
                Name = studentDTO.Name,
                Gender = studentDTO.Gender,
                Identify = studentDTO.Identify,
                Phone = studentDTO.Phone,
                Address = studentDTO.Address,
                EthnicId = studentDTO.EthnicId,
                HighSchoolId = studentDTO.HighSchoolId,
                PlaceOfBirth = studentDTO.PlaceOfBirth,
                ProvinceId = studentDTO.ProvinceId,
                DistrictId = studentDTO.DistrictId,
                TownId = studentDTO.TownId,

                Biology = studentDTO.Biology,
                Chemistry = studentDTO.Chemistry,
                CivicEducation = studentDTO.CivicEducation, 
                Geography = studentDTO.Geography,
                History = studentDTO.History,
                Languages = studentDTO.Languages,
                Literature = studentDTO.Literature,
                Maths = studentDTO.Maths,
                Physics = studentDTO.Physics,
                
            };
            user = await StuedntService.EditProfile(user);

            studentDTO = new StudentDTO
            {
                Id = studentDTO.Id,
                Dob = studentDTO.Dob,
                Name = studentDTO.Name,
                Gender = studentDTO.Gender,
                Identify = studentDTO.Identify,
                Email = studentDTO.Email,
                Phone = studentDTO.Phone,
                Address = studentDTO.Address,
                EthnicId = studentDTO.EthnicId,
                EthnicCode = studentDTO.EthnicCode,
                EthnicName = studentDTO.EthnicName,
                HighSchoolId = studentDTO.HighSchoolId,
                HighSchoolCode = studentDTO.HighSchoolCode,
                HighSchoolName = studentDTO.HighSchoolName,
                PlaceOfBirth = studentDTO.PlaceOfBirth,
                ProvinceId = studentDTO.ProvinceId,
                ProvinceCode = studentDTO.ProvinceCode,
                ProvinceName = studentDTO.ProvinceName,
                DistrictId = studentDTO.DistrictId,
                DistrictCode = studentDTO.DistrictCode,
                DistrictName = studentDTO.DistrictName,
                TownId = studentDTO.TownId,
                TownCode = studentDTO.TownCode,
                TownName = studentDTO.TownName,

                Biology = studentDTO.Biology,
                Chemistry = studentDTO.Chemistry,
                CivicEducation = studentDTO.CivicEducation,
                Geography = studentDTO.Geography,
                History = studentDTO.History,
                Languages = studentDTO.Languages,
                Literature = studentDTO.Literature,
                Maths = studentDTO.Maths,
                Physics = studentDTO.Physics,
                Errors = user.Errors
            };
            if (user.IsValidated)
                return studentDTO;
            else
            {
                return BadRequest(studentDTO);
            }
        }
        #endregion

        [HttpPost, Route(StudentControllerRoute.Import)]
        public async Task ImportExcel([FromForm]IFormFile file)
        {
            MemoryStream memoryStream = new MemoryStream();
            Request.Body.CopyTo(memoryStream);
            await this.StuedntService.ImportExcel(memoryStream.ToArray());
        }
    }
}
