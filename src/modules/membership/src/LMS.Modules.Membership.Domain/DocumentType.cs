using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain;

public sealed record DocumentType : Enumeration
{
    public static readonly DocumentType PersonalIdentification = new(1, "PersonalId");
    public static readonly DocumentType AcademicsIdentification = new(2, "AcademicsId");
    private DocumentType(int id, string name) : base(id, name)
    { }
}
