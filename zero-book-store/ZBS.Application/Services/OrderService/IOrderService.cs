using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.Pagination;
using ZBS.Infrastructure.Models.Enum;
using ZBS.Infrastructure.Repositories.Orders;
using ZBS.Infrastructure.Repositories.Orders.CrudModels;

namespace ZBS.Application.Services.OrderService
{
    public interface IOrderService
    {
        Task<GetAllOrderDto> CreateOrder(CreateOrderDto dto, PaymentType paymentType);
        Task<IEnumerable<GetAllOrderDto>> GetByStatus(Status status, PagingParameters pagingParameters);
        Task<IEnumerable<GetAllOrderDto>> SortAddress(string address, PagingParameters pagingParameters);
        Task<IEnumerable<SortByDateDto>> SortByDate(SortDate sortDate, PagingParameters pagingParameters);
        Task<GetAllOrderDto> EditStatus(EditOrderStatusDto dto);
        Task<IEnumerable<GetAllOrderDto>> GetOrders(int userId, PagingParameters pagingParameters);
    }
}
