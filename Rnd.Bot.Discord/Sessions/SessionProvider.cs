using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Rnd.Api.Client.Clients;
using Rnd.Bot.Discord.Data;

namespace Rnd.Bot.Discord.Sessions;

public class SessionProvider
{
    public SessionProvider(Uri host, Func<IServiceProvider> getServices, TimeSpan? lifespan = null, MemoryCacheOptions? options = null)
    {
        _getServices = getServices;
        CacheOptions = options ?? new MemoryCacheOptions
        {
            SizeLimit = 100,
            ExpirationScanFrequency = TimeSpan.FromMinutes(10)
        };

        Host = host;
        Cache = new MemoryCache(CacheOptions);
        Lifespan = lifespan ?? TimeSpan.FromDays(1);
    }

    public async Task<ApiClient> GetClientAsync(ulong discordId)
    {
        return (await GetSessionAsync(discordId)).Client;
    }

    public async Task<Session> GetSessionAsync(ulong discordId)
    {
        return await Cache.GetOrCreateAsync(discordId, async entry =>
        {
            entry.SlidingExpiration = Lifespan;
            entry.Size = 1;
            
            var db = Services.GetService(typeof(DataContext)) as DataContext 
                     ?? throw new NullReferenceException("DbContext service call error");
            
            var account = await db.Accounts.FirstOrDefaultAsync();
            return await Session.CreateAsync(Host, account);
        });
    }


    public IServiceProvider Services => _getServices(); 
    public MemoryCacheOptions CacheOptions { get; }
    public IMemoryCache Cache { get; }
    public Uri Host { get; }
    public TimeSpan Lifespan { get; }
    
    private readonly Func<IServiceProvider> _getServices;
}