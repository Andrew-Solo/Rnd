using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    
    public static Dictionary<string, dynamic?> ToDictionary(dynamic? data)
    {
        string json = JsonConvert.SerializeObject(data);
        
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic?>>(json) ?? new Dictionary<string, dynamic?>();
        
        foreach (var key in dictionary.Keys.Where(k => k.StartsWith(HidePrefix)))
        {
            dictionary.Remove(key);
        }

        return dictionary;
    }

    public static string ToJson(dynamic? data)
    {
        return JToken.Parse(JsonConvert.SerializeObject(data)) switch
        {
            JObject obj => JsonConvert.SerializeObject(ToDictionary(data)),
            _ => JsonConvert.SerializeObject(data)
        };
    }

    public static JToken ToJToken(dynamic? data)
    {
        return JToken.Parse(ToJson(data));
    }
    
    public static T ToTyped<T>(dynamic data)
    {
        return JsonConvert.DeserializeObject<T>(ToJson(data));
    }
}