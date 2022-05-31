using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models.Enum;

namespace ZBS.Application.Services.PaymentServ.Dtoes
{
    public class CheckTaxDto
    {
        public int OrderId { get; set; }
        public Currency Currency { get; set; }
        public decimal Tax { get; set; }
        
    }
}
