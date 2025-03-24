using LMS.Modules.Membership.Domain.PatronAggregate;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class DocumentTypeTests
{
    [Fact]
    public void New_ShouldReturn_PersonalIdentification()
    {
        //Arrange
        DocumentType documentType1 = DocumentType.PersonalIdentification;
        DocumentType documentType2 = DocumentType.PersonalIdentification;

        //Assert
        Assert.Equal(documentType1, documentType2);

    }

    [Fact]
    public void New_ShouldReturn_AcademicsIdentification()
    {
        //Arrange
        DocumentType documentType1 = DocumentType.AcademicsIdentification;
        DocumentType documentType2 = DocumentType.AcademicsIdentification;

        //Assert
        Assert.Equal(documentType1, documentType2);
    }

    [Fact]
    public void New_ShouldReturn_AddressProof()
    {
        //Arrange
        DocumentType documentType1 = DocumentType.AddressProof;
        DocumentType documentType2 = DocumentType.AddressProof;

        //Assert
        Assert.Equal(documentType1, documentType2);
    }
}
