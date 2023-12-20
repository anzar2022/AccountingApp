using AccountDatabase.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDatabase.Data
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

        public DbSet<InterestMaster> InterestMasters { get; set; }

        public DbSet<Mobile> Mobiles { get; set; }
    }
}
