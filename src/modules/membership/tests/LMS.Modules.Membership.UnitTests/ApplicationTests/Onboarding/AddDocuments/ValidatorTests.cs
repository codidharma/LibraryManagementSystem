using FluentValidation;
using FluentValidation.TestHelper;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments;
using Document = LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments.Document;

namespace LMS.Modules.Membership.UnitTests.ApplicationTests.Onboarding.AddDocuments;

public class ValidatorTests
{
    private readonly IValidator<Document> _documentValidtor;
    private readonly IValidator<AddDocumentsCommand> _commandValidator;
    public ValidatorTests()
    {
        _documentValidtor = new DocumentValidator();
        _commandValidator = new AddDocumentsCommandValidator();
    }

    [Fact]
    public void ForValidDocument_Validator_ShouldReturn_IsValidAsTrue()
    {
        //Arrange
        string documentName = "Passport.pdf";
        string documentType = "IdentityProof";
        string contentType = "application/pdf";
        string content = "sampledata";
        Document document = new(documentName, documentType, contentType, content);

        //Act
        TestValidationResult<Document> result = _documentValidtor.TestValidate(document);

        //Assert
        Assert.True(result.IsValid);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ForInvalidDocument_Validator_ShouldReturn_IsValidAsFalse()
    {
        //Arrange
        Document document = new(string.Empty, string.Empty, string.Empty, string.Empty);

        //Act
        TestValidationResult<Document> result = _documentValidtor.TestValidate(document);

        //Assert
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(r => r.Name);
        result.ShouldHaveValidationErrorFor(r => r.DocumentType);
        result.ShouldHaveValidationErrorFor(r => r.ContentType);
        result.ShouldHaveValidationErrorFor(r => r.Content);
    }

    [Fact]
    public void ForValidCommand_Validator_ShouldReturn_IsValidAsTrue()
    {
        //Arrange
        Guid patronId = Guid.NewGuid();
        string documentName = "Passport.pdf";
        string documentType = "IdentityProof";
        string contentType = "application/pdf";
        string content = "sampledata";
        Document document = new(documentName, documentType, contentType, content);
        AddDocumentsCommand command = new(patronId, [document]);

        //Act
        TestValidationResult<AddDocumentsCommand> result = _commandValidator.TestValidate(command);

        //Assert
        Assert.True(result.IsValid);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ForInvalidCommandWithNoDocuments_Validator_ShouldReturn_IsValidAsFalse()
    {
        //Arrange
        AddDocumentsCommand command = new(Guid.Empty, []);

        //Act
        TestValidationResult<AddDocumentsCommand> result = _commandValidator.TestValidate(command);

        //Assert
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(x => x.Documents);
    }

    [Fact]
    public void ForInvalidCommand_Validator_ShouldReturn_IsValidAsFalse()
    {
        //Arrange
        Document document = new(string.Empty, string.Empty, string.Empty, string.Empty);
        AddDocumentsCommand command = new(Guid.Empty, [document]);

        //Act
        TestValidationResult<AddDocumentsCommand> result = _commandValidator.TestValidate(command);

        //Assert
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(r => r.PatronId);
        result.ShouldHaveValidationErrorFor("Documents[0].Name");
        result.ShouldHaveValidationErrorFor("Documents[0].DocumentType");
        result.ShouldHaveValidationErrorFor("Documents[0].ContentType");
        result.ShouldHaveValidationErrorFor("Documents[0].Content");
    }
}
