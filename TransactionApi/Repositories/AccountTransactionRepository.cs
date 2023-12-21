using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TransactionApi.Data;
using TransactionApi.Entities;

namespace TransactionApi.Repositories
{
    public class AccountTransactionRepository : IAccountTransactionRepository
    {
        private AccountTransactionDBContext _context ;
        private ILogger<AccountTransactionDBContext> _logger;
        public AccountTransactionRepository(AccountTransactionDBContext context, ILogger<AccountTransactionDBContext> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }
        public async Task<AccountTransaction> CreateAccountTransactionAsync(AccountTransaction  accountTransaction)
        {
            try
            {
                _context.AccountTransactions.Add(accountTransaction);
                 await _context.SaveChangesAsync();

                return accountTransaction;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAccountTransactionAsync(Guid Id)
        {
            try
            {
                var transactionToDelete = await _context.AccountTransactions.FindAsync(Id);
                if (transactionToDelete == null)
                {
                    return false;
                }

                _context.AccountTransactions.Remove(transactionToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public async Task<AccountTransaction> GetAccountTransactionByIdAsync(Guid Id)
        {
            try
            {
                var accountTransaction = await _context.AccountTransactions.FirstOrDefaultAsync(a=> a.Id == Id );

                

                return accountTransaction;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<AccountTransaction>> GetAccountTransactionsAsync()
        {
            try
            {
                var accountTransactions = await _context.AccountTransactions.ToListAsync();

                return accountTransactions;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<AccountTransaction>> GetAccountTransactionsByFilterAsync(Expression<Func<AccountTransaction, bool>> filter)
        {
            try
            {
                var accountTransactions = await _context.AccountTransactions.Where(filter).ToListAsync();
                return accountTransactions;
            }
            catch (Exception)
            {

                throw;
            }
        } 

        public async Task<AccountTransaction> GetSingleAccountTransactionByFilterAsync(Expression<Func<AccountTransaction, bool>> filter)
        {
            var accountTransaction = await _context.AccountTransactions.FirstOrDefaultAsync(filter) ;

            return accountTransaction;
        }

        public async Task<AccountTransaction> UpdateAccountTransactionAsync(AccountTransaction accountTransaction)
        {
            try
            {

                _context.AccountTransactions.Update(accountTransaction);
                await _context.SaveChangesAsync();
                return accountTransaction;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
