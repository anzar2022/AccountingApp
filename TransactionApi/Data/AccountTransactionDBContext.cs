using Microsoft.EntityFrameworkCore;
using TransactionApi.Entities;

namespace TransactionApi.Data
{
    public class AccountTransactionDBContext : DbContext
    {
        public AccountTransactionDBContext(DbContextOptions<AccountTransactionDBContext> options)
: base(options)
        { }
        public DbSet<AccountTransaction>  AccountTransactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelBuilder.ApplyConfiguration(new AccountConfiguration());
        }
    }
}
