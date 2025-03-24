namespace LMS.Common.Domain;

public abstract class Entity
{
    public EntityId Id { get; }
    protected Entity()
    {
        Id = new(Guid.NewGuid());
    }
}
