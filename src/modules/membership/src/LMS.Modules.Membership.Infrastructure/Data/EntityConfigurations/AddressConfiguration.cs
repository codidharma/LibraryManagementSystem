using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Modules.Membership.Infrastructure.Data.EntityConfigurations;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");
        builder.HasKey(a => a.Id);
        builder.Ignore(a => a.DomainEvents);
        builder.Property(a => a.Id)
            .HasConversion(id => id.Value, id => new(id))
            .HasColumnName("id");
        builder.Property(a => a.Street).HasColumnName("street").HasMaxLength(300);
        builder.Property(a => a.City).HasColumnName("city").HasMaxLength(20);
        builder.Property(a => a.State).HasColumnName("state").HasMaxLength(20);
        builder.Property(a => a.Country).HasColumnName("country").HasMaxLength(20);
        builder.Property(a => a.ZipCode).HasColumnName("zip_code").HasMaxLength(15);
        builder.Property<EntityId>("PatronId").HasColumnName("patron_id");
    }
}
