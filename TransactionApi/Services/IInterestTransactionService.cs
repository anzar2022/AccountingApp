using AccountDatabase.Entities;
using static TransactionApi.Dtos.InterestEMIDto;

namespace TransactionApi.Services
{
    public interface IInterestTransactionService
    {
        Task<IEnumerable<GetInterestEMIDto>> GetInterestTransactionAsync();
        Task<List<InterestEMI>> GetInterestTransactionByAccountIdAsync(Guid accountId);
        Task<InterestEMI> GetInterestEMIByTransactionIdAsync(Guid transactionId);
        Task<GetInterestEMIDto> GetInterestTransactionByIdAsync(Guid Id);
    }
}
