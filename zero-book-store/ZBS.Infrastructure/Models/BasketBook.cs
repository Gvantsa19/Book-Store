using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Models
{
    [Table("BasketBook")]
    public class BasketBook:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public float Price { get; set; }

        [Required]
        public int BasketId { get; set; }
        public Basket basket { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
