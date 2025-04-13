using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Modules.Membership.Infrastructure.Data.EntityConfigurations;

internal sealed class PatronConfiguration : IEntityTypeConfiguration<Patron>
{
    public void Configure(EntityTypeBuilder<Patron> builder)
    {
        builder.ToTable("patrons", t =>
        {
            t
            .HasCheckConstraint("ck_patrons_patron_type", $"patron_type in ('{PatronType.Regular.Name}', '{PatronType.Research.Name}')");
        });
        builder.Ignore(p => p.DomainEvents);
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, id => new(id))
            .HasColumnName("id");
        builder.Property(p => p.AccessId)
            .HasConversion(a => a.Value, a => AccessId.Create(a).Value)
            .HasColumnName("access_id");
        builder.Property(p => p.Name)
            .HasConversion(n => n.Value, n => Name.Create(n).Value)
            .HasColumnName("name").HasMaxLength(300);
        builder.Property(p => p.Gender)
            .HasConversion(g => g.Value, g => Gender.Create(g).Value)
            .HasColumnName("gender").HasMaxLength(20);
        builder.Property(p => p.DateOfBirth)
            .HasConversion(d => d.Value.Kind == DateTimeKind.Utc ? d.Value : DateTime.SpecifyKind(d.Value, DateTimeKind.Utc),
            d => d.Kind == DateTimeKind.Utc ? DateOfBirth.Create(d).Value
            : DateOfBirth.Create(DateTime.SpecifyKind(d, DateTimeKind.Utc)).Value)
            .HasColumnName("date_of_birth");
        builder.Property(p => p.Email)
            .HasConversion(e => e.Value, e => Email.Create(e).Value)
            .HasColumnName("email").HasMaxLength(300);
        builder.Property(p => p.PatronType)
            .HasConversion(pt => pt.Name, pt => Enumeration.FromName<PatronType>(pt))
            .HasColumnName("patron_type")
            .HasMaxLength(20);
        builder.HasIndex(p => p.AccessId).IsUnique();
        builder.HasIndex(p => p.Email).IsUnique();
        builder.HasOne(p => p.Address).WithOne().IsRequired();
        builder.HasMany(p => p.IdentityDocuments).WithOne().IsRequired();
    }
}
