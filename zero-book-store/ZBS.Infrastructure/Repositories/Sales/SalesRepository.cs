using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.Repositories.Sales.CrudModels;

namespace ZBS.Infrastructure.Repositories.Sales
{
    public class SalesRepository : ISalesRepository
    {
        private readonly DbcontextDapper _dbcontextDapper;

        public SalesRepository(DbcontextDapper dbcontextDapper)
        {
            _dbcontextDapper = dbcontextDapper;
        }


        public async Task<SalesEntity> CreateAsync(CreateSalesModel entity)
        {
            using var connection = _dbcontextDapper.OpenConnection();
            var sales = new SalesEntity
            {
                CategoryID = entity.CategoryID,
                Percent = entity.Percent,
                DateCreated = DateTime.Now
            };

            var id = await connection.InsertAsync<int, SalesEntity>(sales);
            sales.Id = id;

            return sales;

        }

        public async Task<IEnumerable<SalesEntity>> GetAllAsync()
        {
            using var con = _dbcontextDapper.OpenConnection();
            var cmd = @"SELECT * FROM dbo.Sales";

            return await con.QueryAsync<SalesEntity>(cmd);
        }

        public async Task<SalesEntity> GetByIdAsync(int id)
        {
            using var con = _dbcontextDapper.OpenConnection();

            return await con.QueryFirstOrDefaultAsync<SalesEntity>(@"
                        select * from [dbo].[Sales] where Id = @Id" , new { Id = id });
        }

        public async Task<SalesEntity> UpdateAsync(UpdateSalesModel entity)
        {
            using var con = _dbcontextDapper.OpenConnection();
            var sales = await con.GetAsync<SalesEntity>(entity.Id);

            sales.Id = entity.Id;
            sales.CategoryID = entity.CategoryID;
            sales.Percent = entity.Percent;
            sales.DateUpdated = DateTime.Now;
            await con.UpdateAsync(sales);

            return sales;
        }
    }
}
