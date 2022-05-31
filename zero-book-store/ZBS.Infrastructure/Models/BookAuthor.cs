using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Models
{
    [Table("BookAuthor")]
    public class BookAuthor:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book book { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public Author author { get; set; }  
    }
}
