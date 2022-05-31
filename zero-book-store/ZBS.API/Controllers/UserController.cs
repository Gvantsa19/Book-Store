using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims; 
using ZBS.API.Models.User;
using ZBS.Application.Services.LoggedInUserServ;
using ZBS.Application.Services.UserServ;
using ZBS.Application.Services.UserServ.Dtoes;
using ZBS.Infrastructure.Models;
using ZBS.Infrastructure.Repositories.User;
using ZBS.Infrastructure.Repositories.User.Dto;
using ZBS.Shared.Exceptions;
using ZBS.Shared.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILoggedInUserService _loggedInUserService;

        public UserController(IUserService userService, ILoggedInUserService loggedInUserService)
        {
            this.userService = userService;
            _loggedInUserService = loggedInUserService;
        }

        [HttpPost("Registration")]
        public async Task<GetDetailsResDto> Register(RegisterCustomerDto registerCustomerDto)
        {
            return await userService.Register(registerCustomerDto);
        }

        [HttpPost("Login")]
        public async Task<AuthenticateResDto> LoginUserAsync(CustomerLoginDto login)
        {
            return await userService.LoginUserAsync(login);
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<ActionResult> ChangePassword(string password, string confirmPassword)
        {
            var temp=new ChangePasswordDto();

            temp.Id=_loggedInUserService.GetUserId();
            temp.Password=password;
            temp.ConfirmPassword=confirmPassword;

            await userService.ChangePasswordAsync(temp);

            return Ok();
        } 

        [HttpPost("ForgotPassword")]
        public async Task ForgotPassword(forgotPassswordDto forgotPasssword)
        {
            await userService.ForgotPasswordAsync(forgotPasssword);
        }

        [Authorize]
        [HttpGet("Me")]
        public async Task<UserEntity> Me()
        {
            return await userService.GetUserByIdAsync(_loggedInUserService.GetUserId());
        }

        [Authorize]
        [HttpPatch("Update")]
        public async Task<ActionResult<GetUserProfileDto>> UpdateUserProfile(UpdateUserProfileModel model)
        {  
            if (model != null)
            {
                var dto = new UpdateUserProfileDto();
                dto.Id = _loggedInUserService.GetUserId();
                dto.FirstName = model.FirstName;
                dto.LastName = model.LastName;
                dto.Email = model.Email;
                dto.MobileNumber = model.MobileNumber;
                dto.DateOfBirth = model.DateOfBirth;
                dto.Education = model.Education;
                dto.Address = model.Address; 
                return Ok(await userService.UpdateUserProfile(dto));
            }
            return BadRequest();
        }
    }
}
