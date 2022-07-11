namespace RnDBot.Run;

public class Configuration
{
    public Configuration(string token, ulong developGuildId, string connectionString)
    {
        Token = token;
        DevelopGuildId = developGuildId;
        ConnectionString = connectionString;
    }

    public string Token { get; }
    public ulong DevelopGuildId { get; }
    public string ConnectionString { get; }
} 