namespace ZBS.Infrastructure.Repositories.PaymentHistory
{
    public interface IPaymentHistoryRepository
    {
        Task<PaymentHistoryEntity> CreateAsync(PaymentHistoryEntity paymentHistoryEntity);
        Task<IEnumerable<PaymentHistoryEntity>> GetByUserIdAsync(int id, int currentPageNumber, int pageSize);
    }
}