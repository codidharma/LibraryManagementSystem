using LMS.Modules.Membership.API.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class DocumentContentTests : TestBase
{
    [Fact]
    public void New_ShouldThrow_InvalidValueException_ForInvalidBase64String()
    {
        //arrange
        string sampleData = "somedata#!@!@!@";
        DocumentContent content;

        //Act
        Action action = () => { content = new(sampleData); };

        //Assert
        Assert.Throws<InvalidValueException>(action);
    }
}
