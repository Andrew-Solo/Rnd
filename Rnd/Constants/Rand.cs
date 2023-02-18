namespace Rnd.Constants;

public static class Rand
{
    public static Random Get { get; } = new(Guid.NewGuid().GetHashCode());
}