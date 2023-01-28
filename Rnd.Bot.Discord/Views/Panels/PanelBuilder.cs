using System.Text;
using Discord;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rnd.Bot.Discord.Views.Drawers;
using Rnd.Bot.Discord.Views.Fields;
using Rnd.Results;

namespace Rnd.Bot.Discord.Views.Panels;

public class PanelBuilder
{
    private PanelBuilder(string title)
    {
        _panel = new CustomPanel(title);
    }

    public static PanelBuilder WithTitle(string title)
    {
        return new PanelBuilder(title);
    }

    public static PanelBuilder ByField(IField field)
    {
        return WithTitle(field.Name).WithDescription(field.DrawValue());
    }
    
    public static PanelBuilder ByMessage(Message message)
    {
        var field = FieldBuilder.WithName(message.Title);

        if (message.Details.Count == 1)
        {
            field.WithValue(message.Details.First());
        }
        else if (message.Details.Count > 1)
        {
            field.WithValue(message.Details);
        }
        
        return ByField(field.Build())
            .WithFields(message.Tooltips.Select(pair => FieldBuilder.WithName(pair.Key).WithValue(pair.Value).Build()));
    }

    public static PanelBuilder ByObject(dynamic? data, string? title = null)
    {
        return ViewData.ToJToken(data) switch
        {
            JValue value => ByJToken(value, title),
            JArray array => ByJToken(array, title),
            JObject obj => ByJToken(obj, title),
            _ => throw new InvalidOperationException("Unknown token type")
        };
    }

    public static PanelBuilder ByJToken(JValue value, string? title)
    {
        return title == null 
            ? WithTitle(Drawer.Draw(value))
            : WithTitle(title).WithDescription(Drawer.Draw(value));
    }
    
    public static PanelBuilder ByJToken(JArray array, string? title)
    {
        return WithTitle(title ?? "Список").WithDescription(Drawer.Draw(array));
    }
    
    public static PanelBuilder ByJToken(JObject obj, string? title)
    {
        return WithTitle(obj.Extract("Title")?.Value<string>() ?? title ?? "Объект")
            .WithDescription(obj);
    }

    public PanelBuilder WithDescription(JObject obj)
    {
        var extract = obj.Extract("Description")?.Value<string>();
        
        var sb = new StringBuilder();

        if (extract != null) sb.AppendLine(extract);

        sb.AppendLine(Drawer.Draw(obj));

        return WithDescription(sb.ToString());
    }
    
    public PanelBuilder WithDescription(string? description)
    {
        _panel.Description = description;
        return this;
    }
    
    public PanelBuilder WithObject(dynamic? data)
    {
        _panel.Description = Drawer.Draw(ViewData.ToDictionary(data));
        return this;
    }
    
    public PanelBuilder WithField(IField field)
    {
        _panel.Fields.Add(field);
        return this;
    }
    
    public PanelBuilder WithFields(IEnumerable<IField> fields)
    {
        return WithFields(fields.ToArray());
    }
    
    public PanelBuilder WithFields(params IField[] fields)
    {
        _panel.Fields.AddRange(fields);
        return this;
    }
    
    public PanelBuilder WithUri(Uri? uri)
    {
        _panel.Uri = uri;
        return this;
    }
    
    public PanelBuilder WithThumb(Uri? uri)
    {
        _panel.Thumb = uri;
        return this;
    }
    
    public PanelBuilder WithImage(Uri? uri)
    {
        _panel.Image = uri;
        return this;
    }
    
    public PanelBuilder WithFooter(string? footer)
    {
        _panel.Description = footer;
        return this;
    }
    
    public PanelBuilder WithFooterIcon(Uri? uri)
    {
        _panel.FooterIcon = uri;
        return this;
    }
    
    public PanelBuilder WithColor(Color? color)
    {
        _panel.Color = color;
        return this;
    }
    
    public PanelBuilder AsError()
    {
        _panel.Color = Color.Red;
        return this;
    }
    
    public PanelBuilder AsWarning()
    {
        _panel.Color = Color.Gold;
        return this;
    }
    
    public PanelBuilder AsSuccess()
    {
        _panel.Color = Color.Green;
        return this;
    }
    
    public PanelBuilder AsInfo()
    {
        _panel.Color = Color.Blue;
        return this;
    }

    public CustomPanel Build() => _panel;

    private readonly CustomPanel _panel;
}