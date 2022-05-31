using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure;
using ZBS.Infrastructure.Models.Enum;

namespace ZBS.Application.Services.RoleControlServ.Dtoes
{
    public class RoleUpdateDetailsDto: BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }
        public string? Email { get; set; }
    }
}
