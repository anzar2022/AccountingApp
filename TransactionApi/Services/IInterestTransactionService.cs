using AccountDatabase.Entities;
using Microsoft.AspNetCore.Mvc;
using static TransactionApi.Dtos.InterestEMIDto;

namespace TransactionApi.Services
{
    public interface IInterestTransactionService
    {
        Task<IEnumerable<GetInterestEMIDto>> GetInterestTransactionAsync();
        Task<List<InterestEMI>> GetInterestTransactionByAccountIdAsync(Guid accountId);
        Task<InterestEMI> GetInterestEMIByTransactionIdAsync([FromBody] GenerateInterestEMIDto interestEmi);
        Task<GetInterestEMIDto> GetInterestTransactionByIdAsync(Guid Id);
    }
}
