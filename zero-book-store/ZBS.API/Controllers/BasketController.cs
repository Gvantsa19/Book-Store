using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using ZBS.API.Models.Basket;
using ZBS.Application.Pagination;
using ZBS.Application.Services.CartService;
using ZBS.Application.Services.LoggedInUserServ;
using ZBS.Infrastructure.Repositories.Cart.Dto;

namespace ZBS.API.Controllers
{
    [Route("api/[controller]")]

    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private ILoggedInUserService _loggedInUserService;
        public BasketController(IBasketService basketService, ILoggedInUserService loggedInUserService)
        {
            _basketService = basketService;
            _loggedInUserService = loggedInUserService;
        }

        [Authorize]
        [HttpPost("AddToBasket")]
        public async Task<ActionResult<GetAllItemsDto>> AddToCart(AddBasketModel model)
        {
            if (model != null)
            {
                var dto = new AddBasketDto()
                {
                    UserId = _loggedInUserService.GetUserId(),
                    BookId = model.BookId,
                    Quantity = model.Quantity
                }; 
                return Ok(await _basketService.AddToCart(dto));
            }
            return BadRequest();
        }
        [Authorize]
        [HttpGet("GetItems")]
        public async Task<ActionResult<IEnumerable<GetAllItemsDto>>> GetAllItems([FromQuery] PagingParameters pagingParameter)
        {
             return Ok(await _basketService.GetAllItems(_loggedInUserService.GetUserId(), pagingParameter)); 
        }

        [Authorize]
        [HttpPut("EditItem")]
        public async Task<ActionResult<GetAllItemsDto>> EditItem(AddBasketModel model)
        {
            if (model != null)
            {
                var dto = new AddBasketDto()
                {
                    UserId = _loggedInUserService.GetUserId(),
                    BookId = model.BookId,
                    Quantity = model.Quantity
                };
                return Ok(await _basketService.EditItem(dto));
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPatch("DeleteItem")]
        public async Task<ActionResult> DeleteItem(DeleteItemModel model)
        {
            if (model != null)
            {
                var dto = new DeleteItemDto()
                {
                    UserId = _loggedInUserService.GetUserId(),
                    BookId = model.BookId
                };
                await _basketService.DeleteItem(dto);
                return Ok();
            }
            return BadRequest();
          
        }

        [Authorize]
        [HttpPut("CleanBasket")]
        public async Task DeleteCart()
        { 
            await _basketService.DeleteCart(_loggedInUserService.GetUserId());  
        }
    }
}
