using Discord;
using Newtonsoft.Json;
using Rnd.Api.Client.Responses;
using Rnd.Bot.Discord.Views.Fields;

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
    
    public PanelBuilder WithDescription(string? description)
    {
        _panel.Description = description;
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
    
    public PanelBuilder ByErrors(Errors? errors)
    {
        foreach (var (name, value) in errors ?? new Errors())
        {
            WithField(FieldBuilder.WithName(name).Inline().WithValue(value).Build());
        }

        AsError();

        return this;
    }
    
    public PanelBuilder ByClass<T>(T? value) where T : class
    {
        var json = JsonConvert.SerializeObject(value);
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object?>>(json);
        _panel.Description = FieldBuilder.WithName(_panel.Title).Inline().WithValue(dictionary).Build().AsPanel().Description;
        return this;
    }

    public CustomPanel Build() => _panel;

    private readonly CustomPanel _panel;
}