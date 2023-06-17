using System.Text.Json;

namespace Rnd.Data;

public static class Extensions
{
    public static string? GetStringOrNull(this JsonElement item)
    {
        return item.ValueKind != JsonValueKind.String 
            ? item.GetString() 
            : null;
    }
}