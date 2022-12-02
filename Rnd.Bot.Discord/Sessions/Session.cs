using Rnd.Api.Client.Clients;
using Rnd.Bot.Discord.Data;

namespace Rnd.Bot.Discord.Sessions;

public class Session
{
    private Session(Uri host, Account? account)
    {
        Client = new BasicClient(host);
        Account = account;
        Created = DateTime.Now;
    }

    public static async Task<Session> CreateAsync(Uri host, Account? account)
    {
        var session = new Session(host, account);

        if (account != null)
        {
            await session.Client.LoginAsync(account.RndId);
        }

        return session;
    }

    public async Task LogoutAsync()
    {
        Account = null;
        await Client.LogoutAsync();
    }
    
    public void LoginAsync(ulong discordId, Guid rndId)
    {
        Account = new Account
        {
            Id = Guid.NewGuid(),
            Authorized = new DateTimeOffset(DateTime.Now).UtcDateTime,
            DiscordId = discordId,
            RndId = rndId
        };
    }

    public BasicClient Client { get; }
    public Account? Account { get; private set; }
    public DateTimeOffset Created { get; }
}