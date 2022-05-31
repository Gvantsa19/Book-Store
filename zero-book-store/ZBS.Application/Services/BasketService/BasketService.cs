using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.Pagination;
using ZBS.Infrastructure.Repositories.Cart;
using ZBS.Infrastructure.Repositories.Cart.Dto;

namespace ZBS.Application.Services.CartService
{
    public class BasketService : IBasketService
    {

        private IBasketRepository _basketRepository; 
        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository; 
        }

        public async Task<GetAllItemsDto> AddToCart(AddBasketDto dto)
        {
            return await _basketRepository.AddToCart(dto);
        }

        public async Task DeleteCart(int userId)
        {
            await _basketRepository.DeleteCart(userId);
        }

        public async Task DeleteItem(DeleteItemDto dto)
        {
            await _basketRepository.DeleteItem(dto);
        }

        public async Task<GetAllItemsDto> EditItem(AddBasketDto dto)
        {
            return await _basketRepository.EditItem(dto);
        }

        public async Task<IEnumerable<GetAllItemsDto>> GetAllItems(int userId, PagingParameters pagingParameters)
        {
            var t = await _basketRepository.GetAllItems(userId);
            return await Task.FromResult(PageList<GetAllItemsDto>.GetPageList(t.OrderBy(x => x.Price), pagingParameters.PageNumber, pagingParameters.PageSize));

        }
    }
}
