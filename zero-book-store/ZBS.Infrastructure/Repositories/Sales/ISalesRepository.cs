using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Repositories.Sales.CrudModels;

namespace ZBS.Infrastructure.Repositories.Sales
{
    public interface ISalesRepository
    {
        Task<IEnumerable<SalesEntity>> GetAllAsync();
        Task<SalesEntity> GetByIdAsync(int id);
        Task<SalesEntity> CreateAsync(CreateSalesModel entity);
        Task<SalesEntity> UpdateAsync(UpdateSalesModel entity);
    }
}
