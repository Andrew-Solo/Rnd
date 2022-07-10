using Discord;
using Newtonsoft.Json;
using RnDBot.Run;

namespace RnDBot;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var config = await File.ReadAllTextAsync("config.json");
        var configuration = JsonConvert.DeserializeObject<Configuration>(config) ?? throw new InvalidOperationException();
        
        var runtime = new Runtime(configuration, LogConsole);
        await runtime.RunAsync();
    }
    
    private static Task LogConsole(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}