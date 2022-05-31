using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models.Enum;
using ZBS.Infrastructure.Repositories.User.Dto;

namespace ZBS.Infrastructure.Repositories.User
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetAllUsersAsync(int currentPageNumber, int pageSize);
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<UserEntity> DeleteUserById(int id);
        Task<UserEntity> UpdateUserRole(int id, Role role);
        Task<GetUserProfileDto> UpdateUserProfile(UpdateUserProfileDto dto);
    }
}
