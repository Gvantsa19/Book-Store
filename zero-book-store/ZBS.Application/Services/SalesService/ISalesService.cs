using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Repositories.Sales.CrudModels;

namespace ZBS.Application.Services.SalesService
{
    public interface ISalesService
    {
        Task Create(CreateSalesModel entity);
        Task<GetSalesModel> GetById(int id);
    }
}
