using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.Sales
{
    [Table("Sales")]
    public class SalesEntity : BaseEntity
    {
        public int Id { get; set; }
        public int CategoryID { get; set; }
        public short Percent { get; set; }

    }
}
