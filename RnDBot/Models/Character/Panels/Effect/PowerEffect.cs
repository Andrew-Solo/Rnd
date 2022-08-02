using Newtonsoft.Json;
using RnDBot.Models.Common;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Character.Panels.Effect;

public class PowerEffect : IEffect
{
    public PowerEffect(string name, int modifier = 0)
    {
        Name = name;
        Modifier = modifier;
    }

    public string Name { get; }
    public int Modifier { get; }

    public void ModifyPower(CounterField power)
    {
        power.Current += Modifier;
        power.Name = power.Name.Trim('*') + "*";
    }

    [JsonIgnore] 
    public string View => $"**{Name}** Мощь {EmbedView.Build(Modifier, ValueType.InlineModifier)}";
}