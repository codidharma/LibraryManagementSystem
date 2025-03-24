namespace LMS.Common.Domain;

public sealed record EntityId(Guid Id) : ValueObject
{
    public Guid Value { get; } = Id;
}
