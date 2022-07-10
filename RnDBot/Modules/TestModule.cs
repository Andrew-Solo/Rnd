using Discord.Commands;

namespace RnDBot.Modules;

[Group("rnd")]
public class TestModule : ModuleBase<SocketCommandContext>
{
    // /rnd test hello world -> hello world
    [Command("test")]
    [Summary("Echoes a message.")]
    public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
        => ReplyAsync(echo);
}