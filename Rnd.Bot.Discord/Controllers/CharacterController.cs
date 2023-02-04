using System.Drawing;
using Discord;
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
    
    //TODO система приглашений
    [AutocompleteCommand("character", "show")]
    public async Task CharacterNameAutocomplete()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var member = await Data.Members.GetAsync(session.UserId, null);
        await this.CheckResultAsync(member);

        var result = await Data.Characters.ListAsync(member.Value.Id);
        await this.CheckResultAsync(result);
        
        var autocomplete = new Autocomplete<Character>(result.Value, c => c.Name, m => m.Id.ToString());
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
        await this.EmbedResponseAsync(result.OnSuccess(cs => cs.Select(m => m.Name)), "Персонажи");
    }
    
    [AutocompleteCommand("module", "create")]
    public async Task ModuleNameAutocomplete()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Modules.ListAsync();
        await this.CheckResultAsync(result);
        
        var autocomplete = new Autocomplete<Module>(result.Value, m => m.Name, g => g.Id.ToString());
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
        [Summary("name", "Уникальное имя персонажа")] string name, 
        [Summary("module", "Игровая система, оставьте пустым для выбора системы по умолчанию"), Autocomplete] string? moduleId = null,
        [Summary("title", "Имя персонажа")] string? title = null,
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
            Name = name,
            Title = title,
            Description = description,
            ColorHtml = color ?? colorHex,
        };
        
        var result = await Data.Characters.CreateAsync(member.Value.Id, form);
        await this.EmbedResponseAsync(result, "Персонаж создан");
    }
    
    [AutocompleteCommand("character", "edit")]
    public async Task CharacterNameEditAutocomplete() => await CharacterNameAutocomplete();
    
    [AutocompleteCommand("module", "edit")]
    public async Task ModuleNameEditAutocomplete() => await ModuleNameAutocomplete();
    
    [AutocompleteCommand("color", "edit")]
    public async Task ColorNameEditAutocomplete() => await ColorNameAutocomplete();
    
    [SlashCommand("edit", "Отредактировать персонажа")]
    public async Task EditAsync(
        [Summary("character", "Редактируемый персонаж, оставьте пустым для редактрирования активного"), Autocomplete] string? characterId = null, 
        [Summary("name", "Уникальное имя персонажа")] string? name = null, 
        [Summary("module", "Игровая система"), Autocomplete] string? moduleId = null,
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
            Name = name,
            Title = title,
            Description = description,
            ColorHtml = color ?? colorHex,
        };
        
        var result = await Data.Characters.UpdateAsync(member.Value.Id, characterId.AsGuid(), form);
        await this.EmbedResponseAsync(result, "Персонаж отредактирован");
    }
 
    [AutocompleteCommand("character", "delete")]
    public async Task CharacterNameDeleteAutocomplete() => await CharacterNameAutocomplete();
    
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