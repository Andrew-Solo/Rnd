namespace Rnd.Model;

public readonly record struct Message
{
    public Message(string? header = null, IEnumerable<string>? general = null, IDictionary<string, string>? properties = null)
    {
        Header = header;
        General = general?.ToArray() ?? ArraySegment<string>.Empty;
        Properties = new Dictionary<string, string>(properties ?? new Dictionary<string, string>());
    }

    public string? Header { get; }
    public IReadOnlyCollection<string> General { get; }
    public IReadOnlyDictionary<string, string> Properties { get; }
}