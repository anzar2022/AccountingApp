using AccountDatabase.Data;
using AccountDatabase.Entities;
using AccountDatabase.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountApi.Data
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            //throw new NotImplementedException();
        }
    }
}
