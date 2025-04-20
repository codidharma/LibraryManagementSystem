using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Modules.Membership.Infrastructure.Data.EntityConfigurations;

internal sealed class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("documents", t =>
        {
            t
            .HasCheckConstraint("ck_document_content_type",
            $"content_type IN ('{DocumentContentType.Pdf.Name}', '{DocumentContentType.Jpg.Name}', '{DocumentContentType.Jpeg.Name}')");
            t.HasCheckConstraint("ck_document_document_type",
                $"document_type IN ('{DocumentType.PersonalIdentification.Name}', '{DocumentType.AcademicsIdentification.Name}','{DocumentType.AddressProof.Name}')");
        });
        builder.Ignore(d => d.DomainEvents);
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
            .HasConversion(id => id.Value, id => new(id))
            .HasColumnName("id");
        builder.Property(d => d.Name)
            .HasConversion(n => n.Value, n => Name.Create(n).Value)
            .HasColumnName("name")
            .HasMaxLength(50);
        builder.Property(d => d.DocumentType)
            .HasConversion(dt => dt.Name, dt => Enumeration.FromName<DocumentType>(dt))
            .HasColumnName("document_type");
        builder.Property(d => d.Content)
            .HasConversion(c => c.Value, c => DocumentContent.Create(c).Value)
            .HasColumnName("content");
        builder.Property(d => d.ContentType)
            .HasConversion(ct => ct.Name, ct => Enumeration.FromName<DocumentContentType>(ct))
            .HasColumnName("content_type");
        builder
            .Property<DateTime>("created_on");
        builder.Property<DateTime>("modified_on");


        builder.Property<EntityId>("PatronId").HasColumnName("patron_id");
    }
}
