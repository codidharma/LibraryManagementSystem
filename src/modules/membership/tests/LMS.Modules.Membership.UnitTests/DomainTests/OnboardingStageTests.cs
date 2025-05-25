namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class OnboardingStageTests
{
    [Fact]
    public void New_ShouldReturn_PatronAddedStage()
    {
        //Arrange
        OnboardingStage stage1 = OnboardingStage.PatronAdded;
        OnboardingStage stage2 = OnboardingStage.PatronAdded;

        //Assert
        Assert.Equal(stage1, stage2);
    }

    [Fact]
    public void New_ShouldReturn_AddressAddedStage()
    {
        //Arrange
        OnboardingStage stage1 = OnboardingStage.AddressAdded;
        OnboardingStage stage2 = OnboardingStage.AddressAdded;

        //Assert
        Assert.Equal(stage1, stage2);
    }

    [Fact]
    public void New_ShouldReturn_DocumentAddedStage()
    {
        //Arrange
        OnboardingStage stage1 = OnboardingStage.DocumentAdded;
        OnboardingStage stage2 = OnboardingStage.DocumentAdded;

        //Assert
        Assert.Equal(stage1, stage2);
    }

    [Fact]
    public void New_ShouldReturn_DocumentsVerified()
    {
        //Arrange
        OnboardingStage stage1 = OnboardingStage.DocumentsVerified;
        OnboardingStage stage2 = OnboardingStage.DocumentsVerified;

        //Assert
        Assert.Equal(stage1, stage2);
    }
}
