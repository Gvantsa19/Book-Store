using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Models
{
    [Table("Order")]
    public class Order:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User user { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required, MaxLength(120)]
        public string Address { get; set; }

        [Required,Phone]
        public string Phone { get; set; }

        [Required,MaxLength(30)]
        public string PaymentType { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public decimal ShippingPrice { get; set; }

        [Required]
        public int DeliveryId { get; set; } 

        [Required,MaxLength(20)]
        public string Status { get; set; }
    }
}
