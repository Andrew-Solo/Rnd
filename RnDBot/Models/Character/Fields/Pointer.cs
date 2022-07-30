using Newtonsoft.Json;
using RnDBot.Models.Glossaries;
using RnDBot.Views;
using ValueType = RnDBot.Views.ValueType;

namespace RnDBot.Models.Character.Fields;

/// <summary>
/// Point Counter
/// </summary>
public class Pointer : IField
{
    public Pointer(PointerType pointerType, int max, int? current = null)
    {
        PointerType = pointerType;
        Current = current ?? max;
        Max = max;
        Modified = false;
    }

    public PointerType PointerType { get; set; }
    public int Current { get; set; }
    public int Max { get; set; }
    public bool Modified { get; set; }

    [JsonIgnore]
    public string Name => Glossary.PointerNames[PointerType] + (Modified ? "*" : "");
    
    [JsonIgnore]
    public object Value => (Current, Max);
    
    [JsonIgnore]
    public ValueType Type => ValueType.Counter;
    
    [JsonIgnore]
    public bool IsInline => true;
}