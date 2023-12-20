using InterestMasterApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterestMasterApi.Data
{
    public class InterestMasterDBContext : DbContext
    {
        public InterestMasterDBContext(DbContextOptions<InterestMasterDBContext> options)
            : base(options)
        { }

        public DbSet<InterestMaster> InterestMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // You can add configurations or other modelBuilder settings here if needed
            // Example: modelBuilder.ApplyConfiguration(new MasterInterestConfiguration());
        }
    }
}
