using Discord.Interactions;
using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Bot.Discord.Data;
using Rnd.Bot.Discord.Sessions;
using Rnd.Bot.Discord.Views.Fields;
using Rnd.Bot.Discord.Views.Panels;

namespace Rnd.Bot.Discord.Controllers;

[Group("user", "Команды для управления играми")]
public class GameController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Data { get; set; } = null!;
    public SessionProvider Provider { get; set; } = null!;
    
    [AutocompleteCommand("gameId", "show")]
    public async Task GameNameAutocomplete()
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var games = await client.Games.ListOrExceptionAsync();
        
        var autocomplete = new Autocomplete<GameModel>(games, g => g.Name, g => g.Id);
        
        await autocomplete.RespondAsync(Context);
    }
    
    [SlashCommand("show", "Показать данные игры")]
    public async Task ShowAsync(Guid gameId)
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var game = await client.Games.GetOrExceptionAsync(gameId);

        await this.EmbedResponseAsync(PanelBuilder.WithTitle("Игра").ByClass(game));
    }
    
    [SlashCommand("list", "Показать все мои игры")]
    public async Task ListAsync()
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var games = await client.Games.ListOrExceptionAsync();

        await this.EmbedResponseAsync(FieldBuilder.WithName("Мои игры").WithValue(games.Select(g => g.Name)).Build().AsPanel());
    }
}