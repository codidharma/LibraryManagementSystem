namespace LMS.Common.Domain;

public abstract class Entity
{
    public EntityId Id { get; private set; }
    protected Entity()
    {
        Id = new(Guid.NewGuid());
    }

    public void SetEntityId(Guid id)
    {
        Id = new(id);
    }
}
