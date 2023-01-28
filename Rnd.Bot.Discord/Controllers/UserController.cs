using Discord.Interactions;
using Rnd.Bot.Discord.Sessions;
using Rnd.Bot.Discord.Views.Panels;
using Rnd.Data;
using Rnd.Models;

namespace Rnd.Bot.Discord.Controllers;

[Group("user", "Команды для управления аккаунтом RndId")]
public class UserController : InteractionModuleBase<SocketInteractionContext>
{
    //Dependency Injections
    public DataContext Data { get; set; } = null!;
    public SessionProvider Provider { get; set; } = null!;
    
    [SlashCommand("show", "Показать данные аккаунта RndId, который привязан к текущему DiscordId")]
    public async Task ShowAsync()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Users.GetAsync(session.UserId);
        await this.EmbedResponseAsync(result);
    }
    
    [SlashCommand("login", "Привязать существующий аккаунт RndId к текущему DiscordId")]
    public async Task LoginAsync(
        [Summary("login", "Логин или email для входа в аккаунт")] string login, 
        [Summary("password", "Пароль для входа в аккаунт")] string password)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckNotAuthorized(session);

        var userResult = await Data.Users.LoginAsync(login, password);
        await this.CheckResultAsync(userResult);
        var user = userResult.Value;

        if (user.DiscordId == Context.User.Id)
        {
            session.Login(user);
            await this.EmbedResponseAsync(PanelBuilder
                    .WithTitle("Аккаунт уже привязан")
                    .AsSuccess());
        }

        var result = await Data.Users.BindDiscordAsync(user.Id, Context.User.Id);
        await this.EmbedResponseAsync(result, "Аккаунт привязан", () =>  session.Login(result.Value));
    }
    
    [SlashCommand("register", "Создать новый аккаунт RndId и привязать к текущему DiscordId")]
    public async Task RegisterAsync(
        [Summary("email", "Ваш email для входа в аккаунт")] string email, 
        [Summary("password", "Ваш пароль дял входа в аккаунт")] string password, 
        [Summary("login", "Ваш логин для входа в аккаунт, по умолчанию используется email")] string? login = null)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckNotAuthorized(session);

        var form = new User.Form
        {
            Email = email,
            Login = login,
            Password = password,
            DiscordId = Context.User.Id
        };

        var result = await Data.Users.CreateAsync(form);
        await this.EmbedResponseAsync(result, "Аккаунт создан", () =>  session.Login(result.Value));
    }
    
    [SlashCommand("edit", "Отредактировать данные аккаунта RndId, который привязан к текущему DiscordId")]
    public async Task EditAsync(
        [Summary("email", "Новый email аккаунта")] string? email = null, 
        [Summary("login", "Новый логин аккаунта")] string? login = null, 
        [Summary("password", "Новый пароль аккаунта")] string? password = null)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var form = new User.Form
        {
            Email = email,
            Login = login,
            Password = password
        };

        var result = await Data.Users.UpdateAsync(session.UserId, form);
        await this.EmbedResponseAsync(result, "Аккаунт отредактирован");
    }
    
    [SlashCommand("logout", "Отвязать аккаунт RndId от текущего DiscordId")]
    public async Task LogoutAsync()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);
        await this.CheckAuthorized(session);

        var result = await Data.Users.UnbindDiscordAsync(session.UserId);
        await this.EmbedResponseAsync(result, "Аккаунт отвязан", () =>  session.Logout());
    }
}