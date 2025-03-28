using LMS.Modules.Membership.Infrastructure.Data.Dao;
using LMS.Modules.Membership.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace LMS.Modules.Membership.Infrastructure.Data;

internal sealed class MembershipDbContext(DbContextOptions<MembershipDbContext> options) : DbContext(options)
{
    internal DbSet<AddressDao> Addresses { get; set; }
    internal DbSet<DocumentDao> Documents { get; set; }
    internal DbSet<PatronDao> Patrons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("membership");
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        modelBuilder.ApplyConfiguration(new PatronConfiguration());
    }
}
