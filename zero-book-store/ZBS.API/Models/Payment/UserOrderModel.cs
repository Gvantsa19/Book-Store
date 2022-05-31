using ZBS.Infrastructure.Models.Enum;

namespace ZBS.API.Controllers.Models
{
    public class UserOrderModel
    {
        public int OrderId { get; set; }
        public Currency Currency { get; set; }
    }
}
