using Discord;
using Rnd.Bot.Discord.Views.Fields;

namespace Rnd.Bot.Discord.Views.Panels;

public class CustomPanel : IPanel
{
    public CustomPanel(string title)
    {
        Title = title;
        Fields = new List<IField>();
    }

    public string Title { get; set; }
    public string? Description { get; set; }
    public List<IField> Fields  { get; set; }
    
    public Uri? Uri  { get; set; }
    public Uri? Thumb  { get; set; }
    public Uri? Image  { get; set; }
    
    public string? Footer  { get; set; }
    public Uri? FooterIcon  { get; set; }
    
    public Color? Color  { get; set; }
    
    public Embed AsEmbed()
    {
        var eb = new EmbedBuilder
        {
            Title = Title,
            Description = Description,
            Fields = Fields.Select(f => f.AsEmbedField()).ToList(),

            Url = Uri?.ToString(),
            ThumbnailUrl = Thumb?.ToString(),
            ImageUrl = Image?.ToString(),

            Footer = new EmbedFooterBuilder()
                .WithText(Footer)
                .WithIconUrl(FooterIcon?.ToString()),

            Color = Color
        };

        return eb.Build();
    }
}