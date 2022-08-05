using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RnDBot.Data;

namespace RnDBot.Run;

public static class DiscordInitializer
{
    public static DiscordSocketClient Initialize(Func<LogMessage, Task> logHandler, Configuration configuration)
    {
        _configuration = configuration;
        
        Discord.Log += logHandler;
        Discord.Ready += ClientReady;
        
        _services = new ServiceCollection()
            .AddSingleton(Discord)
            .AddSingleton(Interaction)
            .AddDbContext<DataContext>(builder => builder.UseSqlite(_configuration.ConnectionString))
            .BuildServiceProvider();

        return Discord;
    }

    private static async Task ClientReady()
    {
        await Interaction.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

#if DEBUG
        await Interaction.RegisterCommandsToGuildAsync(_configuration.DevelopGuildId);
#else
        await Discord.Rest.DeleteAllGlobalCommandsAsync();
        await Interaction.RegisterCommandsGloballyAsync();
#endif

        Discord.InteractionCreated += async interaction =>
        {
            var context = new SocketInteractionContext(Discord, interaction);
            await Interaction.ExecuteCommandAsync(context, _services);
        };
    }
    
    //TODO нехорошо, в иделе как-то зарефакторить этот класс
    private static IServiceProvider _services = null!;
    private static Configuration _configuration = null!;
    
    private static readonly DiscordSocketClient Discord = new();
    private static readonly InteractionService Interaction = new(Discord);
}