using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.Orders.CrudModels
{
    public class UpdateOrderModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? PaymentType { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ShippingPrice { get; set; }
        public int DeliveryId { get; set; }
        public string? Status { get; set; }
    }
}
