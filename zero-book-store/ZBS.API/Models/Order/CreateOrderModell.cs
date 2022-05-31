using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ZBS.Infrastructure.Models.Enum;

namespace ZBS.API.Models.Order
{
    public class CreateOrderModell
    { 
        [MaxLength(100)]
        public string Address { get; set; }
        [Display(Name = "Mobile Number:")]
        [Required(ErrorMessage = "Mobile Number is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{9})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Phone { get; set; }
        public PaymentType paymentType { get; set; }
        
    }
}
