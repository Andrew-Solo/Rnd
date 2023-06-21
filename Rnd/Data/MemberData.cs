using Rnd.Primitives;

namespace Rnd.Data;

public class MemberData : ModelData
{
    public Guid? UserId => this[nameof(UserId)].GetGuidOrNull();
    public Guid? SpaceId => this[nameof(SpaceId)].GetGuidOrNull();
    public DateTimeOffset? Active => this[nameof(Active)].GetDateTimeOffsetOrNull();
    public DateTimeOffset? Banned => this[nameof(Banned)].GetDateTimeOffsetOrNull();
}