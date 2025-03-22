using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain;

public sealed record DocumentContentType : Enumeration
{
    public static readonly DocumentContentType Pdf = new(1, "application/pdf");
    public static readonly DocumentContentType Jpeg = new(2, "application/jpeg");
    public static readonly DocumentContentType Jpg = new(3, "application/jpg");
    private DocumentContentType(int id, string name) : base(id, name)
    { }
}
