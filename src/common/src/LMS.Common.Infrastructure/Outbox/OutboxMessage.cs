namespace LMS.Common.Infrastructrure.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; init; }
    public string EventType { get; init; }
    public string EventPayload { get; init; }
    public DateTime OccuredOnUtc { get; init; }
    public bool IsProcessed { get; set; }
    public DateTime? ProcessedOnUtc { get; set; }
    public string? Error { get; init; }
}
