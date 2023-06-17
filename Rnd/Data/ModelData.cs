using System.Collections;
using System.Text.Json;

namespace Rnd.Data;

public class ModelData : IDictionary<string, JsonElement>
{
    public ModelData()
    {
        Data = new Dictionary<string, JsonElement>();
    }

    public string? Name => Data[nameof(Name)].GetStringOrNull();
    public string? Path => Data[nameof(Path)].GetStringOrNull();
    
    #region IReadOnlyDictionary
    
    protected IDictionary<string, JsonElement> Data { get; }

    public IEnumerator<KeyValuePair<string, JsonElement>> GetEnumerator()
    {
        return Data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) Data).GetEnumerator();
    }

    public void Add(KeyValuePair<string, JsonElement> item)
    {
        Data.Add(item);
    }

    public void Clear()
    {
        Data.Clear();
    }

    public bool Contains(KeyValuePair<string, JsonElement> item)
    {
        return Data.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, JsonElement>[] array, int arrayIndex)
    {
        Data.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, JsonElement> item)
    {
        return Data.Remove(item);
    }

    public int Count => Data.Count;

    public bool IsReadOnly => Data.IsReadOnly;

    public void Add(string key, JsonElement value)
    {
        Data.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return Data.ContainsKey(key);
    }

    public bool Remove(string key)
    {
        return Data.Remove(key);
    }

    public bool TryGetValue(string key, out JsonElement value)
    {
        return Data.TryGetValue(key, out value);
    }

    public JsonElement this[string key]
    {
        get => Data[key];
        set => Data[key] = value;
    }

    public ICollection<string> Keys => Data.Keys;

    public ICollection<JsonElement> Values => Data.Values;
    
    #endregion
}