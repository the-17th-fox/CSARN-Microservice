using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedLib.AccountsMsvc.Models;
using SharedLib.Auth;
using SharedLib.Generics.Models;

namespace Infrastructure
{
    public class AccountsContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        public DbSet<Passport> Passports => Set<Passport>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public AccountsContext(DbContextOptions<AccountsContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole<Guid>>().HasData(AccountsRoles.Roles);

            builder.Entity<Account>()
                .Ignore(c => c.PhoneNumberConfirmed)
                .Ignore(c => c.EmailConfirmed)
                .Ignore(c => c.TwoFactorEnabled)
                .Ignore(c => c.LockoutEnabled)
                .Ignore(c => c.LockoutEnd);

            builder.Entity<Account>()
                .HasOne(a => a.RefreshToken)
                .WithOne(rt => rt.Account)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Account>()
                .HasOne(a => a.Passport)
                .WithOne(p => p.Account)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var updatedAt = nameof(BaseModel.UpdatedAt);
            var createdAt = nameof(BaseModel.CreatedAt);

            var entries = ChangeTracker.Entries();
            if (!entries.Any())
                return await base.SaveChangesAsync(cancellationToken);

            var modifed = entries
                .Where(e => e.State == EntityState.Modified)
                .Where(e => e.Properties.Where(p => p.Metadata.Name == updatedAt).Any());

            var added = entries
                .Where(e => e.State == EntityState.Added)
                .Where(e => e.Properties.Where(p => p.Metadata.Name == createdAt).Any());

            foreach (var entityEntry in added)
                entityEntry.Property(createdAt).CurrentValue = DateTime.UtcNow;

            foreach (var entityEntry in modifed)
                entityEntry.Property(updatedAt).CurrentValue = DateTime.UtcNow;

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}