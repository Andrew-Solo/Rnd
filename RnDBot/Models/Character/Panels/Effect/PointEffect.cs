using Newtonsoft.Json;
using RnDBot.Models.Character.Fields;
using RnDBot.Models.Glossaries;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Character.Panels.Effect;

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
        if (pointer.PointerType == PointerType) pointer.Max += Modifier;
    }
    
    [JsonIgnore]
    public string View => $"**{Name}** {Glossary.PointerNames[PointerType]} " +
                          $"`{EmbedView.Build(Modifier, ValueType.InlineModifier)}`";
}