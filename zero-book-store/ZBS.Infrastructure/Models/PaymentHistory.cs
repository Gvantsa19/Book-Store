using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models.Enum;

namespace ZBS.Infrastructure.Models
{
    [Table("PaymentHistory")]
    public class PaymentHistory:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required,MaxLength(30)]
        public string PaymentType { get; set; }

        [Required]
        public int UserId { get; set; }
        public User user { get; set; }

        [Required]
        public decimal Amount { get; set; }
        public Currency Currency { get; set; } 

        [Required]
        public DateTime DateOfPayment { get; set; }

        [CreditCard]
        public string? CardNumber { get; set; }

        [MaxLength(3)]
        public string? CVC { get; set; }
    }
}
