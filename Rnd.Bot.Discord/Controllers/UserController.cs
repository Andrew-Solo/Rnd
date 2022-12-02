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
    public async Task LoginAsync(string login, string password)
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
    public async Task RegisterAsync(string email, string password, string? login = null)
    {
        var session = await Provider.GetSessionAsync(Context.User.Id);

        await this.CheckNotAuthorized(session.Client);

        var response = await session.Client.RegisterAsync(new UserFormModel { Email = email, Login = login, Password = password });

        await this.CheckApiResponseAsync(response);
        
        session.LoginAsync(Context.User.Id, response.Value!.Id);
        await Data.Accounts.AddAsync(session.Account!);
        await Data.SaveChangesAsync();
        
        await this.EmbedResponseAsync(PanelBuilder.WithTitle("Аккаунт создан").AsSuccess());
    }
    
    [SlashCommand("edit", "Отредактировать данные аккаунта RndId, который привязан к текущему DiscordId")]
    public async Task EditAsync(string? email = null, string? login = null, string? password = null)
    {
        var client = await Provider.GetClientAsync(Context.User.Id);
        
        await this.CheckAuthorized(client);

        var response = await client.EditAccountAsync(new UserFormModel { Email = email, Login = login, Password = password });
        
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