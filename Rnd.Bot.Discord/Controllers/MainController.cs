using Discord.Interactions;
using Rnd.Bot.Discord.Sessions;
using Rnd.Constants;
using Rnd.Data;
using Rnd.Models;

namespace Rnd.Bot.Discord.Controllers;

public class MainController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Data { get; set; } = null!;
    public SessionProvider Provider { get; set; } = null!;
    
    public async Task UnitAutocomplete(Func<Unit, bool> predicate)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Units.ListAsync(session.UserId);
        await this.CheckResultAsync(result);
        
        var autocomplete = new Autocomplete<Unit>(
            result.Value.Where(predicate), 
            u => u.Title ?? u.Fullname, 
            m => m.Id.ToString()
        );
        
        await autocomplete.RespondAsync(Context);
    }

    [AutocompleteCommand("name", "get")]
    public async Task GetAutocomplete() => await UnitAutocomplete(u => 
        u.Role is UnitRole.Variable or UnitRole.Constant or UnitRole.Expression);
    
    [SlashCommand("get", "Получить характеристику персонажа")]
    public async Task ShowAsync(
        [Summary("name", "Имя характеристики"), Autocomplete] string unitId
    )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var result = await Data.Units.GetAsync(session.UserId, unitId.AsGuid());
        await this.EmbedResponseAsync(result);
    }
    
    [AutocompleteCommand("name", "set")]
    public async Task SetAutocomplete() => await UnitAutocomplete(u => u.Role is UnitRole.Variable);
    
    [SlashCommand("set", "Установить значение характеристики персонажа")]
    public async Task SetAsync(
        [Summary("name", "Имя характеристики"), Autocomplete] string unitId,
        [Summary("value", "Значение характеристики"), Autocomplete] string value
    )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var result = await Data.Units.SetAsync(session.UserId, unitId.AsGuid(), value);
        await this.EmbedResponseAsync(result);
    }
     
    [AutocompleteCommand("name", "act")]
    public async Task ActAutocomplete() => await UnitAutocomplete(u => u.Role is UnitRole.Function);
    
    [SlashCommand("act", "Выполнить действие персонажа")]
    public async Task ActAsync(
        [Summary("name", "Имя действия"), Autocomplete] string unitId,
        [Summary("parameters", "Параметры действия"), Autocomplete] string parameters
    )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var result = await Data.Units.ActAsync(session.UserId, unitId.AsGuid(), parameters);
        await this.EmbedResponseAsync(result);
    }
}