using Discord;

namespace Rnd.Bot.Discord.Views;

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
}