using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models.Enum;

namespace ZBS.Infrastructure.Repositories.User
{
    [Table("User")]
    public class UserEntity:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public Role Role { get; set; }
        public string? Email { get; set; }

        public string? MobileNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        //public string Password { get; set; }

        //public string Salt { get; set; }

        public string? Education { get; set; }

        public string? Address { get; set; }
    }
}
