using AngleSharp.Io.Dom;

namespace MeuPonto.Support;

/// <summary>
/// https://stackoverflow.com/questions/42896380/how-to-specify-an-input-file-to-dom-of-input-type-file-using-anglesharp
/// </summary>
public class FileEntry : IFile
{
    private readonly string _fileName;
    private readonly Stream _content;
    private readonly string _type;
    private readonly DateTime _modified;

    public FileEntry(string fileName, string type, Stream content)
    {
        _fileName = fileName;
        _type = type;
        _content = content;
        _modified = DateTime.Now;
    }

    public Stream Body
    {
        get { return _content; }
    }

    public bool IsClosed
    {
        get { return _content.CanRead == false; }
    }

    public DateTime LastModified
    {
        get { return _modified; }
    }

    public int Length
    {
        get
        {
            return (int)_content.Length;
        }
    }

    public string Name
    {
        get { return _fileName; }
    }

    public string Type
    {
        get { return _type; }
    }

    public void Close()
    {
        _content.Close();
    }

    public void Dispose()
    {
        _content.Dispose();
    }

    public IBlob Slice(int start = 0, int end = int.MaxValue, string contentType = null)
    {
        var ms = new MemoryStream();
        _content.Position = start;
        var buffer = new Byte[Math.Max(0, Math.Min(end, _content.Length) - start)];
        _content.Read(buffer, 0, buffer.Length);
        ms.Write(buffer, 0, buffer.Length);
        _content.Position = 0;
        return new FileEntry(_fileName, _type, ms);
    }
}
