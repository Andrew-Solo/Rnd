using Rnd.Primitives;

namespace Rnd.Data;

public class MethodData : ModelData
{
    public Guid? UnitId => this[nameof(UnitId)].GetGuidOrNull();
    public Methodology? Methodology => this[nameof(Methodology)].GetEnumOrNull<Methodology>();
    public Guid? ReturnId => this[nameof(ReturnId)].GetGuidOrNull();
    public string? Value => this[nameof(Value)].GetRawText();
}