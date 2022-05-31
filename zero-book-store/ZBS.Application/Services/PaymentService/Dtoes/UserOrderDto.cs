using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models.Enum;

namespace ZBS.Application.Services.PaymentServ.Dtoes
{
    public class UserOrderDto
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public Currency Currency { get; set; }
    }
}
