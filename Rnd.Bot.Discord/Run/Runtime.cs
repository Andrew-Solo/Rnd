using Discord;
using Discord.WebSocket;

namespace Rnd.Bot.Discord.Run;

public class Runtime
{
    public Runtime()
    {
        IsStopped = true;
        Discord = Setup.CreateDiscord();
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
        await Discord.LoginAsync(TokenType.Bot, Setup.Configuration.Token);
        await Discord.StartAsync();
        await Task.Delay(-1);
    }
}