using LMS.Common.Infrastructrure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMS.Common.Infrastructure.Outbox;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");
        builder.HasKey(om => om.Id);
        builder.Property(om => om.EventPayload)
            .HasColumnName("event_payload")
            .HasColumnType("jsonb")
            .HasMaxLength(3000)
            .IsRequired();
        builder.Property(om => om.EventType)
            .HasColumnName("event_type")
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(om => om.OccuredOnUtc)
            .HasColumnName("occuredon_utc")
            .IsRequired();
        builder.Property(om => om.IsProcessed)
            .HasColumnName("is_processed")
            .IsRequired();
        builder.Property(om => om.ProcessedOnUtc)
            .HasColumnName("processedon_utc");
        builder.Property(om => om.Error)
            .HasColumnName("error");
    }
}
