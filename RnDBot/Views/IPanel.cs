using Discord;
using RnDBot.View;

namespace RnDBot.Views;

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