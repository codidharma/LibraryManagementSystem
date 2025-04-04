﻿using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class EmailTests : TestBase
{
    [Fact]
    public void Constructor_ShouldReturn_EmailInstance()
    {
        //Arrange
        string emailValue = Faker.Person.Email;

        //Assert
        Email email = new(emailValue);

        //Assert
        Assert.Equal(emailValue, email.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("abc@")]
    public void InvalidEmail_Throws_InvalidValueException(string emailValue)
    {
        //Arrange
        string expectedExceptionMessage = "Email should be in format abc@pqr.com.";
        Email email;

        //Act
        Action action = () => { email = new(emailValue); };

        //Assert
        InvalidValueException exception = Assert.Throws<InvalidValueException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }
}
