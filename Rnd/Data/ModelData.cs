using System.Collections;
using System.Text.Json;
using Rnd.Primitives;

namespace Rnd.Data;

public class ModelData : IDictionary<string, JsonElement>
{
    public ModelData()
    {
        _data = new Dictionary<string, JsonElement>();
    }

    public Guid? Id => this[nameof(Id)].GetGuidOrNull();
    public string? Path => this[nameof(Path)].GetStringOrNull();
    public string? Name => this[nameof(Name)].GetStringOrNull();
    public string? Title => this[nameof(Title)].GetStringOrNull();
    public string? Subtitle => this[nameof(Subtitle)].GetStringOrNull();
    public string? Description => this[nameof(Description)].GetStringOrNull();
    public string? Icon => this[nameof(Icon)].GetStringOrNull();
    public HslaColor? Color => this[nameof(Color)].GetHslaColorOrNull();
    public HslaColor? Subcolor => this[nameof(Subcolor)].GetHslaColorOrNull();
    public string? Thumbnail => this[nameof(Thumbnail)].GetStringOrNull();
    public string? Image => this[nameof(Image)].GetStringOrNull();
    public string? Subimage => this[nameof(Subimage)].GetStringOrNull();
    public Dictionary<string, string> Attributes => this[nameof(Attributes)].GetDictionary();
    public byte? Order => this[nameof(Order)].GetByteOrNull();
    
    #region IReadOnlyDictionary
    
    public int Count => _data.Count;
    public bool IsReadOnly => _data.IsReadOnly;
    public ICollection<string> Keys => _data.Keys;
    public ICollection<JsonElement> Values => _data.Values;
    
    public JsonElement this[string key]
    {
        get => _data.TryGetValue(key, out var value) ? value : new JsonElement();
        set => _data[key] = value;
    }
    
    public IEnumerator<KeyValuePair<string, JsonElement>> GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable) _data).GetEnumerator();
    }

    public void Add(KeyValuePair<string, JsonElement> item)
    {
        _data.Add(item);
    }

    public void Clear()
    {
        _data.Clear();
    }

    public bool Contains(KeyValuePair<string, JsonElement> item)
    {
        return _data.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, JsonElement>[] array, int arrayIndex)
    {
        _data.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, JsonElement> item)
    {
        return _data.Remove(item);
    }

    public void Add(string key, JsonElement value)
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

    public bool TryGetValue(string key, out JsonElement value)
    {
        return _data.TryGetValue(key, out value);
    }

    private IDictionary<string, JsonElement> _data;

    #endregion
}