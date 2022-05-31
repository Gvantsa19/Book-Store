using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Repositories.Ratings.Dto;

namespace ZBS.Infrastructure.Repositories.Ratings
{
    public interface IRatingRepository
    {
        Task<GetBookRatingDto> AddRating(AddRatingDto dto);
        Task<IEnumerable<GetBookRatingDto>> GetByRating(int rating);
        Task<IEnumerable<GetBookRatingDto>> GetByTitle(string title);
        Task<IEnumerable<GetBookRatingDto>> GetPopularBooks();
    }
}
