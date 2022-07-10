using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace RnDBot.Run;

public class Runtime
{
    public Runtime(Configuration configuration, Func<LogMessage, Task> logHandler)
    {
        Configuration = configuration;
        IsStopped = true;
        
        _discord = new DiscordSocketClient();
        _discord.Log += logHandler;

        _commandHandler = new CommandHandler(_discord);
    }
    
    public bool IsStopped { get; private set; }
    public Configuration Configuration { get; }

    public async Task RunAsync()
    {
        IsStopped = false;

        await InitAsync();
    }

    private async Task InitAsync()
    {
        await _commandHandler.InstallCommandsAsync();
        
        await _discord.LoginAsync(TokenType.Bot, Configuration.Token);
        await _discord.StartAsync();
        
        //TODO Костыль, убрать, нужно иметь возможность стопать приложение нормально
        await Task.Delay(-1);
    }

    private readonly DiscordSocketClient _discord;
    private readonly CommandHandler _commandHandler;
}