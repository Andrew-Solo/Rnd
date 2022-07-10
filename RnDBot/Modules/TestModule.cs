using Discord.Interactions;

namespace RnDBot.Modules;

public class TestModule : InteractionModuleBase<SocketInteractionContext>
{
    //test hello world -> hello world
    [SlashCommand("test", "Echo an input")]
    public Task TestAsync([Summary("Text", "The text to echo")] string echo)
        => RespondAsync(echo);
}