using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MUser;

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
        private IUserService UserService;
        public StudentController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        #region Create
        [Route(StudentControllerRoute.Create), HttpPost]
        public async Task<ActionResult<RegisterDTO>> Create([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null) registerDTO = new RegisterDTO();

            User user = new User
            {
                Dob = registerDTO.Dob,
                Email = registerDTO.Email,
                Ethnic = registerDTO.Ethnic,
                FullName = registerDTO.FullName,
                Gender = registerDTO.Gender,
                Identify = registerDTO.Identify,
                Phone = registerDTO.Phone
            };
            user = await UserService.Register(user);

            registerDTO = new RegisterDTO
            {
                Dob = registerDTO.Dob,
                Email = registerDTO.Email,
                Ethnic = registerDTO.Ethnic,
                FullName = registerDTO.FullName,
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
            await this.UserService.ImportExcel(memoryStream.ToArray());
        }
    }
}
