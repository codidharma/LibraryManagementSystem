namespace LMS.Modules.Membership.UnitTests.DomainTests.PatronAggregateTests;

public class StatusTests
{
    [Fact]
    public void New_ShouldReturn_ActiveInstance()
    {
        //Arrange
        Status status1 = Status.Active;
        Status status2 = Status.Active;

        //Act
        Assert.Equal(status1, status2);
    }

    [Fact]
    public void New_ShouldReturn_InActiveInstance()
    {
        //Arrange
        Status status1 = Status.InActive;
        Status status2 = Status.InActive;

        //Act
        Assert.Equal(status1, status2);
    }
}
