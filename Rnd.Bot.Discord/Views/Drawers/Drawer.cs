using Newtonsoft.Json.Linq;

namespace Rnd.Bot.Discord.Views.Drawers;

public abstract class Drawer<T> : IDrawer
{
    public abstract string Draw(T? value);

    string IDrawer.Draw(object? value)
    {
        return Draw(value is T t ? t : default);
    }
}

public static class Drawer
{
    public static string Draw(JToken? value)
    {
        var drawer = new JTokenDrawer();
        return drawer.Draw(value);
    }
    
    public static string Draw(IDictionary<string, dynamic?> value)
    {
        var drawer = new DictionaryDrawer();
        return drawer.Draw(value);
    }

    public static string Draw(List<string?> value)
    {
        var drawer = new ListDrawer();
        return drawer.Draw(value);
    }
    
    public static string Draw(DateTimeOffset? value)
    {
        var drawer = new DateTimeOffsetDrawer();
        return drawer.Draw(value);
    }
    
    public static string Draw(TimeSpan? value)
    {
        var drawer = new TimeSpanDrawer();
        return drawer.Draw(value);
    }
}