using Rnd.Primitives;
using Type = Rnd.Primitives.Type;

namespace Rnd.Data;

public class FieldData : ModelData
{
    public Guid? UnitId => this[nameof(UnitId)].GetGuidOrNull();
    public Prototype? Prototype => this[nameof(Prototype)].GetEnumOrNull<Prototype>();
    public Type? Type => this[nameof(Type)].GetEnumOrNull<Type>();
    public Enumerating? Enumerating => this[nameof(Enumerating)].GetEnumOrNull<Enumerating>();
    public Accessibility? Accessibility => this[nameof(Accessibility)].GetEnumOrNull<Accessibility>();
    public bool? Readonly => this[nameof(Readonly)].GetBooleanOrNull();
    public bool? Hidden => this[nameof(Hidden)].GetBooleanOrNull();
    public bool? Modifiable => this[nameof(Modifiable)].GetBooleanOrNull();
    public bool? Nullable => this[nameof(Nullable)].GetBooleanOrNull();
    public string? Value => this[nameof(Value)].GetRawText();
}