using Discord;
using Discord.WebSocket;

namespace Rnd.Bot.Discord.Run;

public class Runtime
{
    public Runtime(string[] args)
    {
        IsStopped = true;
        Discord = Setup.CreateDiscord();
        if (args.Length > 0) _token = args[0];
        if (args.Length > 1) Setup.Configuration.AirtableToken = args[1];
    }
    
    public bool IsStopped { get; private set; }
    public DiscordSocketClient Discord { get; }

    public async Task RunAsync()
    {
        IsStopped = false;
        await InitAsync();
    }

    private async Task InitAsync()
    {
        await Discord.LoginAsync(TokenType.Bot, _token ?? Setup.Configuration.Token);
        await Discord.StartAsync();
        await Task.Delay(-1);
    }

    private readonly string? _token;
}