namespace Rnd.Bot.Discord.Views.Drawers;

public class SpoilerDrawer : TextDrawer
{
    public override string Draw(string? value)
    {
        return value == null ? "—" : $"||{value}||";
    }
}