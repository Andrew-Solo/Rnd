namespace Rnd.Data;

public class UnitData : ModelData
{
    public Guid? ModuleId => this[nameof(ModuleId)].GetGuidOrNull();

}