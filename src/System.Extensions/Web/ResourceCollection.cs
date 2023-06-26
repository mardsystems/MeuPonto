namespace System.Web;

public class ResourceCollection<T> : Resource
{
    public Resource<T>[] Data { get; set; }
}
