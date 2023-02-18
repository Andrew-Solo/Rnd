namespace Rnd.Bot.Discord.Views.Drawers;

public class CursiveBoldDrawer : TextDrawer
{
    public override string Draw(string? value)
    {
        return value == null ? "—" : $"***{value}***";
    }
}