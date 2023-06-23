namespace System.ComponentModel;

public abstract class DocumentEntity
{
    public Guid? Id { get; set; }

    public string? PartitionKey { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? Version { get; set; }

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object? obj) => obj is DocumentEntity entity
        && entity.Id == Id;

    public DocumentEntity()
    {
        CreationDate = DateTime.Now;
    }
}
