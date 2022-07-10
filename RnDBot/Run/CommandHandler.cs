using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;

namespace RnDBot.Run;

public class CommandHandler
{
    public CommandHandler(DiscordSocketClient discord, CommandService? commands = null)
    {
        _commands = commands ?? new CommandService();
        _discord = discord;
    }
    
    public async Task InstallCommandsAsync()
    {
        _discord.MessageReceived += HandleCommandAsync;
        
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        var message = messageParam as SocketUserMessage;
        if (message == null) return;

        var argPos = 0;

        if (!(message.HasCharPrefix('/', ref argPos) || 
            message.HasMentionPrefix(_discord.CurrentUser, ref argPos)) ||
            message.Author.IsBot)
            return;

        var context = new SocketCommandContext(_discord, message);

        await _commands.ExecuteAsync(context, argPos, null);
    }
    
    private readonly DiscordSocketClient _discord;
    private readonly CommandService _commands;
}