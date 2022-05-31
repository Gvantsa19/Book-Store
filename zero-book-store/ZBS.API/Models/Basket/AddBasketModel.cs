using DataAnnotationsExtensions;

namespace ZBS.API.Models.Basket
{
    public class AddBasketModel
    {
        public int BookId { get; set; }
        [Min(1)]
        public int Quantity { get; set; }
    }
}
