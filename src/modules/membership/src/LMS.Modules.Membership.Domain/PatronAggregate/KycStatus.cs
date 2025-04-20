using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record KycStatus : Enumeration
{
    public static readonly KycStatus Pending = new(1, "Pending");
    public static readonly KycStatus InProgress = new(2, "InProgress");
    public static readonly KycStatus Completed = new(3, "Completed");
    public static readonly KycStatus Failed = new(4, "Failed");
    private KycStatus(int id, string name) : base(id, name) { }
}
