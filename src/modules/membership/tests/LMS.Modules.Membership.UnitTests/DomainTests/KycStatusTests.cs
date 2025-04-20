namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class KycStatusTests
{
    [Fact]
    public void New_ShouldReturn_PendingStatusInstance()
    {
        //Arrange
        KycStatus status1 = KycStatus.Pending;
        KycStatus status2 = KycStatus.Pending;

        //Act
        Assert.Equal(status1, status2);
    }

    [Fact]
    public void New_ShouldReturn_InProgressInstance()
    {
        //Arrange
        KycStatus status1 = KycStatus.InProgress;
        KycStatus status2 = KycStatus.InProgress;

        //Assert
        Assert.Equal(status1, status2);
    }

    [Fact]
    public void New_ShouldReturn_CompletedInstance()
    {
        //Arrange
        KycStatus status1 = KycStatus.Completed;
        KycStatus status2 = KycStatus.Completed;

        //Assert
        Assert.Equal(status1, status2);
    }

    [Fact]
    public void New_ShouldReturn_FailedInstance()
    {
        //Arrange
        KycStatus status1 = KycStatus.Failed;
        KycStatus status2 = KycStatus.Failed;

        //Assert
        Assert.Equal(status1, status2);
    }
}
