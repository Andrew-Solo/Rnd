using System.Diagnostics;
using System.Reflection;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Rnd.Bot.Discord.Run;

public static class Setup
{
    static Setup()
    {
        Discord = new DiscordSocketClient();
        Interaction = new InteractionService(Discord);
        
        Configuration = CreateConfiguration();
        Services = ConfigureServices();
    }

    public static DiscordSocketClient Discord { get; }
    public static InteractionService Interaction { get; }
    public static Configuration Configuration { get; }
    public static ServiceProvider Services { get; }
    
    public static DiscordSocketClient CreateDiscord()
    {
        Discord.Log += Logger;
        Discord.Ready += ReadyHandler;
        Discord.InteractionCreated += InteractionCreatedHandler;
        return Discord;
    }
    
    private static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddSingleton(Discord)
            .AddSingleton(Interaction)
            .BuildServiceProvider();
    }
    
    private static async Task InteractionCreatedHandler(SocketInteraction interaction)
    {
        var context = new SocketInteractionContext(Discord, interaction);
        await Interaction.ExecuteCommandAsync(context, Services);
    }
    
    private static async Task ReadyHandler()
    {
        await Interaction.AddModulesAsync(Assembly.GetEntryAssembly(), Services);
        DebugReadyHandler();
        ReleaseReadyHandler();
    }
    
    [Conditional("DEBUG")]
    private static async void DebugReadyHandler()
    {
        await Interaction.RegisterCommandsToGuildAsync(Configuration.DevelopGuildId);
    }

    [Conditional("RELEASE")]
    private static async void ReleaseReadyHandler()
    {
        await Discord.Rest.DeleteAllGlobalCommandsAsync();
        await Interaction.RegisterCommandsGloballyAsync();
    }
    
    private static Configuration CreateConfiguration()
    {
        var config = File.ReadAllText("config.json");
        return JsonConvert.DeserializeObject<Configuration>(config) 
                            ?? throw new InvalidOperationException();
    }
    
    private static Task Logger(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }
}