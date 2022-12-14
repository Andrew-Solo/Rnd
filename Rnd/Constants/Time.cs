namespace Rnd.Constants;

public readonly struct Time
{
    public static DateTimeOffset Now => DateTimeOffset.Now.UtcDateTime;
    public static DateTimeOffset Zero => DateTimeOffset.FromUnixTimeMilliseconds(0).DateTime;
}