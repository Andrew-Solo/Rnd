namespace Rnd.Result;

public readonly record struct Message
{
    public Message(string header, string general) : this(header, new []{general}) {}
    
    public Message(string header, IEnumerable<string>? general = null, IDictionary<string, HashSet<string>>? properties = null)
    {
        Header = header;
        General = general?.ToList() ?? new List<string>();
        Properties = new Dictionary<string, HashSet<string>>(properties ?? new Dictionary<string, HashSet<string>>());
    }

    public string Header { get; }
    public List<string> General { get; }
    public Dictionary<string, HashSet<string>> Properties { get; }

    public void AddProperty(string name, params string[] errors)
    {
        if (!Properties.ContainsKey(name))
        {
            Properties[name] = errors.ToHashSet();
            return;
        }
        
        Properties[name].UnionWith(errors);
    }
}