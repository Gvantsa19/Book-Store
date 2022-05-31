using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.BookCategory
{
    [Table("BookCategory")]
    public class BookCategoryEntity: BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
