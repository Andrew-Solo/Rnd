namespace Rnd.Bot.Discord.Views.Drawers;

public class CursiveDrawer : TextDrawer
{
    public override string Draw(string? value)
    {
        return value == null ? "—" : $"*{value}*";
    }
}