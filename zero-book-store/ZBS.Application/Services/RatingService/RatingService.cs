using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.Pagination;
using ZBS.Infrastructure.Repositories.Ratings;
using ZBS.Infrastructure.Repositories.Ratings.Dto;

namespace ZBS.Application.Services.RatingService
{

    public class RatingService : IRatingService
    {
        private IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingReposity)
        {
            _ratingRepository = ratingReposity;
        }

        public async Task<GetBookRatingDto> AddRating(AddRatingDto dto)
        {
            return await _ratingRepository.AddRating(dto);
        }

        public async Task<IEnumerable<GetBookRatingDto>> GetByRating(int rating, PagingParameters pagingParameters)
        {
            var t =  await _ratingRepository.GetByRating(rating);
            
            return await Task.FromResult(PageList<GetBookRatingDto>.GetPageList(t.OrderBy(x => x.Price), pagingParameters.PageNumber, pagingParameters.PageSize));

        }

        public async Task<IEnumerable<GetBookRatingDto>> GetByTitle(string title, PagingParameters pagingParameters)
        {
            var t =  await _ratingRepository.GetByTitle(title);

            return await Task.FromResult(PageList<GetBookRatingDto>.GetPageList(t.OrderBy(x => x.Price), pagingParameters.PageNumber, pagingParameters.PageSize));

        }

        public async Task<IEnumerable<GetBookRatingDto>> GetPopularBooks(PagingParameters pagingParameters)
        {
            var t =  await _ratingRepository.GetPopularBooks();

            return await Task.FromResult(PageList<GetBookRatingDto>.GetPageList(t.OrderBy(x => x.Price), pagingParameters.PageNumber, pagingParameters.PageSize));

        }


    }
}
