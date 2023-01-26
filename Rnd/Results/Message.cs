namespace Rnd.Results;

public class Message
{
    public Message(string header, params string[] details) : this(header, details, null) {}
    
    public Message(string header, IEnumerable<string>? details = null, IDictionary<string, HashSet<string>>? tooltips = null)
    {
        Header = header;
        Details = details?.ToHashSet() ?? new HashSet<string>();
        _tooltips = tooltips != null 
            ? new Dictionary<string, HashSet<string>>(tooltips) 
            : new Dictionary<string, HashSet<string>>();
    }

    public string Header { get; private set; }
    public HashSet<string> Details { get; }
    public IReadOnlyDictionary<string, HashSet<string>> Tooltips => _tooltips;

    public void AddDetails(IEnumerable<string> details)
    {
        AddDetails(details.ToArray());
    }
    
    public void AddDetails(params string[] details)
    {
        Details.UnionWith(details);
    }

    public void AddTooltips(string name, IEnumerable<string> messages)
    {
        AddTooltips(name, messages.ToArray());
    }
    
    public void AddTooltips(string name, params string[] messages)
    {
        if (!_tooltips.ContainsKey(name))
        {
            _tooltips[name] = messages.ToHashSet();
            return;
        }
        
        _tooltips[name].UnionWith(messages);
    }

    public void AddTooltips(IReadOnlyDictionary<string, HashSet<string>> tooltips)
    {
        foreach (var (key, value) in _tooltips)
        {
            AddTooltips(key, value);
        }
    }

    public void Update(Message message)
    {
        Header = message.Header;
        AddDetails(message.Details);
        AddTooltips(message.Tooltips);
    }
    
    private readonly Dictionary<string, HashSet<string>> _tooltips;
}