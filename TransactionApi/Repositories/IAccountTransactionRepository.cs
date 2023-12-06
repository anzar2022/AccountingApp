using System.Linq.Expressions;
using TransactionApi.Entities;
namespace TransactionApi.Repositories
{
    public interface IAccountTransactionRepository
    {
        Task<AccountTransaction> CreateAccountTransactionAsync(AccountTransaction account);
        Task<bool> DeleteAccountTransactionAsync(Guid Id);
        Task<AccountTransaction> GetAccountTransactionByIdAsync(Guid Id);
        Task<AccountTransaction> GetSingleAccountTransactionByFilterAsync(Expression<Func<AccountTransaction, bool>> filter);
        Task<IEnumerable<AccountTransaction>> GetAccountTransactionsAsync();
        Task<IEnumerable<AccountTransaction>> GetAccountTransactionsByFilterAsync(Expression<Func<AccountTransaction, bool>> filter);
        Task<AccountTransaction> UpdateAccountTransactionAsync(AccountTransaction account);
    }
}
