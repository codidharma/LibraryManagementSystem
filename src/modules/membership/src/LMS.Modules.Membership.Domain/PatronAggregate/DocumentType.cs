using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record DocumentType : Enumeration
{
    public static readonly DocumentType PersonalIdentification = new(1, "PersonalId");
    public static readonly DocumentType AcademicsIdentification = new(2, "AcademicsId");
    public static readonly DocumentType AddressProof = new(3, "AddressProof");
    private DocumentType(int id, string name) : base(id, name)
    { }
}
