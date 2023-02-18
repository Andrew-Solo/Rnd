using Discord;
using Rnd.Bot.Discord.Views.Fields;

namespace Rnd.Bot.Discord.Views.Panels;

public class SinglePanel : IPanel
{
    public SinglePanel(IField field)
    {
        Field = field;
    }

    public IField Field { get; }
    public string Title => Field.Name;
    public string Description => Field.DrawValue();
    
    public Embed AsEmbed()
    {
        var eb = new EmbedBuilder
        {
            Title = Title,
            Description = Description,
        };

        return eb.Build();
    }
}