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

        [HttpPost, Route(StudentControllerRoute.Import)]
        public async Task ImportExcel([FromForm]IFormFile file)
        {
            MemoryStream memoryStream = new MemoryStream();
            Request.Body.CopyTo(memoryStream);
            await this.StuedntService.ImportExcel(memoryStream.ToArray());
        }
    }
}
