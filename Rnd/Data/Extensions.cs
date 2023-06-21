using System.Text.Json;
using Newtonsoft.Json;
using Rnd.Primitives;
using Exception = System.Exception;

namespace Rnd.Data;

public static class Extensions
{
    public static T? GetObject<T>(this JsonElement item)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(item.GetRawText());
        }
        catch (Exception)
        {
            return default;
        }
    }

    public static string? GetStringOrNull(this JsonElement item)
    {
        return item.ValueKind == JsonValueKind.String 
            ? item.GetString() 
            : null;
    }
    
    public static Guid? GetGuidOrNull(this JsonElement item)
    {
        return Guid.TryParse(GetStringOrNull(item), out var value)
            ? value
            : null;
    }
    
    public static T? GetEnumOrNull<T>(this JsonElement item) where T : struct, Enum
    {
        return Enum.TryParse<T>(GetStringOrNull(item), out var value)
            ? value
            : null;
    }
    
    public static Version? GetVersionOrNull(this JsonElement item)
    {
        try
        {
            return new Version(GetStringOrNull(item)!);
        }
        catch (Exception)
        {
            return null;
        }
    }
    
    public static bool? GetBooleanOrNull(this JsonElement item)
    {
        return item.ValueKind is JsonValueKind.True or JsonValueKind.False  
            ? item.GetBoolean() 
            : null;
    }
    
    public static byte? GetByteOrNull(this JsonElement item)
    {
        if (item.ValueKind != JsonValueKind.Number) return null;
        if (!item.TryGetByte(out var value)) return null;
        return value;
    }
    
    public static short? GetShortOrNull(this JsonElement item)
    {
        if (item.ValueKind != JsonValueKind.Number) return null;
        if (!item.TryGetInt16(out var value)) return null;
        return value;
    }
    
    public static DateTimeOffset? GetDateTimeOffsetOrNull(this JsonElement item)
    {
        if (item.ValueKind != JsonValueKind.String) return null;

        try
        {
            return item.GetDateTimeOffset();
        }
        catch (Exception)
        {
            return null;
        }
    }
    
    public static Dictionary<string, string> GetDictionary(this JsonElement item)
    {
        return item.ValueKind != JsonValueKind.Object 
            ? item.EnumerateObject().ToDictionary(
                prop => prop.Name, 
                prop => prop.Value.GetRawText()
            )
            : new Dictionary<string, string>();
    }
    
    public static HslaColor? GetHslaColorOrNull(this JsonElement item)
    {
        if (item.ValueKind != JsonValueKind.Array || item.GetArrayLength() is < 1 or > 4) return null;
        var array = item.EnumerateArray().ToList();
        
        var hue = array[0].GetShortOrNull();
        if (hue == null) return null;

        byte saturation = 100;
        if (array.Count > 1) saturation = array[1].GetByteOrNull() ?? 100;
        
        byte lightness = 100;
        if (array.Count > 2) lightness = array[2].GetByteOrNull() ?? 100;
        
        byte alpha = 100;
        if (array.Count > 3) alpha = array[3].GetByteOrNull() ?? 100;

        return new HslaColor(hue.Value, saturation, lightness, alpha);
    }
}