namespace System.Web;

public class Resource
{
    public string Title { get; set; }

    public string HRef { get; set; }

    public Link[] Links { get; set; }
}

public class Resource<T> : Resource
{
    public T Data { get; set; }
}
