using Discord;
using Rnd.Bot.Discord.Views.Fields;

namespace Rnd.Bot.Discord.Views.Panels;

public interface IPanel
{
    string Title { get; }
    string? Description => null;
    List<IField>? Fields  => null;
    
    Uri? Uri  => null;
    Uri? Thumb  => null;
    Uri? Image  => null;
    
    string? Footer  => null;
    Uri? FooterIcon  => null;
    
    Color? Color  => null;

    public Embed AsEmbed();
}