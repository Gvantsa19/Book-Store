using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Application.CurrencyExchange;
using ZBS.Application.Services.PaymentServ.Dtoes;
using ZBS.Infrastructure.Models.Enum;
using ZBS.Infrastructure.Repositories.Orders;
using ZBS.Infrastructure.Repositories.Orders.CrudModels;
using ZBS.Infrastructure.Repositories.PaymentHistory;
using ZBS.Infrastructure.Repositories.User;
using ZBS.Shared.Exceptions;

namespace ZBS.Application.Services.PaymentServ
{
    public class PaymentService:IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly CurrencyExchangeRate _currencyExchangeRate;
        private readonly IPaymentHistoryRepository _paymentHistoryRepository;

        public PaymentService(IConfiguration configuration,IOrderRepository orderRepository
            ,IUserRepository userRepository, CurrencyExchangeRate currencyExchangeRate
            ,IPaymentHistoryRepository paymentHistoryRepository)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _currencyExchangeRate = currencyExchangeRate;
            _paymentHistoryRepository = paymentHistoryRepository;
        }

        public async Task<PaymentDetailsDto> CreatePayAsync(CardInfoDto cardInfo,UserOrderDto userOrderDto)
        {
            var checkedTax = await CheckTaxAsync(userOrderDto);

            if (cardInfo == null)
            {
                throw new NotValidCardException(string.Format("Not Valid Credit Card info"));
            }
            var order = await _orderRepository.GetByIdAsync(userOrderDto.OrderId);

            int paymentType;
            int.TryParse(order.PaymentType, out paymentType);

            if (paymentType != (int)PaymentType.card)
            {
                throw new PaymentException(string.Format("Payment Type is not valid"));
            }

            var IsPaid =await PayAsync(cardInfo, (double)checkedTax.Tax, checkedTax.Currency);

            if (!IsPaid)
            {
                throw new PaymentException(string.Format("transaction not completed"));
            }

            var orderStatusUpdate = new EditOrderStatusDto
            {
                UserId = userOrderDto.UserId,
                Id = order.Id,
                status = Status.pending
            };
            await _orderRepository.EditStatus(orderStatusUpdate);

            var paymentHistory = new PaymentHistoryEntity
            {
                PaymentType = order.PaymentType.ToString(),
                UserId = userOrderDto.UserId,
                Amount = checkedTax.Tax,
                Currency = checkedTax.Currency,
                DateOfPayment = DateTime.Now,
                DateCreated = DateTime.Now
            };
            await _paymentHistoryRepository.CreateAsync(paymentHistory);

            var details = new PaymentDetailsDto
            {
                UserId = userOrderDto.UserId,
                PaymentType = order.PaymentType,
                Currency = checkedTax.Currency,
                Amount = checkedTax.Tax,
                DateOfPayment = DateTime.Now
            };

            return details;
        }

        public async Task<CheckTaxDto> CheckTaxAsync(UserOrderDto userOrderDto)
        {
            if (userOrderDto == null)
            {
                throw new PaymentException(string.Format("No data"));
            }

            var user = await _userRepository.GetUserByIdAsync(userOrderDto.UserId);
            if (user == null || user.DateDeleted != null)
            {
                throw new UserNotFoundException(string.Format("User Not Found"));
            }

            var order = await _orderRepository.GetByIdAsync(userOrderDto.OrderId);

            if(order == null || order.UserId != userOrderDto.UserId)
            {
                throw new PaymentException(string.Format("Order Not Found"));
            }
            
            int orderStatus;
            int.TryParse(order.Status, out orderStatus);

            if(orderStatus != ((int)Status.active))
            {
                throw new PaymentException(string.Format("Order Not Found"));
            }

            decimal exchangeRate;

            if (userOrderDto.Currency == Currency.GEL)
            {
                exchangeRate = 1;
            }
            else
            {
                exchangeRate = (decimal)await _currencyExchangeRate.ExchangeRate(userOrderDto.Currency.ToString());
            }

            var CheckedTax = new CheckTaxDto
            {
                OrderId = userOrderDto.OrderId,
                Currency = userOrderDto.Currency,
                Tax = order.TotalPrice / exchangeRate
            };

            return CheckedTax;
        }

        public async Task<bool> PayAsync(CardInfoDto cardInfoDto,double amount,Currency currency)
        {
            try { 
            var opttoken = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = cardInfoDto.CardNumber,
                    ExpMonth = cardInfoDto.Month,
                    ExpYear = cardInfoDto.Year,
                    Cvc = cardInfoDto.CVC.ToString()
                }
            };

            var tokenService = new TokenService();
            Token stripeToken = await tokenService.CreateAsync(opttoken);

            var options = new ChargeCreateOptions
            {
                Amount = (long?)(amount*100),
                Currency = currency.ToString(),
                Description = "transaction",
                Source = stripeToken.Id
            };

            var Service = new ChargeService();
            Charge charge = await Service.CreateAsync(options);


            return charge.Paid;
            }
            catch (Exception ex)
            {
                throw new NotValidCardException(string.Format("Not Valid Credit Card info"));
            }
        }

        public async Task<IEnumerable<PaymentHistoryEntity>> GetPaymentHistoryAsync(int id, int currentPageNumber, int pageSize)
        {
            return await _paymentHistoryRepository.GetByUserIdAsync(id, currentPageNumber, pageSize);
        }

    }
}
