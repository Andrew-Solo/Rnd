using Newtonsoft.Json;
using Rnd.Bot.Discord.Models.Common;
using Rnd.Bot.Discord.Views;
using ValueType = Rnd.Bot.Discord.Views.ValueType;

namespace Rnd.Bot.Discord.Models.Character.Panels.Effect;

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