using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.User.Dto
{
    public class GetUserProfileDto
    { 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Education { get; set; }
        public string? Address { get; set; }
    }
}
