using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Models
{
    [Table("Ratings")]
    public class Ratings : BaseEntity
    {
        [Key]
        public int RatingId { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        [Required]
        public int UserId { get; set; }
        public User user { get; set; }
        [Required]
        public int Id { get; set; }
        public Book book { get; set; }
    }
}
