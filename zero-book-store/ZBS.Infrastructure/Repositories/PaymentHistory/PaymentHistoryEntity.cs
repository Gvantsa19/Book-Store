using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models.Enum;

namespace ZBS.Infrastructure.Repositories.PaymentHistory
{
    [Table("PaymentHistory")]
    public class PaymentHistoryEntity:BaseEntity
    {
        public int? Id { get; set; }
        public string PaymentType { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime DateOfPayment { get; set; }
    }
}
