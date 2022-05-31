using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.Repositories.Cart.Dto;

namespace ZBS.Infrastructure.Repositories.Cart
{
    public class BasketRepository : IBasketRepository
    {
        private DbcontextDapper _dbcontextDapper;

        public BasketRepository(DbcontextDapper dbContextDapper)
        {
            _dbcontextDapper = dbContextDapper;
        }
        public async Task<GetAllItemsDto> AddToCart(AddBasketDto dto)
        {
            using var conn = _dbcontextDapper.OpenConnection();

            var result = await conn.QueryFirstOrDefaultAsync<GetAllItemsDto>("AddToBasketProc",
                new
                {
                    UserId = dto.UserId,
                    BookId = dto.BookId,
                    Quantity = dto.Quantity

                }
                ,
                commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task DeleteCart(int userId)
        {
            using var conn = _dbcontextDapper.OpenConnection();
            await conn.QueryFirstOrDefaultAsync(@" if exists(select UserId from Basket where UserId = @UserId and DateDeleted is null)
                begin
	                update Basket set DateDeleted = GETDATE() where Basket.UserId = @UserId

	                Update BasketBook set DateDeleted = GETDATE()
                end
            ", new { UserId = userId });
        }

        public async Task DeleteItem(DeleteItemDto dto)
        {
            using var conn = _dbcontextDapper.OpenConnection();

            await conn.QueryFirstOrDefaultAsync("DeleteItemProc", new
            {
                UserId = dto.UserId,
                BookId = dto.BookId
            },
            commandType: CommandType.StoredProcedure);
        }

        public async Task<GetAllItemsDto> EditItem(AddBasketDto dto)
        {
            using var conn = _dbcontextDapper.OpenConnection();

            var result = await conn.QueryFirstOrDefaultAsync<GetAllItemsDto>("editItemBasket",
               new
               {
                   UserId = dto.UserId,
                   BookId = dto.BookId,
                   Quantity = dto.Quantity

               }
               ,
               commandType: CommandType.StoredProcedure);
            return result;


             
        }

        public async Task<IEnumerable<GetAllItemsDto>> GetAllItems(int userId)
        {
            using var conn = _dbcontextDapper.OpenConnection();
            return await conn.QueryAsync<GetAllItemsDto>(@" select b.Title, BasketBook.Price,  BasketBook.Quantity
                from BasketBook
                inner join Basket on BasketBook.BasketId = Basket.Id
                inner join Book b on BasketBook.BookId = b.Id
                where Title in (select Title from Book k  where k.Id in 
	                 (select BookId from BasketBook  inner join Basket on BasketBook.BasketId = Basket.Id) and  Basket.UserId = @UserId and BasketBook.DateDeleted is null)
 
            ", new { UserId = userId });
        }


    }
}
