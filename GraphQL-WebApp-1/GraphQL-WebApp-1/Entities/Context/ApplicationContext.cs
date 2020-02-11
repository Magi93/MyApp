using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLWebApp1.Entities.Context
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
            var ids = new Guid[]
            {
                Guid.NewGuid(), Guid.NewGuid()
            };
            ModelBuilder.ApplyConfiguration(new AccountContextConfiguration(ids));

            ModelBuilder.ApplyConfiguration(new OwnerContextConfiguration(ids));
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Owner> Owners { get; set; }

      
    }
    public class AccountContextConfiguration : IEntityTypeConfiguration<Account>
    {
        private Guid[] _ids;

        public AccountContextConfiguration(Guid[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .HasData(
                new Account
                {
                    Id = Guid.NewGuid(),
                    Type = TypeOfAccount.Cash,
                    Description = "Cash account for our users",
                    OwnerId = _ids[0]
                },
                new Account
                {
                    Id = Guid.NewGuid(),
                    Type = TypeOfAccount.Savings,
                    Description = "Savings account for our users",
                    OwnerId = _ids[1]
                },
                new Account
                {
                    Id = Guid.NewGuid(),
                    Type = TypeOfAccount.Income,
                    Description = "Income account for our users",
                    OwnerId = _ids[1]
                }
           );
        }
    }
    public class OwnerContextConfiguration:IEntityTypeConfiguration<Owner>
    {
        private Guid[] _ids;
        public OwnerContextConfiguration(Guid[] ids)
        {
            _ids = ids;
        }
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasData(
                    new Owner
                    {
                        Id = _ids[0],
                        Name = "John Doe",
                        Address = "John Doe's address"
                    },
                new Owner
                {
                    Id = _ids[1],
                    Name = "Jane Doe",
                    Address = "Jane Doe's address"
                }
            );
        }
    }
}
