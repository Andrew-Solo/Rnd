namespace Rnd.Bot.Discord.Run;

public class Configuration
{
    public Configuration(
        string token, 
        string airtableToken,
        ulong developGuildId,
        string defaultGame, 
        Dictionary<string, string> games
    )
    {
        Token = token;
        AirtableToken = airtableToken;
        DevelopGuildId = developGuildId;
        DefaultGame = defaultGame;
        Games = games;
    }

    public string Token { get; }
    public string AirtableToken { get; set; } //TODO set
    public ulong DevelopGuildId { get; }
    public string DefaultGame { get; }
    public Dictionary<string, string> Games { get; }
} 