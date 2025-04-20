using LMS.Common.Application.Data;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

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
}
