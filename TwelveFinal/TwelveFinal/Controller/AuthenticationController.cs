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
                Username = loginDTO.Username,
                Password = loginDTO.Password
            };
            User user = await this.userService.Login(userFilter);
            Response.Cookies.Append("token", user.Jwt);
            return new LoginResultDTO()
            {
                Username = user.Username,
                Token = user.Jwt,
                ExpiredTime = user.ExpiredTime
            };
        }


        [Route("ChangePassword"), HttpPost]
        public async Task<bool> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            UserFilter userFilter = new UserFilter()
            {
                Username = changePasswordDTO.Username,
                Password = changePasswordDTO.Password
            };
            User user = await this.userService.ChangePassword(userFilter, changePasswordDTO.NewPassword);
            return user != null;
        }

        [Route("Register"), HttpPost]
        public async Task<ActionResult<RegisterDTO>> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException(ModelState);

            User user = new User
            {
                Username = registerDTO.Username,
                Password = registerDTO.Password
            };
            user = await userService.Register(user);

            registerDTO = new RegisterDTO
            {
                Username = user.Username,
                Errors = user.Errors
            };
            if (user.IsValidated)
                return registerDTO;
            else
            {
                return BadRequest(registerDTO);
            }
        }
    }
}
