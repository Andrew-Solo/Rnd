namespace Rnd;

public static class Extensions
{
    public static TValue? GetValueOrDefault<TValue, TKey>(this IDictionary<TKey, dynamic> dictionary, TKey name)
    {
        if (!dictionary.ContainsKey(name)) return default;
        return (TValue?) dictionary[name];
    }
    
    public static TValue? Extract<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
        if (!dictionary.ContainsKey(key)) return default;
        var value = dictionary[key];
        dictionary.Remove(key);
        return value;
    }
    
    public static TValue? Extract<TValue>(this IList<TValue> list, Func<TValue, bool> predicate)
    {
        var item = list.FirstOrDefault(predicate);
        if (item == null) return default;
        list.Remove(item);
        return item;
    }
}