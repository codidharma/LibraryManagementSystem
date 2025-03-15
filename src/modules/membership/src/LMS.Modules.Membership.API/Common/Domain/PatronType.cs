using LMS.Common.Domain;

namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed class PatronType(int id, string name) : Enumeration(id, name)
{
    public static readonly PatronType Regular = new(1, "Regular");
    public static readonly PatronType Research = new(2, "Research");

}
