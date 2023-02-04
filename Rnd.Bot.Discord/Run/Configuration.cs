namespace Rnd.Bot.Discord.Run;

public class Configuration
{
    public Configuration(
        string token, 
        ulong developGuildId, 
        string connectionString, 
        string modulesPath
    )
    {
        Token = token;
        DevelopGuildId = developGuildId;
        ConnectionString = connectionString;
        ModulesPath = modulesPath;
    }

    public string Token { get; }
    public ulong DevelopGuildId { get; }
    public string ConnectionString { get; }
    public string ModulesPath { get; }
} 