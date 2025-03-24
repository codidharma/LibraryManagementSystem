namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class DocumentTypeTests
{
    [Fact]
    public void New_ShouldReturn_PersonalIdentification()
    {
        //Arrange
        Domain.PatronAggregate.DocumentType documentType1 = Domain.PatronAggregate.DocumentType.PersonalIdentification;
        Domain.PatronAggregate.DocumentType documentType2 = Domain.PatronAggregate.DocumentType.PersonalIdentification;

        //Assert
        Assert.Equal(documentType1, documentType2);

    }

    [Fact]
    public void New_ShouldReturn_AcademicsIdentification()
    {
        //Arrange
        Domain.PatronAggregate.DocumentType documentType1 = Domain.PatronAggregate.DocumentType.AcademicsIdentification;
        Domain.PatronAggregate.DocumentType documentType2 = Domain.PatronAggregate.DocumentType.AcademicsIdentification;

        //Assert
        Assert.Equal(documentType1, documentType2);
    }

    [Fact]
    public void New_ShouldReturn_AddressProof()
    {
        //Arrange
        Domain.PatronAggregate.DocumentType documentType1 = Domain.PatronAggregate.DocumentType.AddressProof;
        Domain.PatronAggregate.DocumentType documentType2 = Domain.PatronAggregate.DocumentType.AddressProof;

        //Assert
        Assert.Equal(documentType1, documentType2);
    }
}
