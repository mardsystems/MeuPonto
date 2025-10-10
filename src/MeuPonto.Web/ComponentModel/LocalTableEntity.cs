using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel;

public abstract class LocalTableEntity
{
    //[Required]
    public int? Id { get; set; }

    public DateTime? CreationDate { get; set; }

    [Timestamp]
    public byte[]? Version { get; set; }

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object? obj) => obj is LocalTableEntity entity
        && entity.Id == Id;

    public LocalTableEntity()
    {
        CreationDate = DateTime.Now;
    }
}
