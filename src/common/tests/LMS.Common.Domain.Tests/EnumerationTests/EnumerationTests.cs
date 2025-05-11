using System.Dynamic;

namespace LMS.Common.Domain.Tests.EnumerationTests;

public class EnumerationTests
{
    [Fact]
    public void TwoInstancesWithSameId_ShouldBe_Equal()
    {
        //Arrange
        TestEnumeration enum1 = TestEnumeration.Test1;
        TestEnumeration enum2 = TestEnumeration.Test1;

        //Assert
        Assert.True(enum1.Id > 0);
        Assert.False(string.IsNullOrWhiteSpace(enum1.Name));
        Assert.True(enum2.Id > 0);
        Assert.False(string.IsNullOrWhiteSpace(enum2.Name));
        Assert.True(enum1.Equals(enum2));
        Assert.True(enum1 == enum2);
    }

    [Fact]
    public void Enumerations_ShouldReturn_AllAvailableInstancesOfDerivedType()
    {
        //Arrange
        int expectedCount = 2;

        //Act
        IEnumerable<TestEnumeration> testEnumerations = Enumeration.GetAll<TestEnumeration>();

        //Assert
        Assert.Equal(expectedCount, testEnumerations.Count());

    }

    [Fact]
    public void FromId_ShouldReturn_CorrectInstanceOfDerivedType()
    {
        //Arrange
        int inputId = 1;
        TestEnumeration expected = TestEnumeration.Test1;

        //Act
        Result<TestEnumeration> actualResult = Enumeration.FromId<TestEnumeration>(inputId);

        //Assert
        Assert.Equal(expected, actualResult.Value);
    }

    [Fact]
    public void FromId_ShouldThrow_InvalidOperationException_ForIncorrectId()
    {
        //Arrange
        int id = 5;

        string expectedErrorCode = "Common.NotFound";
        string expectedExceptionMessage = $"Value of Id {id} for type {typeof(TestEnumeration)} was not found.";
        Result<TestEnumeration> expectedResult;

        //Act
        expectedResult = Enumeration.FromId<TestEnumeration>(id);

        //Assert
        Assert.True(expectedResult.IsFailure);
        Assert.False(expectedResult.IsSuccess);
        Assert.Equal(expectedErrorCode, expectedResult.Error.Code);
        Assert.Equal(expectedExceptionMessage, expectedResult.Error.Description);
        Assert.Equal(ErrorType.NotFound, expectedResult.Error.ErrorType);

    }

    [Fact]
    public void FromName_ShouldReturn_CorrectInstanceOfDerivedType()
    {
        //Arrange
        string inputName = "Test1";
        TestEnumeration expected = TestEnumeration.Test1;

        //Act
        Result<TestEnumeration> actual = Enumeration.FromName<TestEnumeration>(inputName);

        //Assert
        Assert.True(actual.IsSuccess);
        Assert.Equal(expected, actual.Value);
    }

    [Fact]
    public void FromName_ShouldThrow_InvalidOperationException_ForIncorrectName()
    {
        //Arrange
        string inputName = "someValue";
        string expectedExceptionMessage = $"Value of Name {inputName} for type {typeof(TestEnumeration)} was not found.";
        string expectedErrorCode = "Common.NotFound";

        //Act
        Result<TestEnumeration> actualResult = Enumeration.FromName<TestEnumeration>(inputName);


        //Assert
        Assert.False(actualResult.IsSuccess);
        Assert.True(actualResult.IsFailure);
        Assert.Equal(expectedExceptionMessage, actualResult.Error.Description);
        Assert.Equal(expectedErrorCode, actualResult.Error.Code);
        Assert.Equal(ErrorType.NotFound, actualResult.Error.ErrorType);


    }

    [Fact]
    public void Enumerations_ShouldFailEqualityComparison_BetweenDisparateTypes()
    {
        //Arrange
        TestEnumeration testEnumeration = TestEnumeration.Test1;
        ExpandoObject dynamicObject = new();

        //Assert
        Assert.False(testEnumeration.Equals(dynamicObject));
    }
}
