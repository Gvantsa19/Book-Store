using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.Pagination;
using ZBS.Infrastructure.Repositories.Cart.Dto;

namespace ZBS.Application.Services.CartService
{
    public interface IBasketService
    {
        Task<GetAllItemsDto> AddToCart(AddBasketDto dto);
        Task<GetAllItemsDto> EditItem(AddBasketDto dto);
        Task DeleteItem(DeleteItemDto dto);
        Task<IEnumerable<GetAllItemsDto>> GetAllItems(int userId, PagingParameters pagingParameters);
        Task DeleteCart(int userId);
    }
}
