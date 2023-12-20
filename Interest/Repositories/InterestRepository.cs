using AccountDatabase.Data;
using AccountDatabase.Entities;
using AccountDatabase.Repositories;

namespace Interest.Repositories
{
    public class InterestRepository : RepositoryBase<InterestMaster>, IInterestRepository
    {
        private ILogger<AccountingAppDBContext> _logger;
        public InterestRepository(AccountingAppDBContext accountingAppDBContext, ILogger<AccountingAppDBContext> logger) : base(accountingAppDBContext)
        {
            //   _context = accountingAppDBContext ?? throw new ArgumentNullException(nameof(accountingAppDBContext));
            _logger = logger;
        }
    }
}
