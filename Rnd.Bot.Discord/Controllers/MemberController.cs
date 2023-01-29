using System.Drawing;
using Discord;
using Discord.Interactions;
using Rnd.Bot.Discord.Sessions;
using Rnd.Constants;
using Rnd.Data;
using Rnd.Models;

namespace Rnd.Bot.Discord.Controllers;

[Group("member", "Команды для управления участниками игры")]
public class MemberController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Data { get; set; } = null!;
    public SessionProvider Provider { get; set; } = null!;
    
    //TODO система приглашений
    [AutocompleteCommand("member", "show")]
    public async Task MemberNameAutocomplete()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Members.ListAsync(session.UserId);
        await this.CheckResultAsync(result);
        
        var autocomplete = new Autocomplete<Member>(result.Value, m => m.Nickname, m => m.Id.ToString());
        await autocomplete.RespondAsync(Context);
    }
    
    [SlashCommand("show", "Показать данные участника")]
    public async Task ShowAsync(
        [Summary("member", "Отображаемый участник, оставьте пустым для отображения себя"), Autocomplete] string? memberId = null
        )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Members.GetAsync(session.UserId, null, memberId.AsGuid());
        await this.EmbedResponseAsync(result);
    }
    
    [SlashCommand("list", "Показать всех участников игры")]
    public async Task ListAsync()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Members.ListAsync(session.UserId);
        await this.EmbedResponseAsync(result.OnSuccess(ms => ms.Select(m => m.Nickname)), "Участники");
    }
    
    [AutocompleteCommand("role", "create")]
    public async Task RoleNameAutocomplete()
    {
        //TODO завести локализацию
        var roles = new List<string>
        {
            "Admin",
            "Guide",
            "Player"
        };
        
        var autocomplete = new Autocomplete<string>(roles, s => s);
        
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

    [SlashCommand("create", "Создать участника")]
    public async Task CreateAsync(
        [Summary("user", "Пользователь, приглашаемый в игру")] IUser user,
        [Summary("nickname", "Прозвище участника, логин по умолчанию")] string? nickname = null, 
        [Summary("role", "Роль участника, игрок по умолчанию"), Autocomplete] string? role = null, 
        [Summary("color", "Выбор цвета из списка, случайный по умолчанию"), Autocomplete] string? color = null,
        [Summary("color-hex", "Код цвета формата #RRGGBB, случайный по умолчанию, используйте вместо color")] string? colorHex = null)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var userResult = await Data.Users.GetAsync(user.Id);
        await this.CheckResultAsync(userResult);
        
        var gameResult = await Data.Games.GetAsync(session.UserId);
        await this.CheckResultAsync(gameResult);
        
        var form = new Member.Form
        {
            GameId = gameResult.Value.Id,
            UserId = userResult.Value.Id,
            Role = role.AsEnum<MemberRole>(), //TODO проверить что оно default
            Nickname = nickname ?? userResult.Value.Login,
            ColorHtml = color ?? colorHex,
        };
        
        var result = await Data.Members.CreateAsync(session.UserId, form);
        await this.EmbedResponseAsync(result, "Участник создан");
    }
    
    [AutocompleteCommand("member", "edit")]
    public async Task MemberNameEditAutocomplete() => await MemberNameAutocomplete();
    
    [AutocompleteCommand("role", "edit")]
    public async Task RoleNameEditAutocomplete() => await RoleNameAutocomplete();
    
    [AutocompleteCommand("color", "edit")]
    public async Task ColorNameEditAutocomplete() => await ColorNameAutocomplete();
    
    [SlashCommand("edit", "Отредактировать участника")]
    public async Task EditAsync(
        [Summary("member", "Редактируемый участник, оставьте пустым для редактрирования себя"), Autocomplete] string? memberId = null, 
        [Summary("nickname", "Прозвище участника")] string? nickname = null, 
        [Summary("role", "Роль участника"), Autocomplete] string? role = null, 
        [Summary("color", "Выбор цвета из списка"), Autocomplete] string? color = null,
        [Summary("color-hex", "Код цвета формата #RRGGBB, используйте вместо color")] string? colorHex = null)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);
        
        var form = new Member.Form
        {
            Role = role.AsEnum<MemberRole>(), //TODO проверить что тут null
            Nickname = nickname,
            ColorHtml = color ?? colorHex,
        };
        
        var result = await Data.Members.UpdateAsync(session.UserId, null, memberId.AsGuid(), form);
        await this.EmbedResponseAsync(result, "Участник отредактирован");
    }
 
    [AutocompleteCommand("member", "delete")]
    public async Task MemberNameDeleteAutocomplete() => await MemberNameAutocomplete();
    
    //TODO модаль: вы точно хотите удалить участника?
    [SlashCommand("delete", "Удалить участника")]
    public async Task DeleteAsync(
        [Summary("member", "Исключаемый участник, оставьте пустым, чтобы исключить себя"), Autocomplete] string? memberId = null
        )
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Members.DeleteAsync(session.UserId, null, memberId.AsGuid());
        await this.EmbedResponseAsync(result, "Участник удален");
    }
}