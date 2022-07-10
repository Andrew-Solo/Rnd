namespace RnDBot.Run;

public class Configuration
{
    public Configuration(string token, ulong developGuildId)
    {
        Token = token;
        DevelopGuildId = developGuildId;
    }

    public string Token { get; }
    public ulong DevelopGuildId { get; }
} 