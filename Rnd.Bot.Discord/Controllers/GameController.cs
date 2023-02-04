using Discord.Interactions;
using Rnd.Bot.Discord.Sessions;
using Rnd.Data;
using Rnd.Models;

namespace Rnd.Bot.Discord.Controllers;

[Group("game", "Команды для управления играми")]
public class GameController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Data { get; set; } = null!;
    public SessionProvider Provider { get; set; } = null!;
    
    [AutocompleteCommand("game", "show")]
    public async Task GameNameAutocomplete()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Games.ListAsync(session.UserId);
        await this.CheckResultAsync(result);
        
        var autocomplete = new Autocomplete<Game>(result.Value, g => g.Name, g => g.Id.ToString());
        await autocomplete.RespondAsync(Context);
    }
    
    [SlashCommand("show", "Показать данные игры")]
    public async Task ShowAsync(
        [Summary("game", "Отображаемая игра, оставьте пустым для отображения активной игры"), Autocomplete] string? gameId = null
        )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Games.GetAsync(session.UserId, gameId.AsGuid());
        await this.EmbedResponseAsync(result, "Игра");
    }
    
    [SlashCommand("list", "Показать все мои игры")]
    public async Task ListAsync()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var result = await Data.Games.ListAsync(session.UserId);
        await this.EmbedResponseAsync(result.OnSuccess(gs => gs.Select(g => g.Name)), "Мои игры");
    }
    
    [AutocompleteCommand("game", "select")]
    public async Task GameNameSelectAutocomplete() => await GameNameAutocomplete();
    
    [SlashCommand("select", "Выбрать активную игру")]
    public async Task SelectAsync(
        [Summary("game", "Выбираемая игра, она станет активной"), Autocomplete] string gameId
    )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Games.SelectAsync(session.UserId, gameId.AsGuid());
        await this.EmbedResponseAsync(result, "Игра активна");
    }
    
    [AutocompleteCommand("module", "create")]
    public async Task ModuleNameAutocomplete()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Modules.ListAsync();
        await this.CheckResultAsync(result);
        
        var autocomplete = new Autocomplete<Module>(result.Value, m => m.VersionedTitle, g => g.Id.ToString());
        await autocomplete.RespondAsync(Context);
    }
    
    [SlashCommand("create", "Создать игру")]
    public async Task CreateAsync(
        [Summary("name", "Уникальное имя создаваемой игры")] string name, 
        [Summary("module", "Модуль по умочланию"), Autocomplete] string moduleId,
        [Summary("title", "Название игры")] string? title = null, 
        [Summary("description", "Описание игры")] string? description = null)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var form = new Game.Form
        {
            Name = name,
            ModuleId = moduleId.AsGuid(),
            Title = title,
            Description = description,
        };

        var result = await Data.Games.CreateAsync(session.UserId, form);
        await this.EmbedResponseAsync(result, "Игра создана");
    }
    
    [AutocompleteCommand("game", "edit")]
    public async Task GameNameEditAutocomplete() => await GameNameAutocomplete();
    
    [AutocompleteCommand("module", "edit")]
    public async Task ModuleNameEditAutocomplete() => await ModuleNameAutocomplete();
    
    //TODO Module parameter
    [SlashCommand("edit", "Отредактировать игру")]
    public async Task EditAsync(
        [Summary("game", "Редактируемая игра, оставьте пустым для редактрирования активной игры"), Autocomplete] string? gameId = null, 
        [Summary("module", "Модуль"), Autocomplete] string? moduleId = null,
        [Summary("name", "Новое уникальное имя игры")] string? name = null, 
        [Summary("title", "Новое название игры")] string? title = null, 
        [Summary("description", "Новое описание игры")] string? description = null)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var form = new Game.Form
        {
            Name = name,
            ModuleId = moduleId.AsGuid(),
            Title = title,
            Description = description,
        };
        
        var result = await Data.Games.UpdateAsync(session.UserId, gameId.AsGuid(), form);
        await this.EmbedResponseAsync(result, "Игра отредактирована");
    }
 
    [AutocompleteCommand("game", "delete")]
    public async Task GameNameDeleteAutocomplete() => await GameNameAutocomplete();
    
    //TODO модаль: вы точно хотите удалить игру?
    [SlashCommand("delete", "Удалить игру")]
    public async Task DeleteAsync(
        [Summary("game", "Удаляемая игра, оставьте пустым для удаления активной игры"), Autocomplete] string? gameId = null
        )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Games.DeleteAsync(session.UserId, gameId.AsGuid());
        await this.EmbedResponseAsync(result, "Игра удалена");
    }
}