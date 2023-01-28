namespace Rnd.Bot.Discord.Run;

public class Configuration
{
    public Configuration(string token, ulong developGuildId, string connectionString, string apiHostUrl)
    {
        Token = token;
        DevelopGuildId = developGuildId;
        ConnectionString = connectionString;
    }

    public string Token { get; }
    public ulong DevelopGuildId { get; }
    public string ConnectionString { get; }
} 