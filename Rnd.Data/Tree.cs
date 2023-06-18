using System.Collections;

namespace Rnd.Data;

public class Tree : IReadOnlyDictionary<string, Node>
{
    public Tree(params Node[] nodes)
    {
        _data = nodes.ToDictionary(node => node.Resource, node => node);
    }

    public Node User => this[""];
    public Node Users => this[nameof(Users)];
    public Node Modules => this[nameof(Modules)];
    public Node Fields => this[nameof(Fields)];
    public Node Units => this[nameof(Units)];

    #region IDictionary

    public int Count => _data.Count;
    public bool IsReadOnly => _data.IsReadOnly;
    public IEnumerable<string> Keys => _data.Keys;
    public IEnumerable<Node> Values => _data.Values;

    public Node this[string key]
    {
        get => _data[key];
        set => _data[key] = value;
    }
    
    public IEnumerator<KeyValuePair<string, Node>> GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) _data).GetEnumerator();
    }

    public void Add(KeyValuePair<string, Node> item)
    {
        _data.Add(item);
    }

    public void Clear()
    {
        _data.Clear();
    }

    public bool Contains(KeyValuePair<string, Node> item)
    {
        return _data.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, Node>[] array, int arrayIndex)
    {
        _data.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, Node> item)
    {
        return _data.Remove(item);
    }

    public void Add(string key, Node value)
    {
        _data.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return _data.ContainsKey(key);
    }

    public bool Remove(string key)
    {
        return _data.Remove(key);
    }

    public bool TryGetValue(string key, out Node value)
    {
        return _data.TryGetValue(key, out value!);
    }
    
    private IDictionary<string, Node> _data;

    #endregion
}