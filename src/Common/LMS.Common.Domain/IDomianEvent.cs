namespace LMS.Common.Domain;

public interface IDomianEvent
{
    Guid Id { get; }
    DateTime OccuredOnUtc { get; }
}
