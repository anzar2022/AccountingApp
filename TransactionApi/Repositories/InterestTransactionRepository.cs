using AccountDatabase.Data;
using AccountDatabase.Entities;
using AccountDatabase.Repositories;

namespace TransactionApi.Repositories
{
    public class InterestTransactionRepository : RepositoryBase<InterestEMI>, IInterestTransactionRepository
    {
        private ILogger<AccountingAppDBContext> _logger;
        public InterestTransactionRepository(AccountingAppDBContext accountingAppDBContext, ILogger<AccountingAppDBContext> logger) : base(accountingAppDBContext)
        {
            //   _context = accountingAppDBContext ?? throw new ArgumentNullException(nameof(accountingAppDBContext));
            _logger = logger;
        }
    }
}
