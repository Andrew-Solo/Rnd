using Rnd.Bot.Discord.Run;

namespace Rnd.Bot.Discord;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var runtime = new Runtime();
        await runtime.RunAsync();
    }
}