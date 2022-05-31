using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZBS.Application.Services.AuthorService;
using ZBS.Infrastructure.Repositories.Authors;
using ZBS.Infrastructure.Repositories.Authors.CrudModels;

namespace ZBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository authorRepository;
        public AuthorController(IAuthorRepository authorRepository, IAuthorService authorService)
        {
            this.authorRepository = authorRepository;
        }

        [HttpGet("GetAuthors")]
        public async Task<IEnumerable<AuthorEntity>> GetAllAsync()
        {
            return await authorRepository.GetAllAsync();
        }

        [HttpPost("CreateAuthor")]
        public async Task<AuthorEntity> CreateAsync(CreateAuthorModel entity)
        {
            return await authorRepository.CreateAsync(entity);
        }

        [HttpGet("{Id}")]
        public async Task<AuthorEntity> GetByIdAsync(int Id)
        {
            return await authorRepository.GetByIdAsync(Id);
        }


        [HttpPut("update")]
        public async Task<AuthorEntity> UpdateAsync(UpdateAuthorModel entity)
        {
            return await authorRepository.UpdateAsync(entity);
        }
    }
}
