using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Models
{
    [Table("BookCategory")]
    public class BookCategory:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(40)]
        public string Name { get; set; }
    }
}
