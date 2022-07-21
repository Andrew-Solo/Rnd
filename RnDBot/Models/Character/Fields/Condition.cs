using RnDBot.Models.Glossaries;
using RnDBot.View;
using ValueType = RnDBot.View.ValueType;

namespace RnDBot.Models.Character.Fields;

public class Condition : IField
{
    public Condition(ConditionType conditionType, int max, int? current = null)
    {
        ConditionType = conditionType;
        Current = current ?? max;
        Max = max;
    }

    public ConditionType ConditionType { get; set; }
    public int Current { get; set; }
    public int Max { get; set; }

    public string Name => Glossary.ConditionNames[ConditionType];
    public object Value => (Current, Max);
    public ValueType Type => ValueType.Counter;
    public bool IsInline => true;
}