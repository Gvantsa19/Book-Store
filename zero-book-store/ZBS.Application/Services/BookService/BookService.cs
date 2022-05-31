using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.Services.BookServ.Dtoes;
using ZBS.Infrastructure.Repositories.Authors;
using ZBS.Infrastructure.Repositories.Books;
using ZBS.Infrastructure.Repositories.Books.CRUDmodels;
using ZBS.Shared.Exceptions;

namespace ZBS.Application.Services.BookServ
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;
        private readonly ILogger<BookService> logger;

        public BookService(IBookRepository bookRepository,IAuthorRepository authorRepository, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            this.logger = logger;
        }
        public async Task Create(CreateBookWithAuthorDto createBookWithAuthorDto)
        {
            var createBookModel = new CreateBookModel
            {
                Title = createBookWithAuthorDto.Title,
                CategoryId= createBookWithAuthorDto.CategoryId,
                Price = createBookWithAuthorDto.Price,
                Quantity=createBookWithAuthorDto.Quantity,
                DateofPublish=createBookWithAuthorDto.DateofPublish,
                NumberOfPages=createBookWithAuthorDto.NumberOfPages,
                Description=createBookWithAuthorDto.Description,
                Language=createBookWithAuthorDto.Language.ToString(),
                Publisher=createBookWithAuthorDto.Publisher
            };

            var book = await _bookRepository.CreateAsync(createBookModel);

            if (createBookWithAuthorDto.AuthorId != null)
            {    
              var author = await _authorRepository.GetByIdAsync((int)createBookWithAuthorDto.AuthorId);

              if(author != null || author.DateDeleted == null)
              {
                    var authBookDto = new CreateAuthorBookDto
                    {
                        AuthorId = (int)createBookWithAuthorDto.AuthorId,
                        BookId = book.Id
                    };
                
                await _bookRepository.AddAuthorToBook(authBookDto);
              }
            }
        }

        public async Task DeleteBook(int id)
        {
            var  book = await _bookRepository.GetByIdAsync(id);

            if( book == null || book.DateDeleted != null)
            {
                logger.LogInformation("book not found");
            }
            await _bookRepository.DeleteById(id);
        }

        public async Task EditBookQuantity(EditBookQuantityDto entity)
        {
            var book =await _bookRepository.GetByIdAsync(entity.Id);
            if (book.DateDeleted != null || book==null) 
            {
                throw new BookException(string.Format("Book not found"));
            }
            
            book.Quantity = entity.Quantity;

            var updateBook = new UpdateBookModel()
            {
                Id = book.Id,
                Title = book.Title,
                CategoryId = book.CategoryId,
                Price = book.Price,
                Quantity = book.Quantity,
                DateofPublish = book.DateofPublish,
                Publisher = book.Publisher,
                NumberOfPages = book.NumberOfPages,
                Description = book.Description,
                Language = Infrastructure.Models.Enum.Language.Eng,
            };

            await _bookRepository.UpdateAsync(updateBook);
        }

        public async Task<IEnumerable<GetBookModel>> GetAllBook(int currentPageNumber, int pageSize)
        {
            var list = await _bookRepository.GetAllAsync(currentPageNumber, pageSize);
            return list;
        }

        public async Task<GetBookModel> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null || book.DateDeleted != null)
            {
                throw new BookException(string.Format("Book not found"));
            }
            return book;
        }

        public async Task<IEnumerable<BookEntity>> SearchBookAsync(FilterBookDto entity)
        {
            var result = await _bookRepository.SearchBookAsync(entity);
            return result;
        }
    }
}
