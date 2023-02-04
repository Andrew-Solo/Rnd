using System.Drawing;
using Discord.Interactions;
using Rnd.Bot.Discord.Sessions;
using Rnd.Data;
using Rnd.Models;

namespace Rnd.Bot.Discord.Controllers;

[Group("character", "Команды для управления персонажами участника")]
public class CharacterController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Data { get; set; } = null!;
    public SessionProvider Provider { get; set; } = null!;
    
    //TODO выполнение от имени другого участника
    [AutocompleteCommand("character", "show")]
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
    
    [SlashCommand("show", "Показать данные персонажа")]
    public async Task ShowAsync(
        [Summary("character", "Отображаемый персонаж, оставьте пустым для отображения активного"), Autocomplete] string? characterId = null
        )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var member = await Data.Members.GetAsync(session.UserId, null);
        await this.CheckResultAsync(member);

        var result = await Data.Characters.GetAsync(member.Value.Id, characterId.AsGuid());
        await this.EmbedResponseAsync(result);
    }
    
    [SlashCommand("list", "Показать всех персонажей участника")]
    public async Task ListAsync()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var member = await Data.Members.GetAsync(session.UserId, null);
        await this.CheckResultAsync(member);

        var result = await Data.Characters.ListAsync(member.Value.Id);
        await this.EmbedResponseAsync(result.OnSuccess(cs => cs.Select(m => m.Title)), "Персонажи");
    }
    
    [AutocompleteCommand("character", "select")]
    public async Task СharacterTitleSelectAutocomplete() => await CharacterTitleAutocomplete();
    
    [SlashCommand("select", "Выбрать активного персонажа")]
    public async Task SelectAsync(
        [Summary("сharacter", "Выбираемая игра, она станет активной"), Autocomplete] string сharacterId
    )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var member = await Data.Members.GetAsync(session.UserId, null);
        await this.CheckResultAsync(member);

        var result = await Data.Characters.SelectAsync(member.Value.Id, сharacterId.AsGuid());
        await this.EmbedResponseAsync(result, "Персонаж активен");
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
    
    [AutocompleteCommand("color", "create")]
    public async Task ColorNameAutocomplete()
    {
        //TODO завести локализацию?
        
        //Select non-system colors
        var colors  = Enum
            .GetValues<KnownColor>()
            .Where(c => (int) c > 26 && (int) c < 168 || (int) c == 75)
            .Select(c => Enum.GetName(typeof(KnownColor), c) ?? String.Empty)
            .Where(s => s != String.Empty);
        
        var autocomplete = new Autocomplete<string>(colors, s => s);
        
        await autocomplete.RespondAsync(Context);
    }

    [SlashCommand("create", "Создать персонажа")]
    public async Task CreateAsync(
        [Summary("title", "Имя персонажа")] string title,
        [Summary("module", "Модуль, оставьте пустым для выбора модуля по умолчанию"), Autocomplete] string? moduleId = null,
        [Summary("description", "Описание персонажа")] string? description = null,
        [Summary("color", "Выбор цвета из списка, цвет участника по умочланию"), Autocomplete] string? color = null,
        [Summary("color-hex", "Код цвета формата #RRGGBB, цвет участника по умочланию, используйте вместо color")] string? colorHex = null)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var member = await Data.Members.GetAsync(session.UserId, null);
        await this.CheckResultAsync(member);

        if (moduleId == null)
        {
            var game = await Data.Games.GetAsync(member.Value.UserId, member.Value.GameId);
            await this.CheckResultAsync(member);
            moduleId = game.Value.ModuleId.ToString();
        }
        
        var form = new Character.Form
        {
            OwnerId = member.Value.Id,
            ModuleId = moduleId.AsGuid(),
            Title = title,
            Description = description,
            ColorHtml = color ?? colorHex,
        };
        
        var result = await Data.Characters.CreateAsync(member.Value.Id, form);
        await this.EmbedResponseAsync(result, "Персонаж создан");
    }
    
    [AutocompleteCommand("character", "edit")]
    public async Task CharacterTitleEditAutocomplete() => await CharacterTitleAutocomplete();
    
    [AutocompleteCommand("module", "edit")]
    public async Task ModuleNameEditAutocomplete() => await ModuleNameAutocomplete();
    
    [AutocompleteCommand("color", "edit")]
    public async Task ColorNameEditAutocomplete() => await ColorNameAutocomplete();
    
    [SlashCommand("edit", "Отредактировать персонажа")]
    public async Task EditAsync(
        [Summary("character", "Редактируемый персонаж, оставьте пустым для редактрирования активного"), Autocomplete] string? characterId = null, 
        [Summary("module", "Модуль"), Autocomplete] string? moduleId = null,
        [Summary("title", "Имя персонажа")] string? title = null,
        [Summary("description", "Описание персонажа")] string? description = null,
        [Summary("color", "Выбор цвета из списка"), Autocomplete] string? color = null,
        [Summary("color-hex", "Код цвета формата #RRGGBB, используйте вместо color")] string? colorHex = null)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var member = await Data.Members.GetAsync(session.UserId, null);
        await this.CheckResultAsync(member);
        
        var form = new Character.Form
        {
            //Сменять владельца?
            ModuleId = moduleId.AsGuid(),
            Title = title,
            Description = description,
            ColorHtml = color ?? colorHex,
        };
        
        var result = await Data.Characters.UpdateAsync(member.Value.Id, characterId.AsGuid(), form);
        await this.EmbedResponseAsync(result, "Персонаж отредактирован");
    }
 
    [AutocompleteCommand("character", "delete")]
    public async Task CharacterTitleDeleteAutocomplete() => await CharacterTitleAutocomplete();
    
    //TODO модаль: вы точно хотите удалить персонажа?
    [SlashCommand("delete", "Удалить персонажа")]
    public async Task DeleteAsync(
        [Summary("character", "Исключаемый персонаж, оставьте пустым, чтобы исключить активного"), Autocomplete] string? characterId = null
        )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var member = await Data.Members.GetAsync(session.UserId, null);
        await this.CheckResultAsync(member);

        var result = await Data.Characters.DeleteAsync(member.Value.Id, characterId.AsGuid());
        await this.EmbedResponseAsync(result, "Персонаж удален");
    }
}