using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedLib.AccountsMsvc.Models;
using SharedLib.Auth;
using SharedLib.Generics;

namespace Infrastructure
{
    public class AccountsContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        // Accounts msvc
        public DbSet<Passport> Passports => Set<Passport>();

        public AccountsContext(DbContextOptions<AccountsContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = AccountsRoles.Roles;
            builder.Entity<IdentityRole<Guid>>().HasData(roles);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var updatedAt = nameof(BaseModel.UpdatedAt);
            var createdAt = nameof(BaseModel.CreatedAt);

            var entries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.State == EntityState.Modified || e.State == EntityState.Added)
                .Where(e =>
                    e.Properties.Where(p =>
                        p.Metadata.Name == updatedAt || p.Metadata.Name == createdAt).Any());

            if (!entries.Any())
                return await base.SaveChangesAsync(cancellationToken);

            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Modified:
                        entityEntry.Property(updatedAt).CurrentValue = DateTime.UtcNow;
                        break;

                    case EntityState.Added:
                        entityEntry.Property(createdAt).CurrentValue = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}