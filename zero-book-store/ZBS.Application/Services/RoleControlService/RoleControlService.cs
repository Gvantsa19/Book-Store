using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.Services.RoleControlServ.Dtoes;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.GenericRepositoryP;
using ZBS.Infrastructure.Models;
using ZBS.Infrastructure.Models.Enum;
using ZBS.Infrastructure.Repositories.User;
using ZBS.Shared.Exceptions;

namespace ZBS.Application.Services.RoleControlServ
{
    public class RoleControlService : IRoleControlService
    {
       
        private readonly IUserRepository _userRepository;

        public RoleControlService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<RoleUpdateDetailsDto> UpdateUserRoleAsync(int Id, Role role)
        {
            var user = await _userRepository.GetUserByIdAsync(Id);

            if (user == null || user.DateDeleted != null)
            {
                throw new RoleException(string.Format($"User does not exist"));
            }

            if (user.Role == role)
            {
                throw new RoleException(string.Format($"User already has {role.ToString()} Role"));
            }

            var changedUser = await _userRepository.UpdateUserRole(Id, role);

            var roleUpdatedUser = new RoleUpdateDetailsDto
            {
                Id = changedUser.Id,
                FirstName = changedUser.FirstName,
                LastName = changedUser.LastName,
                Role = changedUser.Role,
                Email = changedUser.Email,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            return roleUpdatedUser;
        }
    }
}
