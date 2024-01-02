using AccountDatabase.Entities;
using Microsoft.AspNetCore.Mvc;
using static TransactionApi.Dtos.InterestEMIDto;

namespace TransactionApi.Services
{
    public interface IInterestTransactionService
    {
        Task<IEnumerable<GetInterestEMIDto>> GetInterestTransactionAsync();
        Task<List<InterestEMI>> GetInterestTransactionByAccountIdAsync(Guid accountId);
        Task<IEnumerable<GetInterestEMIDto>> GetInterestTransactionsByIdAsync(Guid transactionId);
        Task<InterestEMI> GetInterestEMIByTransactionIdAsync([FromBody] GenerateInterestEMIDto interestEmi);
        Task<List<InterestEMI>> GetInterestEMIForTransactionsAsync(string emiMonth);
        Task<InterestEMI> UpdateInterestTransactionPaymentAsync(UpdateInterestEMIDto updateDto);
        Task<GetInterestEMIDto> GetInterestTransactionByIdAsync(Guid Id);
        Task<bool> DeleteInterestEMIAsync(Guid Id);
    }
}
