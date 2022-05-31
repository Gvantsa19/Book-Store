using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models.Enum;

namespace ZBS.Application.Services.PaymentServ.Dtoes
{
    public class PaymentDetailsDto
    {
        public int UserId { get; set; }
        public string PaymentType { get; set; }
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfPayment { get; set; }
    }
}
