namespace Rnd.Bot.Discord.Run;

public class Configuration
{
    public Configuration(string token, ulong developGuildId, string connectionString, string apiHostUrl)
    {
        Token = token;
        DevelopGuildId = developGuildId;
        ConnectionString = connectionString;
        ApiHostUri = new Uri(apiHostUrl);
    }

    public string Token { get; }
    public ulong DevelopGuildId { get; }
    public string ConnectionString { get; }
    public Uri ApiHostUri { get; }
} 