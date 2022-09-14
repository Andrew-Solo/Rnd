namespace Rnd.Api.Helpers;

public static class EnumHelper
{
    public static TEnum Parse<TEnum>(this TEnum enumerable, string value) where TEnum : Enum
    {
        var type = typeof(TEnum);
        return (TEnum) Enum.Parse(type, value);
    }
    
    public static TEnum Parse<TEnum>(string value) where TEnum : Enum
    {
        var type = typeof(TEnum);
        return (TEnum) Enum.Parse(type, value);
    }
}