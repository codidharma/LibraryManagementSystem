using System.ComponentModel.DataAnnotations;

namespace LMS.Modules.Membership.Infrastructure.Outbox;

internal sealed class OutboxOptions
{
    [Range(minimum: 5, maximum: short.MaxValue)]
    public int IntervalBetweenRunsInSeconds { get; init; }

    [Range(minimum: 1, maximum: 50)]
    public int BatchSize { get; init; }
}
