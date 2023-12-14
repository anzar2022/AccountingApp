using AccountApi.Data;
using AccountDatabase.Data;
using AccountDatabase.Entities;
using AccountDatabase.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Reflection;

namespace AccountApi.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository //: IAccountRepository
    {
      //  private AccountingAppDBContext _context;
        private ILogger<AccountingAppDBContext> _logger;

        public AccountRepository(AccountingAppDBContext accountingAppDBContext,ILogger<AccountingAppDBContext> logger) : base(accountingAppDBContext)
        {
         //   _context = accountingAppDBContext ?? throw new ArgumentNullException(nameof(accountingAppDBContext));
            _logger = logger;
        }

        //public AccountRepository(AccountingAppDBContext context, ILogger<AccountingAppDBContext> logger)
        //{
        //    _context = context ?? throw new ArgumentNullException(nameof(context));
        //    _logger = logger;
        //}

        //public async Task<IEnumerable<Account>> GetAccountsAsync()
        //{
        //    try
        //    {
        //        _logger.LogInformation($"{nameof(GetAccountsAsync)}");
        //        var accounts = await _context.Accounts.ToListAsync();

        //        return accounts;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"{nameof(ex)}");
        //        throw ex;
        //    }
        //}
        //public async Task<Account> GetAccountByIdAsync(Guid Id)
        //{
        //    try
        //    {
        //        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == Id);

        //        return account;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public async Task<Account> CreateAccountAsync(Account account)
        //{
        //    try
        //    {
        //        _context.Accounts.Add(account);
        //        await _context.SaveChangesAsync();
        //        return account;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public async Task<Account> UpdateAccountAsync(Account account)
        //{
        //    try
        //    {
        //        _context.Accounts.Update(account).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //        return account;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public async Task<bool> DeleteAccountAsync(Guid Id)
        //{
        //    try
        //    {
        //        var accountToDelete = await _context.Accounts.FindAsync(Id);
        //        if (accountToDelete == null)
        //        {
        //            return false;
        //        }

        //        _context.Accounts.Remove(accountToDelete);
        //        await _context.SaveChangesAsync();

        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public async Task<Account> GetSingleAccountByFilterAsync(Expression<Func<Account, bool>> filter)
        //{
        //    try
        //    {

        //        var filterAccount = await _context.Accounts.FirstOrDefaultAsync(filter);

        //        return filterAccount;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public async Task<IEnumerable<Account>> GetAccountsByFilterAsync(Expression<Func<Account, bool>> filter)
        //{

        //    try
        //    {
        //        var filterAcclounts = await _context.Accounts.Where(filter).ToListAsync();

        //        return filterAcclounts;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}
