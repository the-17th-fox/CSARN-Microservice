using CSARNCore.AccountsMsvc.Constants;
using CSARNCore.AccountsMsvc.Models;
using CSARNCore.Generics;
using CSARNCore.MessagingMicroservice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSARNCore.Infrastructure
{
    public class CSARNContext : IdentityDbContext<Account, IdentityRole<Guid>, Guid>
    {
        // Accounts msvc
        public DbSet<Passport> Passports => Set<Passport>();

        // Messaging msvc
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Response> Responses => Set<Response>();
        public DbSet<Report> Reports => Set<Report>();
        public DbSet<Notification> Notifications => Set<Notification>();

        public CSARNContext(DbContextOptions<CSARNContext> opt) : base(opt)
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
