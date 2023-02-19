namespace System.DomainModel;

public abstract class Entity
{
    public Guid Id { get; set; }

    [Infrastructure]
    public byte[] Version { get; set; }
}
