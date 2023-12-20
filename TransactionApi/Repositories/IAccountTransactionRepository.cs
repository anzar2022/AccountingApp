using AccountDatabase.Entities;
using AccountDatabase.Repositories;
using System.Linq.Expressions;
namespace TransactionApi.Repositories
{
    public interface IAccountTransactionRepository : IRepositoryBase<AccountTransaction>
    {
        //Task<AccountTransaction> CreateAccountTransactionAsync(AccountTransaction account);
        //Task<bool> DeleteAccountTransactionAsync(Guid Id);
        //Task<AccountTransaction> GetAccountTransactionByIdAsync(Guid Id);
        //Task<AccountTransaction> GetSingleAccountTransactionByFilterAsync(Expression<Func<AccountTransaction, bool>> filter);
        //Task<IEnumerable<AccountTransaction>> GetAccountTransactionsAsync();
        //Task<IEnumerable<AccountTransaction>> GetAccountTransactionsByFilterAsync(Expression<Func<AccountTransaction, bool>> filter);
        //Task<AccountTransaction> UpdateAccountTransactionAsync(AccountTransaction account);
    }
}
