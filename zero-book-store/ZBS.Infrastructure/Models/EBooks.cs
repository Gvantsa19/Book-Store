using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Models
{
    [Table("EBooks")]
    public class EBooks : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public int FileSize { get; set; }
        [Required]
        public byte[] FileContent { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadedBy { get; set; }
    }
}
