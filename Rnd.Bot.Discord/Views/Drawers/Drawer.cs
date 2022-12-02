namespace Rnd.Bot.Discord.Views.Drawers;

public abstract class Drawer<T> : IDrawer
{
    public abstract string Draw(T? value);

    string IDrawer.Draw(object? value)
    {
        return Draw(value is T t ? t : default);
    }
}