using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.Services.PaymentServ.Dtoes;
using ZBS.Infrastructure.Models.Enum;
using ZBS.Infrastructure.Repositories.PaymentHistory;

namespace ZBS.Application.Services.PaymentServ
{
    public interface IPaymentService
    {
        public Task<bool> PayAsync(CardInfoDto cardInfoDto, double amount, Currency currency);
        Task<CheckTaxDto> CheckTaxAsync(UserOrderDto userOrderDto);
        Task<PaymentDetailsDto> CreatePayAsync(CardInfoDto cardInfo, UserOrderDto userOrderDto);
        Task<IEnumerable<PaymentHistoryEntity>> GetPaymentHistoryAsync(int id, int currentPageNumber, int pageSize);
    }
}
