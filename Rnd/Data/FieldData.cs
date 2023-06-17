using Rnd.Primitives;
using Type = Rnd.Primitives.Type;

namespace Rnd.Data;

public class FieldData : ModelData
{
    public Guid? UnitId => this[nameof(UnitId)].GetGuidOrNull();
    public Guid? MethodId => this[nameof(MethodId)].GetGuidOrNull();
    public Type? Type => this[nameof(Type)].GetEnumOrNull<Type>();
    public Accessibility? Accessibility => this[nameof(Accessibility)].GetEnumOrNull<Accessibility>();
    public Interactivity? Interactivity => this[nameof(Interactivity)].GetEnumOrNull<Interactivity>();
    public Enumerating? Enumerating => this[nameof(Enumerating)].GetEnumOrNull<Enumerating>();
    public bool? Nullable => this[nameof(Nullable)].GetBooleanOrNull();
    public string? Value => this[nameof(Value)].GetRawText();
}