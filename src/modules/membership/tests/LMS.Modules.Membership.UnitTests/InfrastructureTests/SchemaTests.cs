using LMS.Modules.Membership.Infrastructure.Data;

namespace LMS.Modules.Membership.UnitTests.InfrastructureTests;

public class SchemaTests
{
    [Fact]
    public void SchemaName_ShouldReturn_CorrectValue()
    {
        //Arrange
        string expectedValue = "membership";

        //Act
        string actualValue = Schema.Name;

        //Assert
        Assert.Equal(expectedValue, actualValue);
    }
}
