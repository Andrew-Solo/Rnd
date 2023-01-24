using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Rnd.Bot.Discord.Views;

public static class ViewData
{
    public const string HidePrefix = "_";
    
    public static JsonSerializerSettings Settings { get; } = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
    };
    
    public static Dictionary<string, dynamic> ToDictionary(dynamic? data)
    {
        string json = JsonConvert.SerializeObject(data);
        
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json) ?? new Dictionary<string, dynamic>();
        
        foreach (var key in dictionary.Keys.Where(k => k.StartsWith(HidePrefix)))
        {
            dictionary.Remove(key);
        }

        return dictionary;
    }

    public static string ToJson(dynamic data)
    {
        return JsonConvert.SerializeObject(ToDictionary(data));
    }
    
    public static T ToTyped<T>(dynamic data)
    {
        return JsonConvert.DeserializeObject<T>(ToJson(data));
    }

    public static TValue? GetValueOrDefault<TValue>(this IDictionary<string, dynamic> dictionary, string name)
    {
        if (!dictionary.ContainsKey(name)) return default;
        return (TValue?) dictionary[name];
    }
}