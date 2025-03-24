using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record PatronType : Enumeration
{
    public static readonly PatronType Regular = new(1, "Regular");
    public static readonly PatronType Research = new(2, "Research");

    private PatronType(int id, string name) : base(id, name)
    { }
}
