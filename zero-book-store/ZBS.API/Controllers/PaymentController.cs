using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZBS.API.Controllers.Models;
using ZBS.Application.Services.LoggedInUserServ;
using ZBS.Application.Services.PaymentServ;
using ZBS.Application.Services.PaymentServ.Dtoes;
using ZBS.Infrastructure.Repositories.PaymentHistory;

namespace ZBS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PaymentController:BaseController
    {
        private IPaymentService _paymnetService;
        private ILoggedInUserService _loggedInUserService;

        public PaymentController(IPaymentService paymentService, ILoggedInUserService loggedInUserService)
        {
            _paymnetService = paymentService;
            _loggedInUserService = loggedInUserService;
        }

        [HttpPost("Pay")]
        public async Task<PaymentDetailsDto> Pay(UserOrderModel item, CardInfoDto cardInfoDto)
        {
            var dto = new UserOrderDto
            {
                OrderId = item.OrderId,
                Currency = item.Currency,
                UserId = _loggedInUserService.GetUserId()
            };

            return await _paymnetService.CreatePayAsync(cardInfoDto, dto);
        }

        [HttpGet("MyPaymentHistory")]
        public async Task<IEnumerable<PaymentHistoryEntity>> PaymentHistoryAsync(int currentPageNumber, int pageSize)
        {
            return await _paymnetService.GetPaymentHistoryAsync(_loggedInUserService.GetUserId(), currentPageNumber, pageSize);
        }

        [HttpGet("CheckTax")]
        public async Task<CheckTaxDto> CheckTaxAsync(UserOrderModel item)
        {
            var dto = new UserOrderDto
            {
                OrderId = item.OrderId,
                Currency = item.Currency,
                UserId=_loggedInUserService.GetUserId()
            };

            return await _paymnetService.CheckTaxAsync(dto);
        }

    }
}
