using AccountDatabase.Data;
using AccountDatabase.Entities;
using AccountDatabase.Repositories;

namespace Master.Repositories
{
    public class MasterRepository : RepositoryBase<InterestMaster>, IMasterRepository
    {
        private ILogger<AccountingAppDBContext> _logger;
        public MasterRepository(AccountingAppDBContext accountingAppDBContext, ILogger<AccountingAppDBContext> logger) : base(accountingAppDBContext)
        {
            //   _context = accountingAppDBContext ?? throw new ArgumentNullException(nameof(accountingAppDBContext));
            _logger = logger;
        }

    }
}
