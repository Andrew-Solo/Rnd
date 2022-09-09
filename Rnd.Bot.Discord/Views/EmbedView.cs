using System.Text;
using Discord;
using Rnd.Bot.Discord.Models.Common;

namespace Rnd.Bot.Discord.Views;

public static class EmbedView
{
    public static Embed[] Build(IPanelList panelList)
    {
        return panelList.Panels.Select(Build).ToArray();
    }
    
    public static Embed Build(IPanel panel)
    {
        var eb = new EmbedBuilder
        {
            Title = panel.Title,
            Description = panel.Description,
            Fields = new List<EmbedFieldBuilder>(panel.Fields?.Select(Build) ?? Array.Empty<EmbedFieldBuilder>()),

            Url = panel.Uri?.ToString(),
            ThumbnailUrl = panel.Thumb?.ToString(),
            ImageUrl = panel.Image?.ToString(),

            Footer = new EmbedFooterBuilder()
                .WithText(panel.Footer)
                .WithIconUrl(panel.FooterIcon?.ToString()),

            Color = panel.Color
        };

        return eb.Build();
    }

    public static EmbedFieldBuilder Build(IField field)
    {
        return new EmbedFieldBuilder
        {
            Name = field.Name,
            Value = Build(field.Value, field.Type),
            IsInline = field.IsInline
        };
    }

    public static string Build(object? value, ValueType type)
    {
        const string defaultValue = "—";
        string? result = null;
        
        switch (type)
        {
            case ValueType.Text:
            {
                result = value as string;
                break;
            }
            case ValueType.Cursive:
            {
                result = $"*{value}*";
                break;
            }
            case ValueType.Bold:
            {
                result = $"**{value}**";
                break;
            }
            case ValueType.BoldCursive:
            {
                result = $"***{value}***";
                break;
            }
            case ValueType.Mono:
            {
                result = $"`{value}`";
                break;
            }
            case ValueType.Spoiler:
            {
                result = $"||{value}||";
                break;
            }
            case ValueType.List:
            {
                if (value is not string[] list) break;
                
                result = string.Join("\n" , list.Select(i => $"– {i}"));
                
                break;
            }
            case ValueType.Modifier:
            {
                if (value is not int modifier) break;
                var prefix = (int?) modifier >= 0 ? "+" : "";
                
                result = $"```md\n# {prefix}{(int?) modifier}\n```";
                
                break;
            }
            case ValueType.InlineModifier:
            {
                if (value is not int modifier) break;
                var prefix = (int?) modifier >= 0 ? "+" : "";
                
                result = $"`{prefix}{(int?) modifier}`";
                
                break;
            }
            case ValueType.Counter:
            {
                var counter = value as (int, int)?;
                if (counter == null) break;
                var (current, max) = counter.Value;
                
                result = $"```md\n<_{current} / _{max}>\n```";
                
                break;
            }
            case ValueType.Dictionary:
            {
                if (value is not Dictionary<string, string> dictionary) break;

                var sb = new StringBuilder();
                sb.AppendLine("```md");
                
                foreach (var (left, right) in dictionary)
                {
                    sb.AppendLine($"[{left}]({right})");
                }
                
                sb.AppendLine("```");

                result = sb.ToString();
                
                break;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        return string.IsNullOrWhiteSpace(result) ? defaultValue : result;
    }

    public static Embed Error(params string[] errors)
    {
        var panel = new CommonPanel("Ошибка валидации")
        {
            Color = Color.Red,
            Description = Build(errors, ValueType.List)
        };

        return Build(panel);
    }
}