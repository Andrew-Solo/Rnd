using RnDBot.Models.Glossaries;
using RnDBot.View;
using ValueType = RnDBot.View.ValueType;

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
    }

    public PointerType PointerType { get; set; }
    public int Current { get; set; }
    public int Max { get; set; }

    public string Name => Glossary.ConditionNames[PointerType];
    public object Value => (Current, Max);
    public ValueType Type => ValueType.Counter;
    public bool IsInline => true;
}