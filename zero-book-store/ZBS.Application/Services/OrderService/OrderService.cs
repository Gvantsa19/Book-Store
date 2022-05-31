using AutoMapper;
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
    public class OrderService : IOrderService
    { 
        private IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository; 
        }

        public async Task<GetAllOrderDto> CreateOrder(CreateOrderDto dto, PaymentType paymentType)
        {
            return await _orderRepository.CreateOrder(dto, paymentType); 
        }

        public async Task<GetAllOrderDto> EditStatus(EditOrderStatusDto dto)
        {
            return await _orderRepository.EditStatus(dto); 
        }



        public async Task<IEnumerable<GetAllOrderDto>> SortAddress(string address, PagingParameters pagingParameters)
        {
            var t = await _orderRepository.SortAddress(address);
            return await Task.FromResult(PageList<GetAllOrderDto>.GetPageList(t.OrderBy(x => x.Address), pagingParameters.PageNumber, pagingParameters.PageSize));

        }

        public async Task<IEnumerable<SortByDateDto>> SortByDate(SortDate sortDate, PagingParameters pagingParameters)
        {
            var t = await _orderRepository.SortByDate(sortDate);


            switch (sortDate)
            {
                case SortDate.asc:
                     
                    return await Task.FromResult(PageList<SortByDateDto>.GetPageList(t.OrderBy(x => x.DateCreated), pagingParameters.PageNumber, pagingParameters.PageSize));
                    break;
                case SortDate.desc: 
                    return await Task.FromResult(PageList<SortByDateDto>.GetPageList(t.OrderByDescending(x => x.DateCreated), pagingParameters.PageNumber, pagingParameters.PageSize));

                    break;
                default:
                    return await Task.FromResult(PageList<SortByDateDto>.GetPageList(t.OrderBy(x => x.DateCreated), pagingParameters.PageNumber, pagingParameters.PageSize));

            }


        }

        public async Task<IEnumerable<GetAllOrderDto>> GetByStatus(Status status, PagingParameters pagingParameters)
        {
            var t = await _orderRepository.GetByStatus(status); 
            return await Task.FromResult(PageList<GetAllOrderDto>.GetPageList(t.OrderBy(x => x.TotalPrice), pagingParameters.PageNumber, pagingParameters.PageSize));

        }

        public async Task<IEnumerable<GetAllOrderDto>> GetOrders(int userId, PagingParameters pagingParameters)
        {
            var t = await _orderRepository.GetOrders(userId); 
            return await Task.FromResult(PageList<GetAllOrderDto>.GetPageList(t.OrderBy(x => x.TotalPrice), pagingParameters.PageNumber, pagingParameters.PageSize));

        }
    }
}

