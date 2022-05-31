using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Models
{
        [Table("Sales")]
    public class Sales : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryID { get; set; }
        
        [Required]
        public BookCategory Category { get; set; }

        [Required]
        public short Percent { get; set; }

        //public DateTime ExpirationDate { get; set; }

    }
}
