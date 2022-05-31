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
    [Table("Book")]
    public class Book:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(60)]
        public string Title { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public BookCategory BookCategory { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime DateofPublish { get; set; }

        [Required, MinLength(60)]
        public string Publisher { get; set; }

        [Required]
        public int NumberOfPages { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required,MaxLength(30)]
        public Language Language { get; set; }
    }
}
