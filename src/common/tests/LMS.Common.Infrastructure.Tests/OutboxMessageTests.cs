using LMS.Common.Infrastructrure.Outbox;

namespace LMS.Common.Infrastructure.Tests;

public class OutboxMessageTests
{
    [Fact]
    public void New_ShouldCreate_OutboxMessageInstance()
    {
        //Arrange
        Guid id = Guid.NewGuid();
        string eventType = "Generic.TestCreatedEvent";
        string payload = "somepayload";
        DateTime occuredOnUtc = DateTime.UtcNow;

        //Act
        OutboxMessage message = new()
        {
            Id = id,
            EventType = eventType,
            EventPayload = payload,
            OccuredOnUtc = occuredOnUtc,
            IsProcessed = false
        };

        //Assert
        Assert.Equal(id, message.Id);
        Assert.Equal(eventType, message.EventType);
        Assert.Equal(payload, message.EventPayload);
        Assert.Equal(occuredOnUtc, message.OccuredOnUtc);
        Assert.False(message.IsProcessed);
        Assert.Null(message.ProcessedOnUtc);
        Assert.Null(message.Error);
    }

}
