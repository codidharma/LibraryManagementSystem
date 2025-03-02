using Bogus;
using LMS.Common.Domain;

namespace LMS.Modules.IAM.UnitTests.Base;

public abstract class TestBase
{
    protected static readonly Faker Faker = new();

    public static T GetRaisedEvent<T>(Entity entity) where T : IDomianEvent
    {
        T? domainEvent = entity.DomianEvents.OfType<T>().SingleOrDefault();

        if (domainEvent is null)
        {
            string errorMessage = $"Domain event of type {typeof(T)} was not raised.";
            throw new LmsException(errorMessage);
        }

        return domainEvent;
    }
}
