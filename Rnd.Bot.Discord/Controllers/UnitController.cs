using Discord.Interactions;
using Rnd.Bot.Discord.Sessions;
using Rnd.Data;
using Rnd.Models;

namespace Rnd.Bot.Discord.Controllers;

[Group("unit", "Команды для управления юнитами персонажа")]
public class UnitController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Data { get; set; } = null!;
    public SessionProvider Provider { get; set; } = null!;
    
    [AutocompleteCommand("unit", "show")]
    public async Task UnitTitleAutocomplete()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var member = await Data.Members.GetAsync(session.UserId, null);
        await this.CheckResultAsync(member);

        var result = await Data.Units.ListAsync(member.Value.Id, null);
        await this.CheckResultAsync(result);
        
        var autocomplete = new Autocomplete<Unit>(result.Value, u => u.Fullname, m => m.Id.ToString());
        await autocomplete.RespondAsync(Context);
    }
    
    //TODO выбор чужих персонажей из игры
    [SlashCommand("show", "Показать юнит персонажа")]
    public async Task ShowAsync(
        [Summary("unit", "Юнит"), Autocomplete] string unitId
    )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var member = await Data.Members.GetAsync(session.UserId, null);
        await this.CheckResultAsync(member);
        
        var result = await Data.Units.GetAsync(member.Value.Id, null, unitId.AsGuid()!.Value); //TODO nullable
        await this.EmbedResponseAsync(result);
    }

    [AutocompleteCommand("character", "list")]
    public async Task CharacterTitleAutocomplete()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var member = await Data.Members.GetAsync(session.UserId, null);
        await this.CheckResultAsync(member);

        var result = await Data.Characters.ListAsync(member.Value.Id);
        await this.CheckResultAsync(result);
        
        var autocomplete = new Autocomplete<Character>(result.Value, c => c.Title, m => m.Id.ToString());
        await autocomplete.RespondAsync(Context);
    }
    
    [SlashCommand("list", "Показать все юниты персонажа")]
    public async Task ListAsync(
        [Summary("character", "Персонаж, оставьте пустым для выбора активного"), Autocomplete] string? characterId = null
    )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var member = await Data.Members.GetAsync(session.UserId, null);
        await this.CheckResultAsync(member);

        var result = await Data.Units.ListAsync(member.Value.Id, characterId.AsGuid());
        await this.EmbedResponseAsync(result.OnSuccess(us => us.Select(u => u.Fullname)), "Юниты");
    }
    
    //TODO create
    //TODO edit
    //TODO delete
}