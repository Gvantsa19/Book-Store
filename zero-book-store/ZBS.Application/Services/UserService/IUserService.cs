using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.Services.UserServ.Dtoes;
using ZBS.Infrastructure.Models;
using ZBS.Infrastructure.Repositories.User;
using ZBS.Infrastructure.Repositories.User.Dto;

namespace ZBS.Application.Services.UserServ
{
    public interface IUserService
    {
        Task<GetDetailsResDto> Register(RegisterCustomerDto registerCustomerDto);
        Task<AuthenticateResDto> LoginUserAsync(CustomerLoginDto login);

        Task ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task ForgotPasswordAsync(forgotPassswordDto _forgotPassswordDto);
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<UserEntity> DeleteUserByIdAsync(int id);
        Task<GetUserProfileDto> UpdateUserProfile(UpdateUserProfileDto dto);
    }
}
