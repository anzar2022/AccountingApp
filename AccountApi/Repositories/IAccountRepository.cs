using AccountDatabase.Data;
using AccountDatabase.Entities;
using AccountDatabase.Repositories;
using System.Linq.Expressions;
using System.Reflection;

namespace AccountApi.Repositories
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        //Task<Account> CreateAccountAsync(Account account);
        //Task<bool> DeleteAccountAsync(Guid Id);
        //Task<Account> GetAccountByIdAsync(Guid Id);
        //Task<Account> GetSingleAccountByFilterAsync(Expression<Func<Account, bool>> filter);
        //Task<IEnumerable<Account>> GetAccountsAsync();
        //Task<IEnumerable<Account>> GetAccountsByFilterAsync(Expression<Func<Account, bool>> filter);
        //Task<Account> UpdateAccountAsync(Account account);
    }
}