namespace Rnd.Bot.Discord.Views.Drawers;

public class BoldDrawer : TextDrawer
{
    public override string Draw(string? value)
    {
        return value == null ? "—" : $"**{value}**";
    }
}