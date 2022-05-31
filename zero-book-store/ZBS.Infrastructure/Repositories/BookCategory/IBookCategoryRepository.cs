using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Repositories.BookCategory.CrudModels;

namespace ZBS.Infrastructure.Repositories.BookCategory
{
    public interface IBookCategoryRepository
    {
        Task<IEnumerable<BookCategoryEntity>> GetAllAsync();
        Task<BookCategoryEntity> GetByIdAsync(int id);
        Task<BookCategoryEntity> CreateAsync(CreateBookCategoryModel entity);
        Task<BookCategoryEntity> UpdateAsync(UpdateBookCategoryModel entity);
        Task<BookCategoryEntity> DeleteAsync(int id);
    }
}
