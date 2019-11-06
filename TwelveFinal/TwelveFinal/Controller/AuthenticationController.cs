using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Controller.DTO;
using TwelveFinal.Entities;
using TwelveFinal.Services.MUser;

namespace TwelveFinal.Controller
{
    [Route("api/Authentication")]
    public class AuthenticationController : ControllerBase
    {
        private IUserService userService;
        public AuthenticationController(IUserService _userService)
        {
            this.userService = _userService;
        }
        [Route("Login"), HttpPost]
        public async Task<LoginResultDTO> Login([FromBody] LoginDTO loginDTO)
        {
            UserFilter userFilter = new UserFilter()
            {
                Email = loginDTO.EmailOrPhone,
                Phone = loginDTO.EmailOrPhone,
                Password = loginDTO.Password
            };
            User user = await this.userService.Login(userFilter);
            Response.Cookies.Append("token", user.Jwt);
            return new LoginResultDTO()
            {
                FullName = user.FullName,
                Token = user.Jwt,
                ExpiredTime = user.ExpiredTime
            };
        }


        [Route("ChangePassword"), HttpPost]
        public async Task<bool> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            UserFilter userFilter = new UserFilter()
            {
                Identify = changePasswordDTO.Identify,
                Password = changePasswordDTO.Password
            };
            User user = await this.userService.ChangePassword(userFilter, changePasswordDTO.NewPassword);
            return user != null;
        }

        [Route("ForgotPassword"), HttpPost]
        public async Task<bool> ForgotPassword([FromBody] ForgotPasswordDTO forgotPasswordDTO)
        {
            UserFilter userFilter = new UserFilter()
            {
                Identify = forgotPasswordDTO.Identify,
                Email = forgotPasswordDTO.Email
            };
            await this.userService.ForgotPassword(userFilter);
            return true;
        }

        //[Route("Register"), HttpPost]
        //public async Task<ActionResult<RegisterDTO>> Register([FromBody] RegisterDTO registerDTO)
        //{
        //    User user = new User
        //    {
        //        FullName = registerDTO.FullName,
        //        Password = registerDTO.Password,
        //        Gender = registerDTO.Gender,
        //        Email = registerDTO.Email,
        //        Phone = registerDTO.Phone
        //    };
        //    user = await userService.Register(user);

        //    registerDTO = new RegisterDTO
        //    {
        //        FullName = user.FullName,
        //        Errors = user.Errors
        //    };
        //    if (user.IsValidated)
        //        return registerDTO;
        //    else
        //    {
        //        return BadRequest(registerDTO);
        //    }
        //}
    }
}
