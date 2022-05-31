using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.Services.BookServ.Dtoes;
using ZBS.Infrastructure.Repositories.Books;
using ZBS.Infrastructure.Repositories.Books.CRUDmodels;

namespace ZBS.Application.Services.BookServ
{
    public interface IBookService
    {
        Task<IEnumerable<GetBookModel>> GetAllBook(int currentPageNumber, int pageSize);
        Task<GetBookModel> GetBookById(int id);
        Task Create(CreateBookWithAuthorDto createBookWithAuthorDto);
        Task EditBookQuantity(EditBookQuantityDto entity);
        Task DeleteBook(int id);
        Task<IEnumerable<BookEntity>> SearchBookAsync(FilterBookDto entity);
    }
}
