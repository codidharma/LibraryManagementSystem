namespace LMS.Common.Domain.Tests.DomainEventTests;

public class DomainEventTests
{
    [Fact]
    public void ParameterlessConstructor_Sets_CorrectEventMetadata()
    {
        //Arrange
        DummyDomainEvent dummy = new DummyDomainEvent();

        //Assert
        Assert.False(dummy.Id == Guid.Empty);
        Assert.IsType<DateTime>(dummy.OccuredOnUtc);
    }

    [Fact]
    public void ParametrizedConstructor_Sets_CorrectEventMetadata()
    {
        //Arrange
        Guid eventId = Guid.NewGuid();
        DateTime dateOccuredOnUtc = DateTime.UtcNow;
        //Act
        DummyDomainEvent dummy = new(eventId, dateOccuredOnUtc);

        //Assert
        Assert.Equal(eventId, dummy.Id);
        Assert.Equal(dateOccuredOnUtc, dummy.OccuredOnUtc);
    }
}
