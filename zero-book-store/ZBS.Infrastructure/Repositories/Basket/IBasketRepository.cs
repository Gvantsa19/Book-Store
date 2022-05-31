using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Repositories.Cart.Dto;

namespace ZBS.Infrastructure.Repositories.Cart
{
    public interface IBasketRepository
    {
        Task<GetAllItemsDto> AddToCart(AddBasketDto dto);
        Task<GetAllItemsDto> EditItem(AddBasketDto dto);
        Task  DeleteItem(DeleteItemDto dto);
        Task<IEnumerable<GetAllItemsDto>> GetAllItems(int userId);
        Task  DeleteCart(int userId);
    }
}
