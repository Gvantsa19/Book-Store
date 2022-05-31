using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models.Enum;

namespace ZBS.Infrastructure.Models
{
    [Table("User")]
    public class User:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        
        [Phone]
        public string? MobileNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        [MaxLength(250)]
        public string Password { get; set; }

        [MaxLength(250)]
        public string Salt { get; set; }

        [MaxLength(200)]
        public string? Education { get; set; }

        [MaxLength(150)]
        public string Address { get; set; }
    }
}
