using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.Sales.CrudModels
{
    public class CreateSalesModel
    {
        public int CategoryID { get; set; }
        public short Percent { get; set; }
    }
}
