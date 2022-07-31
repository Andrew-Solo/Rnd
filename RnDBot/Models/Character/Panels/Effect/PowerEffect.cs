using Newtonsoft.Json;
using RnDBot.Models.Common;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Character.Panels.Effect;

public class PowerEffect : IEffect
{
    public PowerEffect(string name, int maxModifier = 0, int currentModifier = 0)
    {
        Name = name;
        MaxModifier = maxModifier;
        CurrentModifier = currentModifier;
    }

    public string Name { get; }
    public int MaxModifier { get; }
    public int CurrentModifier { get; }

    public void ModifyPower(CounterField power)
    {
        power.Max += MaxModifier;
        power.Current += CurrentModifier;
        power.Name = power.Name.Trim('*') + "*";
    }

    [JsonIgnore] 
    public string View => $"**{Name}** Мощь {EmbedView.Build(CurrentModifier, ValueType.InlineModifier)}, " +
                                       $"Лимит {EmbedView.Build(MaxModifier, ValueType.InlineModifier)}";
}