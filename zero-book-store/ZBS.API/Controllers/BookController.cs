using Microsoft.AspNetCore.Mvc;
using ZBS.Application.Services.BookServ;
using ZBS.Infrastructure.Repositories.Books;
using ZBS.Infrastructure.Repositories.Books.CRUDmodels;

namespace ZBS.API.Controllers
{
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("Search")]
        public Task<IEnumerable<BookEntity>> Search(FilterBookDto filterBookDto)
        {
            return _bookService.SearchBookAsync(filterBookDto);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetBookModel>>> GetAllBooks(int currentPageNumber, int pageSize)
        {
            var list = await _bookService.GetAllBook(currentPageNumber, pageSize);
            return Ok(list);
        }

    }
}
