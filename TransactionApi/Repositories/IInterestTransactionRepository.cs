using AccountDatabase.Entities;
using AccountDatabase.Repositories;
using static TransactionApi.Dtos.InterestEMIDto;

namespace TransactionApi.Repositories
{
    public interface IInterestTransactionRepository : IRepositoryBase<InterestEMI>
    {
        Task<InterestEMI> GetInterestTransactionByTransactionId(Guid transactionId);
    }
}
