using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.Repositories.Ratings.Dto;

namespace ZBS.Infrastructure.Repositories.Ratings
{

    public class RatingRepository : IRatingRepository
    {

        private readonly DbcontextDapper _dbcontextDapper;
        private readonly DataBaseContext _context;
        public RatingRepository(DbcontextDapper dbcontextDapper, DataBaseContext context)
        {
            _dbcontextDapper = dbcontextDapper;
            _context = context;
        }

        public async Task<GetBookRatingDto> AddRating(AddRatingDto dto)
        {
            using var conn = _dbcontextDapper.OpenConnection();

            return await conn.QueryFirstOrDefaultAsync<GetBookRatingDto>("AddRatingProc",
                new
                {
                    Rating = dto.Rating,
                    UserId = dto.UserId,
                    Id = dto.Id
                }
                ,
                commandType: CommandType.StoredProcedure

                );
             

        }

        public async Task<IEnumerable<GetBookRatingDto>> GetByRating(int rating)
        {
            using var conn = _dbcontextDapper.OpenConnection();
            return await conn.QueryAsync<GetBookRatingDto>(@" select avg(r.Rating) [Rating] , b.Title , bc.[Name][Genre],
                    b.Price, b.DateofPublish, b.Publisher, b.NumberOfPages, b.[Description], b.[Language]
                    from BookCategory bc inner join Book b on bc.Id = b.CategoryId
                    inner join [Ratings] r on r.Id = b.Id
                    group by b.Title , bc.[Name], b.Price, b.DateofPublish, b.Publisher, b.NumberOfPages, b.[Description], b.[Language]
                    having avg([Rating]) >= @Rating
            ", new { Rating = rating });
        }

        public async Task<IEnumerable<GetBookRatingDto>> GetByTitle(string title)
        {
            using var conn = _dbcontextDapper.OpenConnection();
            return await conn.QueryAsync<GetBookRatingDto>(@" begin  select avg(r.Rating) [Rating] , Book.Title [Title] , bc.[Name] [Genre],
                            Book.Price, Book.DateofPublish, Book.Publisher,Book.NumberOfPages, Book.[Description], Book.[Language]
                            from BookCategory bc  inner join Book  on bc.Id = Book.CategoryId  inner join [Ratings] r on r.Id =Book.Id
				            where [Title]  like '%' + @Title + '%' 
                            group by  Book.Title , bc.[Name], Book.Price, Book.DateofPublish, Book.Publisher, Book.NumberOfPages, Book.[Description], Book.[Language]
            end                     
            ", new { Title = title });
        }

        public async Task<IEnumerable<GetBookRatingDto>> GetPopularBooks()
        {
            using var conn = _dbcontextDapper.OpenConnection();
            return await conn.QueryAsync<GetBookRatingDto>(@"  select avg(r.Rating) [Rating] , b.Title , bc.[Name] [Genre],
                b.Price, b.DateofPublish, b.Publisher, b.NumberOfPages, b.[Description], b.[Language]
                from BookCategory bc  inner join Book b on bc.Id = b.CategoryId  inner join [Ratings] r on r.Id = b.Id
                group by  b.Title , bc.[Name], b.Price, b.DateofPublish, b.Publisher, b.NumberOfPages, b.[Description], b.[Language]
                order by [Rating] desc
                ");

        }

    }
}
