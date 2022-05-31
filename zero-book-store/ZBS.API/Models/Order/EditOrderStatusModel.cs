using ZBS.Infrastructure.Models.Enum;

namespace ZBS.API.Models.Order
{
    public class EditOrderStatusModel
    {
        public int Id { get; set; }
        public Status status { get; set; }
    }
}
