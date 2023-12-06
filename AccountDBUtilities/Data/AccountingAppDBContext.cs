using AccountDBUtilities.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountDBUtilities.Data
{
    public class AccountingAppDBContext : DbContext
    {
        public AccountingAppDBContext(DbContextOptions<AccountingAppDBContext> options)
 : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelBuilder.ApplyConfiguration(new AccountConfiguration());
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
    }
}
