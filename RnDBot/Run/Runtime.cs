using System.Reflection;
using Discord;
using Discord.Interactions;
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
        _discord.Ready += ClientReady;

        _interactionService = new InteractionService(_discord);
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
        //await _commandHandler.InstallCommandsAsync();
        
        await _discord.LoginAsync(TokenType.Bot, Configuration.Token);
        await _discord.StartAsync();
        
        //TODO Костыль, убрать, нужно иметь возможность стопать приложение нормально
        await Task.Delay(-1);
    }
    
    private async Task ClientReady()
    {
        await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), null);

#if DEBUG
        await _interactionService.RegisterCommandsToGuildAsync(Configuration.DevelopGuildId);
#else
        await _interactionService.RegisterCommandsGloballyAsync();
#endif

        _discord.InteractionCreated += async interaction =>
        {
            var context = new SocketInteractionContext(_discord, interaction);
            await _interactionService.ExecuteCommandAsync(context, null);
        };
    }

    private readonly DiscordSocketClient _discord;
    private readonly InteractionService _interactionService;
}