using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models.Enum;
using ZBS.Infrastructure.Repositories.Orders.CrudModels;

namespace ZBS.Infrastructure.Repositories.Orders
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderEntity>> GetAllAsync(int currentPageNumber, int pageSize);
        Task<IEnumerable<OrderEntity>> GetByUserIdAsync(int id);
        Task<OrderEntity> CreateAsync(CreateOrderModel entity);
        Task<OrderEntity> UpdateAsync(UpdateOrderModel entity);
        Task<OrderEntity> DeleteAsync(int id);
        Task<OrderEntity> GetByIdAsync(int id);
        Task<IEnumerable<GetAllOrderDto>> GetByStatus(Status status);
        Task<IEnumerable<GetAllOrderDto>> SortAddress(string address);
        Task<IEnumerable<SortByDateDto>> SortByDate(SortDate sortDate);
        Task<GetAllOrderDto> EditStatus(EditOrderStatusDto dto);

        Task<IEnumerable<GetAllOrderDto>> GetOrders(int userId);

        Task<GetAllOrderDto> CreateOrder(CreateOrderDto dto, PaymentType paymentType);
    }
}
