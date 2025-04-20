using LMS.Common.Application.Data;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LMS.Modules.Membership.Infrastructure.Data;

internal sealed class MembershipDbContext(DbContextOptions<MembershipDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Document> Documents { get; set; }
    internal DbSet<Patron> Patrons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.Name);
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        modelBuilder.ApplyConfiguration(new PatronConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInfo();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        ApplyAuditInfo();
        return base.SaveChanges();
    }

    private void ApplyAuditInfo()
    {
        IEnumerable<EntityEntry> trackedEntries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (EntityEntry trackedEntry in trackedEntries)
        {
            DateTime now = DateTime.UtcNow;
            if (trackedEntry.State == EntityState.Added)
            {
                trackedEntry.Property("created_on").CurrentValue = now;
            }
            trackedEntry.Property("modified_on").CurrentValue = now;
        }
    }
}
