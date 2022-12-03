using Discord.Interactions;
using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Bot.Discord.Data;
using Rnd.Bot.Discord.Sessions;
using Rnd.Bot.Discord.Views.Fields;
using Rnd.Bot.Discord.Views.Panels;

namespace Rnd.Bot.Discord.Controllers;

[Group("game", "Команды для управления играми")]
public class GameController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public SessionProvider Provider { get; set; } = null!;
    
    [AutocompleteCommand("game-id", "show")]
    public async Task GameNameAutocomplete()
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var games = await client.Games.ListOrExceptionAsync();
        
        var autocomplete = new Autocomplete<GameModel>(games, g => g.Name, g => g.Id.ToString());
        
        await autocomplete.RespondAsync(Context);
    }
    
    [SlashCommand("show", "Показать данные игры")]
    public async Task ShowAsync(string gameId)
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var game = await client.Games.GetOrExceptionAsync(new Guid(gameId));

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

    [SlashCommand("create", "Создать игру")]
    public async Task CreateAsync(string name, string? title = null, string? description = null)
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var game = new GameFormModel
        {
            Name = name,
            Title = title,
            Description = description,
        };

        var response = await client.Games.AddAsync(game);

        await this.ApiResponseAsync("Игра создана", response);
    }
    
    [AutocompleteCommand("game-id", "edit")]
    public async Task GameNameEditAutocomplete() => await GameNameAutocomplete();
    
    [SlashCommand("edit", "Отредактировать игру")]
    public async Task EditAsync(string gameId, string? name = null, string? title = null, string? description = null)
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);
        
        var game = new GameFormModel
        {
            Name = name,
            Title = title,
            Description = description,
        };
        
        var response = await client.Games.EditAsync(game, new Guid(gameId));

        await this.ApiResponseAsync("Игра отредактирована", response);
    }
 
    [AutocompleteCommand("game-id", "delete")]
    public async Task GameNameDeleteAutocomplete() => await GameNameAutocomplete();
    
    [SlashCommand("delete", "Удалить игру")]
    public async Task DeleteAsync(string gameId)
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var response = await client.Games.DeleteAsync(new Guid(gameId));
        
        await this.ApiResponseAsync("Игра удалено", response);
    }
}