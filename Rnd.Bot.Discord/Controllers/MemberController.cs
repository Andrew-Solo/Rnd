using System.Drawing;
using Discord;
using Discord.Interactions;
using Rnd.Api.Client.Clients;
using Rnd.Api.Client.Models.Basic.Member;
using Rnd.Bot.Discord.Sessions;
using Rnd.Bot.Discord.Views.Fields;
using Rnd.Bot.Discord.Views.Panels;

namespace Rnd.Bot.Discord.Controllers;

[Group("member", "Команды для управления участниками игры")]
public class MemberController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public SessionProvider Provider { get; set; } = null!;
    
    [AutocompleteCommand("member", "show")]
    public async Task MemberNameAutocomplete()
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var members = await client.Games[Guid.Empty].Members.ListOrExceptionAsync();
        
        var autocomplete = new Autocomplete<MemberModel>(members, m => m.Nickname, m => m.Id.ToString());
        
        await autocomplete.RespondAsync(Context);
    }
    
    [SlashCommand("show", "Показать данные участника")]
    public async Task ShowAsync(
        [Summary("member", "Отображаемый участник, оставьте пустым для отображения себя"), Autocomplete] string? memberId = null
        )
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var member = await client.Games[Guid.Empty].Members.GetOrExceptionAsync(new Guid(memberId ?? Guid.Empty.ToString()));

        await this.EmbedResponseAsync(PanelBuilder.WithTitle("Участник").ByObject(member));
    }
    
    [SlashCommand("list", "Показать все мои игры")]
    public async Task ListAsync()
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var members = await client.Games[Guid.Empty].Members.ListOrExceptionAsync();

        await this.EmbedResponseAsync(FieldBuilder.WithName("Участники игры").WithValue(members.Select(g => g.Nickname)).Build().AsPanel());
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
        var colors  = Enum.GetNames(typeof(KnownColor));
        
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
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var userClient = await Provider.GetClientAsync(user.Id);

        if (userClient.Status != ClientStatus.Ready)
        {
            await this.EmbedResponseAsync(PanelBuilder.WithTitle("Указанный пользователь не авторизован").AsError());
            return;
        }
        
        var form = new MemberFormModel
        {
            Nickname = nickname,
            Role = role,
            ColorHtml = color ?? colorHex,
            UserId = userClient.User.Id,
        };

        var response = await client.Games[Guid.Empty].Members.AddAsync(form);

        await this.ApiResponseAsync("Участник создан", response);
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
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);
        
        var form = new MemberFormModel
        {
            Nickname = nickname,
            Role = role,
            ColorHtml = color ?? colorHex,
        };
        
        var response = await client.Games[Guid.Empty].Members.EditAsync(form, new Guid(memberId ?? Guid.Empty.ToString()));

        await this.ApiResponseAsync("Участник отредактирован", response);
    }
 
    [AutocompleteCommand("member", "delete")]
    public async Task MemberNameDeleteAutocomplete() => await MemberNameAutocomplete();
    
    //TODO модаль: вы точно хотите удалить участника?
    [SlashCommand("delete", "Удалить участника")]
    public async Task DeleteAsync(
        [Summary("member", "Исключаемый участник, оставьте пустым, чтобы исключить себя"), Autocomplete] string? memberId = null
        )
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var response = await client.Games[Guid.Empty].Members.DeleteAsync(new Guid(memberId ?? Guid.Empty.ToString()));
        
        await this.ApiResponseAsync("Участник удален", response);
    }
}