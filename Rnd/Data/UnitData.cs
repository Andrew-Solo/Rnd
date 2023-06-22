namespace Rnd.Data;

public class UnitData : ModelData
{
    public Guid? ModuleId => this[nameof(ModuleId)].GetGuidOrNull();
    public bool? Default => this[nameof(Default)].GetBooleanOrNull();
    public bool? Hidden => this[nameof(Hidden)].GetBooleanOrNull();
    public byte? Order => this[nameof(Order)].GetByteOrNull();
}