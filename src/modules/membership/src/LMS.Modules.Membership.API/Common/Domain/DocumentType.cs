using LMS.Common.Domain;

namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed class DocumentType : Enumeration
{
    public static readonly DocumentType Pdf = new(1, "application/pdf");
    private DocumentType(int id, string name) : base(id, name)
    { }
}
