using Newtonsoft.Json;
using Rnd.Bot.Discord.Views;
using ValueType = Rnd.Bot.Discord.Views.ValueType;

namespace Rnd.Bot.Discord.Models.Common;

public class ModifierField : IField
{
    public ModifierField(string name, int value)
    {
        Name = name;
        IntValue = value;
    }

    public string Name { get; set; }
    public int IntValue { get; set; }
    
    [JsonIgnore]
    public object Value => IntValue;
    
    [JsonIgnore]
    public ValueType Type => ValueType.Modifier;
    
    [JsonIgnore]
    public bool IsInline => true;
}