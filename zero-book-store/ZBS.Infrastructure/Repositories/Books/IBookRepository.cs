using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models;
using ZBS.Infrastructure.Repositories.Books.CRUDmodels;

namespace ZBS.Infrastructure.Repositories.Books
{
    public interface IBookRepository
    {
        Task<IEnumerable<GetBookModel>> GetAllAsync(int currentPageNumber, int pageSize);
        Task<GetBookModel> GetByIdAsync(int id);
        Task<BookEntity> CreateAsync(CreateBookModel entity);
        Task<BookEntity> UpdateAsync(UpdateBookModel entity);
        Task<BookEntity> DeleteById(int id);
        Task<IEnumerable<BookEntity>> SearchBookAsync(FilterBookDto filterBookDto);
        Task<AuthorBookDto> AddAuthorToBook(CreateAuthorBookDto createAuthorBookDto);
    }
}
