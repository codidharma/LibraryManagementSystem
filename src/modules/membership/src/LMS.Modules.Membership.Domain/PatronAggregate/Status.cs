using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record Status : Enumeration
{
    public static readonly Status Active = new(1, "Active");
    public static readonly Status InActive = new(1, "InActive");
    private Status(int id, string name) : base(id, name) { }
}
