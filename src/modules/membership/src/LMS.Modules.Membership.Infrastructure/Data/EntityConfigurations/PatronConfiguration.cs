using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Infrastructure.Data.Dao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Modules.Membership.Infrastructure.Data.EntityConfigurations;

internal sealed class PatronConfiguration : IEntityTypeConfiguration<PatronDao>
{
    public void Configure(EntityTypeBuilder<PatronDao> builder)
    {
        builder.ToTable("patrons", t =>
        {
            t
            .HasCheckConstraint("ck_patrons_patron_type", $"[patron_type] in ('{PatronType.Regular.Name}', '{PatronType.Research.Name}')");
        });
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(300);
        builder.Property(p => p.Gender).HasColumnName("gender").HasMaxLength(20);
        builder.Property(p => p.DateOfBirth).HasColumnName("date_of_birth");
        builder.Property(p => p.Email).HasColumnName("email").HasMaxLength(300);
        builder.HasIndex(p => p.Email).IsUnique();
        builder.HasOne<AddressDao>().WithOne();
        builder.HasMany<DocumentDao>().WithOne();
    }
}
