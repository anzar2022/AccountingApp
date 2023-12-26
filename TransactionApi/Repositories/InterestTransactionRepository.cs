using AccountDatabase.Data;
using AccountDatabase.Entities;
using AccountDatabase.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TransactionApi.Repositories
{
    public class InterestTransactionRepository : RepositoryBase<InterestEMI>, IInterestTransactionRepository
    {
        private ILogger<AccountingAppDBContext> _logger;
    
        public InterestTransactionRepository(AccountingAppDBContext accountingAppDBContext,  ILogger<AccountingAppDBContext> logger) : base(accountingAppDBContext)
        {
            //   _context = accountingAppDBContext ?? throw new ArgumentNullException(nameof(accountingAppDBContext));
            _logger = logger;
            _accountingAppDBContext = accountingAppDBContext;
        }

        public async Task<InterestEMI> GetInterestTransactionByTransactionId(Guid transactionId)
        {
            var transaction = await _accountingAppDBContext.InterestEMIs.FirstOrDefaultAsync(e => e.TransactionId == transactionId);
            return transaction;
        }

        public async Task<InterestEMI> GetInterestTransactionByPrincipalAmountAsync(double principalAmount)
        {
            var transaction = await _accountingAppDBContext.InterestEMIs.FirstOrDefaultAsync(e => e.PrincipalAmount == principalAmount);
            return transaction;
        }
    }
}
