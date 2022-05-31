using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Repositories.Sales;
using ZBS.Infrastructure.Repositories.Sales.CrudModels;
using ZBS.Shared.Exceptions;

namespace ZBS.Application.Services.SalesService
{
    public class SalesService : ISalesService
    {
        private ISalesRepository _salesRepository;
        public SalesService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }
        public async Task Create(CreateSalesModel entity)
        {
            await _salesRepository.CreateAsync(entity);
        }

        public async Task<GetSalesModel> GetById(int id)
        {
            var sale = await _salesRepository.GetByIdAsync(id);

            if (sale == null)
            {
                throw new SalesException(string.Format("Sale not found"));
            }
            return new GetSalesModel
            {
                Id = sale.Id,
                CategoryID = sale.CategoryID,
                Percent = sale.Percent
            };
        }
    }
}
