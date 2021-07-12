using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Wallet.Api.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Account> Accounts { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker
                .Entries()
                .Where(entityEntry => entityEntry.Entity is BaseEntity && (entityEntry.State == EntityState.Added ||
                                                                           entityEntry.State == EntityState.Modified));

            var date = DateTime.UtcNow;
            foreach (var entityEntry in entries)
            {
                var baseEntity = (BaseEntity) entityEntry.Entity;
                if (entityEntry.State == EntityState.Added)
                {
                    // TODO: Add user reference
                    baseEntity.CreatedOn = date;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    // TODO: User reference
                    baseEntity.ModifiedOn = date;
                }
            }
            
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}