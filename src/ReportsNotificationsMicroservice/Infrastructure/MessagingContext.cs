using CSARNCore.Generics;
using CSARNCore.MessagingMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagingMicroservice.Infrastructure
{
    public class MessagingContext : DbContext
    {
        DbSet<Tag> Tags => Set<Tag>();
        DbSet<Response> Responses => Set<Response>();
        DbSet<Report> Reports => Set<Report>();
        DbSet<Notification> Notifications => Set<Notification>();

        public MessagingContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
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
