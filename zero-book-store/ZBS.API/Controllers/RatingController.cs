using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using ZBS.API.Models.Rating;
using ZBS.Application.Pagination;
using ZBS.Application.Services.LoggedInUserServ;
using ZBS.Application.Services.RatingService;
using ZBS.Infrastructure.Repositories.Ratings.Dto;

namespace ZBS.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        private ILoggedInUserService _loggedInUserService;
        public RatingController(IRatingService ratingService, ILoggedInUserService loggedInUserService)
        {
            _ratingService = ratingService;
            _loggedInUserService = loggedInUserService;
        }
        [Authorize]
        [HttpPost("AddRating")]
        public async Task<ActionResult<GetBookRatingDto>> AddRating([FromQuery] AddRatingModel model)
        {
            if (model != null)
            {
                var dto = new AddRatingDto()
                {

                    Rating = model.Rating,
                    UserId = _loggedInUserService.GetUserId(),
                    Id = model.Id
                };
                return Ok(await _ratingService.AddRating(dto));
            }
            return BadRequest();
        }

        [HttpGet("{rating}")]
        public async Task<ActionResult<IEnumerable<GetBookRatingDto>>> GetByRating(int rating, [FromQuery] PagingParameters pagingParameters)
        {
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(await _ratingService.GetByRating(rating, pagingParameters));
        }

        [HttpGet("GetPopularBooks")]
        public async Task<ActionResult<IEnumerable<GetBookRatingDto>>> GetPopularBooks([FromQuery] PagingParameters pagingParameters)
        {
            return Ok(await _ratingService.GetPopularBooks(pagingParameters));
        }
    }
}
