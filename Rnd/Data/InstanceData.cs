namespace Rnd.Data;

public class InstanceData : ModelData
{
    public Guid? CreatorId => this[nameof(CreatorId)].GetGuidOrNull();
    public Guid? UnitId => this[nameof(UnitId)].GetGuidOrNull();
    public Guid? SpaceId => this[nameof(SpaceId)].GetGuidOrNull();
    public Dictionary<string, string> Properties => this[nameof(Properties)].GetDictionary();
}