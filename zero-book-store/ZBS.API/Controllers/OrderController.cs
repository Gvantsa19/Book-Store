using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using ZBS.API.Models.Order;
using ZBS.Application.Pagination;
using ZBS.Application.Services.LoggedInUserServ;
using ZBS.Application.Services.OrderService;
using ZBS.Infrastructure.Models.Enum;
using ZBS.Infrastructure.Repositories.Orders;
using ZBS.Infrastructure.Repositories.Orders.CrudModels;

namespace ZBS.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository OrderRepository;
        private readonly IOrderService _orderService;
        private readonly ILoggedInUserService _loggedInUserService;
        public OrderController(IOrderRepository OrderRepository, IOrderService orderService, ILoggedInUserService loggedInUserService)
        {
            this.OrderRepository = OrderRepository;
            _orderService = orderService;
            _loggedInUserService = loggedInUserService;
        }
        [Authorize(Roles = "Admin,Delivery")]
        [HttpGet("GetAll")]
        public async Task<IEnumerable<OrderEntity>> GetAllAsync(int currentPageNumber, int pageSize)
        {
            return await OrderRepository.GetAllAsync(currentPageNumber, pageSize);
        }
       
        [Authorize]
        [HttpPut("Update")]
        public async Task<OrderEntity> UpdateAsync(UpdateOrderModel entity)
        {
            return await OrderRepository.UpdateAsync(entity);
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<OrderEntity> DeleteAsync(int id)
        {
            return await OrderRepository.DeleteAsync(id);
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<GetAllOrderDto>> CreateOrder([FromQuery] CreateOrderModell model, PaymentType paymentType)
        {
            var dto = new CreateOrderDto()
            {
                UserId = _loggedInUserService.GetUserId(),
                Address = model.Address,
                Phone = model.Phone,
                paymentType = model.paymentType
            };
            return Ok(await _orderService.CreateOrder(dto, paymentType));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetByStatus")]
        public async Task<ActionResult<IEnumerable<GetAllOrderDto>>> GetByStatus(Status status, [FromQuery] PagingParameters pagingParameters)
        {
            return Ok(await _orderService.GetByStatus(status, pagingParameters));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("SortAddress/{address}")]
        public async Task<ActionResult<IEnumerable<GetAllOrderDto>>> SortAddress(string address, [FromQuery] PagingParameters pagingParameters)
        {
            return Ok(await _orderService.SortAddress(address, pagingParameters));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("SortByDate")]
        public async Task<ActionResult<IEnumerable<GetAllOrderDto>>> SortByDate(SortDate sortDate, [FromQuery] PagingParameters pagingParameters)
        {
            return Ok(await _orderService.SortByDate(sortDate, pagingParameters));
        }

        [Authorize(Roles = "Admin, Delivery")]
        [HttpPatch("EditStatus")]
        public async Task<ActionResult<GetAllOrderDto>> EditStatus([FromQuery] EditOrderStatusModel model)
        {
            if (model != null)
            {
                var dto = new EditOrderStatusDto()
                {
                    UserId = _loggedInUserService.GetUserId(),
                    Id = model.Id,
                    status = model.status
                };
                return Ok(await _orderService.EditStatus(dto));
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("GetOrders")]
        public async Task<ActionResult<IEnumerable<GetAllOrderDto>>> GetOrders([FromQuery] PagingParameters pagingParameters)
        { 
            return Ok(await _orderService.GetOrders(_loggedInUserService.GetUserId(), pagingParameters)); 
        }

    }
}
