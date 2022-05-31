using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Application.Services.UserServ.Dtoes
{
    public class RegisterCustomerDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string? MobileNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        public string? Address { get; set; }
        public string? Education { get; set; }

    }
}
