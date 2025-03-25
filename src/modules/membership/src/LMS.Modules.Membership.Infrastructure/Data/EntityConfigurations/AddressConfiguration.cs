using LMS.Modules.Membership.Infrastructure.Data.Dao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Modules.Membership.Infrastructure.Data.EntityConfigurations;

internal class AddressConfiguration : IEntityTypeConfiguration<AddressDao>
{
    public void Configure(EntityTypeBuilder<AddressDao> builder)
    {
        builder.ToTable("addresses");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasColumnName("id");
        builder.Property(a => a.Street).HasColumnName("street").HasMaxLength(300);
        builder.Property(a => a.City).HasColumnName("city").HasMaxLength(20);
        builder.Property(a => a.State).HasColumnName("state").HasMaxLength(20);
        builder.Property(a => a.Country).HasColumnName("country").HasMaxLength(20);
        builder.Property(a => a.ZipCode).HasColumnName("zip_code").HasMaxLength(15);
    }
}
