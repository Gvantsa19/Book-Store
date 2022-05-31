using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts;
using ZBS.Shared.Helpers;

namespace ZBS.Infrastructure.Repositories.PaymentHistory
{
    public class PaymentHistoryRepository: IPaymentHistoryRepository
    {
        private readonly DbcontextDapper _dbcontextDapper;

        public PaymentHistoryRepository(DbcontextDapper dbcontextDapper)
        {
            _dbcontextDapper = dbcontextDapper;
        }

        public async Task<PaymentHistoryEntity> CreateAsync(PaymentHistoryEntity paymentHistoryEntity)
        {
            using var con =_dbcontextDapper.OpenConnection();
            await con.InsertAsync<int,PaymentHistoryEntity>(paymentHistoryEntity);

            con.Dispose();
            return paymentHistoryEntity;
        }

        public async Task<IEnumerable<PaymentHistoryEntity>> GetByUserIdAsync(int id, int currentPageNumber, int pageSize)
        {
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            using var con=_dbcontextDapper.OpenConnection();
            var sql = @"SELECT [Id] ,[PaymentType],[UserId],[Amount],[Currency] ,[DateOfPayment] ,[DateCreated],[DateUpdated] ,[DateDeleted]
                      FROM [ZBS].[dbo].[PaymentHistory] where [UserId] = @Id

                      SELECT [Id],[PaymentType] ,[UserId],[Amount],[Currency],[DateOfPayment],[DateCreated],[DateUpdated],[DateDeleted]
                      FROM [ZBS].[dbo].[PaymentHistory]
                      where [UserId] = @Id ORDER BY Id OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = con.QueryMultiple(sql, new { Id = id, Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();
            List<PaymentHistoryEntity> allTodos = reader.Read<PaymentHistoryEntity>().ToList();

            var result = new PagingResponseModel<List<PaymentHistoryEntity>>(allTodos, count, currentPageNumber, pageSize);
            return result.Data;
        }
    }
}
