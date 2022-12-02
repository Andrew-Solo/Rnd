namespace Rnd.Bot.Discord.Views.Drawers;

public class TextDrawer : Drawer<string?>
{
    public override string Draw(string? value)
    {
        return value ?? "—";
    }
}