using Discord;
using Rnd.Bot.Discord.Views.Drawers;
using Rnd.Bot.Discord.Views.Panels;

namespace Rnd.Bot.Discord.Views.Fields;

public interface IField
{
    string Name { get; }
    object? Value { get; }
    IDrawer Drawer { get; }
    bool IsInline { get; }

    string DrawValue();
    SinglePanel AsPanel();
    Embed AsEmbed();
    EmbedFieldBuilder AsEmbedField();
}