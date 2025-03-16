using LMS.Common.Domain;

namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed class DocumentType : Enumeration
{
    public static readonly DocumentType PersonalIdentification = new(1, "PersonalId");
    public static readonly DocumentType AcademicsIdentification = new(2, "AcademicsId");
    private DocumentType(int id, string name) : base(id, name)
    { }
}
