using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.Services.RoleControlServ.Dtoes;
using ZBS.Infrastructure.Models.Enum;

namespace ZBS.Application.Services.RoleControlServ
{
    public interface IRoleControlService
    {
        Task<RoleUpdateDetailsDto> UpdateUserRoleAsync(int Id, Role role);
    }
}
