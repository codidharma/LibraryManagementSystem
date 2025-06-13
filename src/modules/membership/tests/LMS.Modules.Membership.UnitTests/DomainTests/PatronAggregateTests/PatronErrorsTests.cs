using LMS.Modules.Membership.Domain.Common;

namespace LMS.Modules.Membership.UnitTests.DomainTests.PatronAggregateTests;

public class PatronErrorsTests
{
    [Fact]
    public void PatronNotFoundMethod_ShouldReturn_PatronNotFoundError()
    {
        //Arrange
        Guid patronId = Guid.NewGuid();
        string errorCode = ErrorCodes.NotFound;
        string errorDescription = $"The patron with id {patronId.ToString()} was not found.";

        //Act
        Error notFoundError = PatronErrors.PatronNotFound(patronId);

        //Assert
        Assert.Equal(errorCode, notFoundError.Code);
        Assert.Equal(errorDescription, notFoundError.Description);
        Assert.Equal(ErrorType.NotFound, notFoundError.ErrorType);
    }

    [Fact]
    public void EmailAlreadyTakenMethod_ShouldReturn_EmailAlreadyTakenError()
    {
        //Arrange
        string email = "testEmail@Test.com";
        string errorCode = ErrorCodes.Conflict;
        string errorDescription = $"The email {email} is already taken.";

        //Act
        Error emailAlreadyTakenError = PatronErrors.EmailAlreadyTaken(email);

        //Assert
        Assert.Equal(errorCode, emailAlreadyTakenError.Code);
        Assert.Equal(errorDescription, emailAlreadyTakenError.Description);
        Assert.Equal(ErrorType.Conflict, emailAlreadyTakenError.ErrorType);
    }

    [Fact]
    public void AddressNotFoundMethod_ShouldReturn_AddressNotFoundError()
    {
        //Arrange
        Guid patronId = Guid.NewGuid();
        string errorCode = ErrorCodes.NotFound;
        string errorDescription = $"There was no address found on the patron with id {patronId.ToString()}.";

        //Act
        Error addressNotFoundError = PatronErrors.AddressNotFound(patronId);

        //Assert
        Assert.Equal(errorCode, addressNotFoundError.Code);
        Assert.Equal(errorDescription, addressNotFoundError.Description);
        Assert.Equal(ErrorType.NotFound, addressNotFoundError.ErrorType);
    }

    [Fact]
    public void DocumentNotFoundMethod_ShouldReturn_DocumentNotFoundError()
    {
        //Arrange
        Guid documentId = Guid.NewGuid();
        string errorCode = ErrorCodes.NotFound;
        string errorDescription = $"The document with id {documentId.ToString()} was not found.";

        //Act
        Error documentNotFoundError = PatronErrors.DocumentNotFound(documentId);

        //Assert
        Assert.Equal(errorCode, documentNotFoundError.Code);
        Assert.Equal(errorDescription, documentNotFoundError.Description);
        Assert.Equal(ErrorType.NotFound, documentNotFoundError.ErrorType);
    }
}
