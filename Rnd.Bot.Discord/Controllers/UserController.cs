using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using Rnd.Api.Client.Clients;
using Rnd.Api.Client.Models.Basic.User;
using Rnd.Bot.Discord.Data;
using Rnd.Bot.Discord.Sessions;
using Rnd.Bot.Discord.Views.Panels;

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
        var client = await Provider.GetClientAsync(Context.User.Id);

        await this.CheckAuthorized(client);
        
        await this.EmbedResponseAsync(PanelBuilder.WithTitle("Текущий аккаунт").ByClass(client.User));
    }
    
    [SlashCommand("login", "Привязать существующий аккаунт RndId к текущему DiscordId")]
    public async Task LoginAsync(
        [Summary("login", "Логин или email для входа в аккаунт")] string login, 
        [Summary("password", "Пароль для входа в аккаунт")] string password)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);

        await this.CheckNotAuthorized(session.Client);

        var response = await session.Client.LoginAsync(login, password);

        await this.CheckApiResponseAsync(response);
        
        session.LoginAsync(Context.User.Id, response.Value!.Id);
        await Data.Accounts.AddAsync(session.Account!);
        await Data.SaveChangesAsync();
        
        await this.EmbedResponseAsync(PanelBuilder.WithTitle("Аккаунт привязан").AsSuccess());
    }
    
    [SlashCommand("register", "Создать новый аккаунт RndId и привязать к текущему DiscordId")]
    public async Task RegisterAsync(
        [Summary("email", "Ваш email для входа в аккаунт")] string email, 
        [Summary("password", "Ваш пароль дял входа в аккаунт")] string password, 
        [Summary("login", "Ваш логин для входа в аккаунт, по умолчанию используется email")] string? login = null)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);

        await this.CheckNotAuthorized(session.Client);

        var form = new UserFormModel
        {
            Email = email,
            Login = login,
            Password = password
        };
        
        var response = await session.Client.RegisterAsync(form);

        await this.CheckApiResponseAsync(response);
        
        session.LoginAsync(Context.User.Id, response.Value!.Id);
        await Data.Accounts.AddAsync(session.Account!);
        await Data.SaveChangesAsync();
        
        await this.EmbedResponseAsync(PanelBuilder.WithTitle("Аккаунт создан").AsSuccess());
    }
    
    [SlashCommand("edit", "Отредактировать данные аккаунта RndId, который привязан к текущему DiscordId")]
    public async Task EditAsync(
        [Summary("email", "Новый email аккаунта")] string? email = null, 
        [Summary("login", "Новый логин аккаунта")] string? login = null, 
        [Summary("password", "Новый пароль аккаунта")] string? password = null)
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var form = new UserFormModel
        {
            Email = email,
            Login = login,
            Password = password
        };

        var response = await client.EditAccountAsync(form);
        
        await this.CheckApiResponseAsync(response);

        await this.EmbedResponseAsync(PanelBuilder.WithTitle("Аккаунт отредактирован").AsSuccess());
    }
    
    [SlashCommand("logout", "Отвязать аккаунт RndId от текущего DiscordId")]
    public async Task LogoutAsync()
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);

        await this.CheckAuthorized(session.Client);
        
        if (session.Account != null)
        {
            Data.Remove(session.Account);
            await Data.SaveChangesAsync();
        }
        
        await session.LogoutAsync();

        await this.EmbedResponseAsync(PanelBuilder.WithTitle("Аккаунт отвязан").AsSuccess());
    }
}