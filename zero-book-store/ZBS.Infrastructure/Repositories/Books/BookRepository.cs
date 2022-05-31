using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.Models;
using ZBS.Infrastructure.Repositories.Books.CRUDmodels;
using ZBS.Shared.Helpers;

namespace ZBS.Infrastructure.Repositories.Books
{
    public class BookRepository : IBookRepository
    {
        private readonly DbcontextDapper _dbcontextDapper;

        public BookRepository(DbcontextDapper dbcontextDapper)
        {
            _dbcontextDapper = dbcontextDapper;
        }
        public async Task<BookEntity> CreateAsync(CreateBookModel entity)
        {
            using var connection = _dbcontextDapper.OpenConnection();
            var book = new BookEntity
            {
                Title = entity.Title,
                CategoryId = entity.CategoryId,
                Price = entity.Price,
                Quantity = entity.Quantity,
                DateofPublish = entity.DateofPublish,
                Publisher = entity.Publisher,
                NumberOfPages = entity.NumberOfPages,
                Description = entity.Description,
                Language = Models.Enum.Language.Eng,
                DateCreated = DateTime.Now
            };

            var id = await connection.InsertAsync<int, BookEntity>(book);
            book.Id = id;
            return book;
        }

        public async Task<BookEntity> DeleteById(int id)
        {
            using var connection = _dbcontextDapper.OpenConnection();
            var book = await connection.GetAsync<BookEntity>(id);
            book.DateDeleted= DateTime.Now;
            await connection.UpdateAsync(book);

            return book;
        }

        public async Task<IEnumerable<GetBookModel>> GetAllAsync(int currentPageNumber, int pageSize)
        {
            using var con = _dbcontextDapper.OpenConnection();
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            string sql = @"SELECT b.[Id] ,b.[Title] as Title,b.[CategoryId],bc.[Name] as CategoryName ,b.[Price] as Price,b.[Quantity] as Quantity ,b.[DateofPublish] as DateofPublish ,b.[Publisher] as Publisher
                              ,b.[NumberOfPages] as NumberOfPages  ,b.[Description] as Description
                              ,b.[Language] as Language  ,a.Id as AuthorId
	                          ,a.FirstName as AuthorFirstName ,a.LastName as AuthorLastName
                              ,b.[DateCreated] as DateCreated
                              ,b.[DateUpdated] as DateUpdated
                              ,b.[DateDeleted] as DateDeleted
                          FROM [dbo].[Book] b
                          left join BookCategory bc  on bc.Id=b.CategoryId left join BookAuthor ba on ba.BookId=b.Id left join Author a on ba.AuthorId=a.Id

                          SELECT b.[Id] ,b.[Title] as Title,b.[CategoryId] ,bc.[Name] as CategoryName,b.[Price] as Price,b.[Quantity] as Quantity ,b.[DateofPublish] as DateofPublish
                              ,b.[Publisher] as Publisher,b.[NumberOfPages] as NumberOfPages
                              ,b.[Description] as Description ,b.[Language] as Language
	                          ,a.Id as AuthorId,a.FirstName as AuthorFirstName ,a.LastName as AuthorLastName ,b.[DateCreated] as DateCreated
                              ,b.[DateUpdated] as DateUpdated,b.[DateDeleted] as DateDeleted
                          FROM [dbo].[Book] b
                          left join BookCategory bc  on bc.Id=b.CategoryId left join BookAuthor ba on ba.BookId=b.Id left join Author a on ba.AuthorId=a.Id
                            ORDER BY Id
                            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            //var list = await con.QueryAsync<GetBookModel>(sql);
            var reader = con.QueryMultiple(sql, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<GetBookModel> allTodos = reader.Read<GetBookModel>().ToList();

            var result = new PagingResponseModel<List<GetBookModel>>(allTodos, count, currentPageNumber, pageSize);
            return result.Data;
     
        }

        public async Task<GetBookModel> GetByIdAsync(int id)
        {
            using var con=_dbcontextDapper.OpenConnection();
            
            var book= await con.QueryFirstOrDefaultAsync<GetBookModel>("SELECT * from GetBookByID(@Id)", new { Id = id });
            return book;
        }

        public async Task<IEnumerable<BookEntity>> SearchBookAsync(FilterBookDto filterBookDto)
        {
             using var con = _dbcontextDapper.OpenConnection();

            return await con.QueryAsync<BookEntity>(@"select * from BookSearch(@Title, @CategoryName)", new {Title=filterBookDto.Title, CategoryName=filterBookDto.CategoryName}); 
        }

        public async Task<BookEntity> UpdateAsync(UpdateBookModel entity)
        {
            using var connection = _dbcontextDapper.OpenConnection();
            var book = await connection.GetAsync<BookEntity>(entity.Id);
            book.Id= entity.Id;
            book.Title = entity.Title;
            book.CategoryId= entity.CategoryId;
            book.Price= entity.Price;
            book.Quantity= entity.Quantity;
            book.DateofPublish= entity.DateofPublish;
            book.Description= entity.Description;
            book.Language= Models.Enum.Language.Eng;
            book.DateCreated = book.DateCreated;
            book.DateUpdated = DateTime.Now;
            
            await connection.UpdateAsync(book);

            return book;
        }
         public async Task<AuthorBookDto> AddAuthorToBook(CreateAuthorBookDto createAuthorBookDto)
        {
            using var connection = _dbcontextDapper.OpenConnection();
            var authorBook = new AuthorBookDto
            {
                BookId = createAuthorBookDto.BookId,
                AuthorId = createAuthorBookDto.AuthorId,
                DateCreated = DateTime.Now
            };

            var id=await connection.InsertAsync<int,AuthorBookDto>(authorBook);
            authorBook.Id= id;
            return authorBook;
        }
    }
}
