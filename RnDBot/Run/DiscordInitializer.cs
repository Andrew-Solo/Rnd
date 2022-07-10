using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace RnDBot.Run;

public static class DiscordInitializer
{
    public static DiscordSocketClient Initialize(Func<LogMessage, Task> logHandler, ulong developGuildId)
    {
        _developGuildId = developGuildId;
        Discord.Log += logHandler;
        Discord.Ready += ClientReady;

        return Discord;
    }

    private static async Task ClientReady()
    {
        await Interaction.AddModulesAsync(Assembly.GetEntryAssembly(), Services);

#if DEBUG
        await Interaction.RegisterCommandsToGuildAsync(_developGuildId);
#else
        await _interactionService.RegisterCommandsGloballyAsync();
#endif

        Discord.InteractionCreated += async interaction =>
        {
            var context = new SocketInteractionContext(Discord, interaction);
            await Interaction.ExecuteCommandAsync(context, Services);
        };
    }
    
    private static ulong _developGuildId;
    private static readonly DiscordSocketClient Discord = new();
    private static readonly InteractionService Interaction = new(Discord);
    private static readonly IServiceProvider Services = new ServiceCollection()
        .AddSingleton(Discord)
        .AddSingleton(Interaction)
        .BuildServiceProvider();
}