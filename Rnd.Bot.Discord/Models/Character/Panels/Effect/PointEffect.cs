using Newtonsoft.Json;
using Rnd.Bot.Discord.Models.Character.Fields;
using Rnd.Bot.Discord.Models.Glossaries;
using Rnd.Bot.Discord.Views;
using ValueType = Rnd.Bot.Discord.Views.ValueType;

namespace Rnd.Bot.Discord.Models.Character.Panels.Effect;

public class PointEffect : IEffect
{
    public PointEffect(string name, PointerType pointerType, int modifier = 0)
    {
        PointerType = pointerType;
        Name = name;
        Modifier = modifier;
    }
    
    public string Name { get; }
    public PointerType PointerType { get; }
    public int Modifier { get; }

    public void ModifyPointer(Pointer pointer)
    {
        if (pointer.PointerType != PointerType) return;
        
        pointer.Max += Modifier;
        pointer.Current += Modifier;
        pointer.Modified = true;

        if (pointer.Max >= 0) return;
        
        pointer.Current -= pointer.Max;
        pointer.Max = 0;
    }
    
    [JsonIgnore]
    public string View => $"**{Name}** {Glossary.PointerNames[PointerType]} " +
                          $"`{EmbedView.Build(Modifier, ValueType.InlineModifier)}`";
}