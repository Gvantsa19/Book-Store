using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.User.Dto
{
    public class UpdateUserProfileDto
    {
        public int Id { get; set; } 

        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        [Required, RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }

        [Display(Name = "Mobile Number:")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{9})$", ErrorMessage = "Invalid Mobile Number.")]
        public string? MobileNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }
        [Required, MaxLength(100)]
        public string? Education { get; set; }
        [Required, MaxLength(100)]
        public string? Address { get; set; }
    }
}
