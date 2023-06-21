using Rnd.Primitives;

namespace Rnd.Data;

public class GroupData : ModelData
{
    public Guid? SpaceId => this[nameof(SpaceId)].GetGuidOrNull();
    public bool? DenyAll => this[nameof(DenyAll)].GetBooleanOrNull();
    public bool? DenyAllAsCreator => this[nameof(DenyAllAsCreator)].GetBooleanOrNull();
    public List<Permission>? Permissions => this[nameof(Permissions)].GetObject<List<Permission>>();
}