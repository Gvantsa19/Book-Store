using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.Repositories.BookCategory.CrudModels;

namespace ZBS.Infrastructure.Repositories.BookCategory
{
    public class BookCategoryRepository : IBookCategoryRepository 
    {

        private readonly DbcontextDapper _dbcontextDapper;

        public BookCategoryRepository(DbcontextDapper dbcontextDapper)
        {
            _dbcontextDapper = dbcontextDapper;
        }

        public async Task<BookCategoryEntity> CreateAsync(CreateBookCategoryModel entity)
        {
            using var connection = _dbcontextDapper.OpenConnection();
            var bookCategory = new BookCategoryEntity
            {
                Name = entity.Name,
                DateCreated = DateTime.Now
            };

            var id = await connection.InsertAsync<int, BookCategoryEntity>(bookCategory);
            bookCategory.Id = id;

            return bookCategory;
        }


        public async Task<IEnumerable<BookCategoryEntity>> GetAllAsync() {
            using var con = _dbcontextDapper.OpenConnection();
            var cmd = @"SELECT * FROM dbo.BookCategory";

            return await con.QueryAsync<BookCategoryEntity>(cmd);
            
        }


        public async Task<BookCategoryEntity> GetByIdAsync(int id)
        {
            using var con = _dbcontextDapper.OpenConnection();

            return await con.QueryFirstOrDefaultAsync<BookCategoryEntity>(@"
                        select * from [dbo].[BookCategory] where Id = @Id", new { Id = id });
        }

        public async Task<BookCategoryEntity> UpdateAsync(UpdateBookCategoryModel entity)
        {
            using var con = _dbcontextDapper.OpenConnection();
            var bookCategory = await con.GetAsync<BookCategoryEntity>(entity.ID);

            bookCategory.Id = entity.ID;
            bookCategory.Name = entity.Name;
            bookCategory.DateUpdated = DateTime.Now;
            await con.UpdateAsync(bookCategory);

            return bookCategory;
        }


        public async Task<BookCategoryEntity> DeleteAsync(int id)
        {
            using var con = _dbcontextDapper.OpenConnection();
            var bookCategory = await con.GetAsync<BookCategoryEntity>(id);

            bookCategory.DateDeleted = DateTime.Now;

            await con.UpdateAsync(bookCategory);

            return bookCategory;
        }
    }
}
