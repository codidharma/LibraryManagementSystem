using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Infrastructure.Data.Dao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Modules.Membership.Infrastructure.Data.EntityConfigurations;

internal sealed class DocumentConfiguration : IEntityTypeConfiguration<DocumentDao>
{
    public void Configure(EntityTypeBuilder<DocumentDao> builder)
    {
        builder.ToTable("documents", t =>
        {
            t
            .HasCheckConstraint("ck_document_content_type",
            $"[content_type] IN ('{DocumentContentType.Pdf.Name}', '{DocumentContentType.Jpg.Name}', '{DocumentContentType.Jpeg.Name}')");
            t.HasCheckConstraint("ck_document_document_type",
                $"[document_type] IN ('{DocumentType.PersonalIdentification.Name}', '{DocumentType.AcademicsIdentification.Name}','{DocumentType.AddressProof.Name}')");
        });
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id");
        builder.Property(d => d.ContentType).HasColumnName("content_type");
        builder.Property(d => d.Content).HasColumnName("content");
        builder.Property(d => d.DocumentType).HasColumnName("document_type");
        builder.Property<Guid>("PatronDaoId").HasColumnName("patron_id");
    }
}
