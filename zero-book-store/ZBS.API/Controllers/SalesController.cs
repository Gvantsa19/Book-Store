using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZBS.Application.Services.SalesService;
using ZBS.Infrastructure.Repositories.Sales;
using ZBS.Infrastructure.Repositories.Sales.CrudModels;

namespace ZBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesRepository salesRepository;
        public SalesController(ISalesRepository salesRepository, ISalesService salesService)
        {
            this.salesRepository = salesRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<SalesEntity>> GetAllAsync()
        {
            return await salesRepository.GetAllAsync();
        }
        [Authorize(Roles="Admin")]
        [HttpPost("Create")]
        public async Task<SalesEntity> CreateAsync(CreateSalesModel entity)
        {
            return await salesRepository.CreateAsync(entity);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}")]
        public async Task<SalesEntity> GetByIdAsync(int Id)
        {
            return await salesRepository.GetByIdAsync(Id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update")]
        public async Task<SalesEntity> UpdateAsync(UpdateSalesModel entity)
        {
            return await salesRepository.UpdateAsync(entity);
        }

    }
}
