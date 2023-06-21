using Rnd.Primitives;

namespace Rnd.Data;

public class SpaceData : ModelData
{
    public Guid? OwnerId => this[nameof(OwnerId)].GetGuidOrNull();
    public DateTimeOffset? Archived => this[nameof(Archived)].GetDateTimeOffsetOrNull();
}