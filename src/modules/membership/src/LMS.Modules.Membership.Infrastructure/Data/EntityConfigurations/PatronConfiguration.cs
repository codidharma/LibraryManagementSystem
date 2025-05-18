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
            .HasCheckConstraint(
                "ck_patrons_patron_type",
                $"patron_type in ('{PatronType.Regular.Name}', '{PatronType.Research.Name}')");
            t.HasCheckConstraint(
                "ck_patrons_kyc_status",
                $"kyc_status in ('{KycStatus.Pending.Name}', '{KycStatus.InProgress.Name}', '{KycStatus.Completed.Name}', '{KycStatus.Failed.Name}')");
            t.HasCheckConstraint(
                "ck_patrons_status",
                $"status in ('{Status.Active.Name}', '{Status.InActive.Name}')"
                );
        });
        builder.Ignore(p => p.DomainEvents);
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, id => new(id))
            .HasColumnName("id");
        builder.Property(p => p.AccessId)
            .HasConversion(a => a.Value, a => AccessId.Create(a).Value)
            .HasColumnName("access_id")
            .IsRequired(false);
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
        builder.Property(p => p.NationalId)
            .HasConversion(nId => nId.Value, nId => NationalId.Create(nId).Value)
            .HasColumnName("national_id")
            .HasMaxLength(10);
        builder.Property(p => p.PatronType)
            .HasConversion(pt => pt.Name, pt => Enumeration.FromName<PatronType>(pt).Value)
            .HasColumnName("patron_type")
            .HasMaxLength(20);
        builder.OwnsOne(p => p.Address, address =>
        {
            address.Property(a => a.BuildingNumber).HasColumnName("building_number").HasMaxLength(20);
            address.Property(a => a.Street).HasColumnName("street").HasMaxLength(300);
            address.Property(a => a.City).HasColumnName("city").HasMaxLength(20);
            address.Property(a => a.State).HasColumnName("state").HasMaxLength(20);
            address.Property(a => a.Country).HasColumnName("country").HasMaxLength(20);
            address.Property(a => a.ZipCode).HasColumnName("zip_code").HasMaxLength(15);
            address.Property<DateTime>("created_on").HasColumnName("address.created_on");
            address.Property<DateTime>("modified_on").HasColumnName("address.modified_on");
        });
        builder.Property(p => p.KycStatus)
            .HasConversion(ks => ks.Name, ks => Enumeration.FromName<KycStatus>(ks).Value)
            .HasColumnName("kyc_status")
            .HasMaxLength(20);
        builder.Property(p => p.Status)
            .HasConversion(s => s.Name, s => Enumeration.FromName<Status>(s).Value)
            .HasColumnName("status")
            .HasMaxLength(10);
        builder
            .Property<DateTime>("created_on");

        builder.Property<DateTime>("modified_on");

        builder.HasIndex(p => p.Email).IsUnique();
        builder.HasIndex(p => p.NationalId).IsUnique();
        builder.HasMany(p => p.Documents).WithOne();
    }
}
