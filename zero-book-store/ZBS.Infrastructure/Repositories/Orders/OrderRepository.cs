using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.Models.Enum;
using ZBS.Infrastructure.Repositories.Orders.CrudModels;
using ZBS.Shared.Helpers;

namespace ZBS.Infrastructure.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {

        private readonly DbcontextDapper _dbcontextDapper;

       public OrderRepository(DbcontextDapper dbcontextDapper)
        {
            _dbcontextDapper = dbcontextDapper;
        }

        public async Task<OrderEntity> CreateAsync(CreateOrderModel entity)
        {
            using var connection = _dbcontextDapper.OpenConnection();
            var order = new OrderEntity
            {
                UserId = entity.UserId,
                Quantity = entity.Quantity,
                Address = entity.Address,
                Phone = entity.Phone,
                PaymentType = entity.PaymentType,
                TotalPrice = entity.TotalPrice,
                ShippingPrice = entity.ShippingPrice,
                DeliveryId = entity.DeliveryId,
                Status = entity.Status,
                DateCreated = DateTime.Now
            };

            var id = await connection.InsertAsync<int, OrderEntity>(order);
            order.Id = id;
            return order;
        }

        public async Task<IEnumerable<OrderEntity>> GetAllAsync(int currentPageNumber, int pageSize)
        {
            using var con = _dbcontextDapper.OpenConnection();

            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            var cmd = @"SELECT [Id],[UserId],[Quantity],[Address],[Phone],[PaymentType],[TotalPrice],[ShippingPrice],[DeliveryId],[Status]
                        FROM [ZBS].[dbo].[Order]

                        SELECT [Id],[UserId],[Quantity],[Address],[Phone],[PaymentType] ,[TotalPrice],[ShippingPrice] ,[DeliveryId],[Status]
                          FROM [ZBS].[dbo].[Order] ORDER BY Id OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = con.QueryMultiple(cmd, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<OrderEntity> allTodos = reader.Read<OrderEntity>().ToList();

            var result = new PagingResponseModel<List<OrderEntity>>(allTodos, count, currentPageNumber, pageSize);
            return result.Data;

        }


        public async Task<IEnumerable<OrderEntity>> GetByUserIdAsync(int id)
        {
            using var con=_dbcontextDapper.OpenConnection();

            return await con.QueryAsync<OrderEntity>(@"SELECT *
                  FROM [dbo].[Order]
                  where [UserId]=@Id",new {  Id= id});

        }

        public async Task<OrderEntity> GetByIdAsync(int id)
        {
            using var con = _dbcontextDapper.OpenConnection();

            return await con.GetAsync<OrderEntity>(id);

        }

        public async Task<OrderEntity> UpdateAsync(UpdateOrderModel entity)
        {
            using var con = _dbcontextDapper.OpenConnection();
            var order = await con.GetAsync<OrderEntity>(entity.Id);

            order.Id = entity.Id;
            order.UserId = entity.UserId;
            order.Quantity = entity.Quantity;
            order.Address = entity.Address;
            order.Phone = entity.Phone;
            order.PaymentType = entity.PaymentType;
            order.TotalPrice = entity.TotalPrice;
            order.ShippingPrice = entity.ShippingPrice;
            order.DeliveryId = entity.DeliveryId;
            order.Status = entity.Status;
            order.DateUpdated = DateTime.Now;
            await con.UpdateAsync(order);

            return order;
        }

        public async Task<OrderEntity> DeleteAsync(int id)
        {
            using var con = _dbcontextDapper.OpenConnection();
            var order = await con.GetAsync<OrderEntity>(id);

            order.DateDeleted = DateTime.Now;

            await con.UpdateAsync(order);

            return order;
        }




        public async Task<IEnumerable<GetAllOrderDto>> GetByStatus(Status status)
        {
            using var conn = _dbcontextDapper.OpenConnection();
            return await conn.QueryAsync<GetAllOrderDto>(@"SELECT * FROM [Order] where Status = @Status",
                new { Status = status });
        }

        public async Task<IEnumerable<GetAllOrderDto>> SortAddress(string address)
        {
            using var conn = _dbcontextDapper.OpenConnection();
            return await conn.QueryAsync<GetAllOrderDto>(@"SELECT * FROM [Order] WHERE [Address]  like '%' + @Address + '%' ",
                new { Address = address });
        }

        public async Task<IEnumerable<SortByDateDto>> SortByDate(SortDate sortDate)
        {
            using var conn = _dbcontextDapper.OpenConnection();
            return await conn.QueryAsync<SortByDateDto>(@"SELECT * FROM [Order]");


        }

        public async Task<GetAllOrderDto> EditStatus(EditOrderStatusDto dto)
        {
            using var conn = _dbcontextDapper.OpenConnection();

            return await conn.QueryFirstOrDefaultAsync<GetAllOrderDto>(@" begin update  [Order] set Status = @Status where UserId = @UserId and Id = @Id   
                                        begin
                                        select Quantity, [Address], Phone, PaymentType, TotalPrice,  ShippingPrice, DeliveryId,  [Status]
							                from [Order]  where [Order].Id = @Id
                                        end
                                    end 
                                    ", dto);
        }

        public async Task<GetAllOrderDto> CreateOrder(CreateOrderDto dto, PaymentType paymentType)
        {
            using var conn = _dbcontextDapper.OpenConnection();

            return await conn.QueryFirstOrDefaultAsync<GetAllOrderDto>("createOrderProc",
                new
                {
                    UserId = dto.UserId,
                    Address = dto.Address,
                    Phone = dto.Phone,
                    PaymentType = paymentType,
                    Status = Status.active 
                }
                ,
                commandType: CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<GetAllOrderDto>> GetOrders(int userId)
        {
            using var conn = _dbcontextDapper.OpenConnection();
            return await conn.QueryAsync<GetAllOrderDto>(@"SELECT * FROM [Order] WHERE UserId = @UserId ",
                new { UserId = userId });
        }
    }
    
}
