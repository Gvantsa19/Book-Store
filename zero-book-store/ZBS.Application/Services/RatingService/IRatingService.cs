using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.Pagination;
using ZBS.Infrastructure.Repositories.Ratings.Dto;

namespace ZBS.Application.Services.RatingService
{
    public interface IRatingService
    {
        Task<GetBookRatingDto> AddRating(AddRatingDto dto);
        Task<IEnumerable<GetBookRatingDto>> GetByRating(int rating, PagingParameters pagingParameters);
        Task<IEnumerable<GetBookRatingDto>> GetByTitle(string title, PagingParameters pagingParameters);
        Task<IEnumerable<GetBookRatingDto>> GetPopularBooks(PagingParameters pagingParameters);
    }
}
