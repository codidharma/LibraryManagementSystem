using LMS.Modules.Membership.Infrastructure.Data.Dao;
using LMS.Modules.Membership.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace LMS.Modules.Membership.Infrastructure.Data;

internal sealed class PatronsDbContext(DbContextOptions<PatronsDbContext> options) : DbContext(options)
{
    internal DbSet<AddressDao> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("patrons");
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
    }
}
