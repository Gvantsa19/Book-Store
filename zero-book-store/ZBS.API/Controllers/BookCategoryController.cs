using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZBS.Infrastructure.Repositories.BookCategory;
using ZBS.Infrastructure.Repositories.BookCategory.CrudModels;

namespace ZBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class BookCategoryController : ControllerBase
    {
        private readonly IBookCategoryRepository bookCategoryRepository;

        public BookCategoryController(IBookCategoryRepository bookCategoryRepository)
        {
            this.bookCategoryRepository = bookCategoryRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<BookCategoryEntity>> GetAllAsync()
        {
            return await bookCategoryRepository.GetAllAsync();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<BookCategoryEntity> CreateAsync(CreateBookCategoryModel entity)
        {
            return await bookCategoryRepository.CreateAsync(entity);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}")]
        public async Task<BookCategoryEntity> GetByIdAsync(int Id)
        {
            return await bookCategoryRepository.GetByIdAsync(Id);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<BookCategoryEntity> UpdateAsync(UpdateBookCategoryModel entity)
        {
            return await bookCategoryRepository.UpdateAsync(entity);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<BookCategoryEntity> DeleteAsync(int id)
        {
            return await bookCategoryRepository.DeleteAsync(id);
        }

    }
}
