using Microsoft.Extensions.Caching.Memory;
using Rnd.Data;

namespace Rnd.Bot.Discord.Sessions;

public class SessionProvider
{
    public SessionProvider(Func<IServiceProvider> getServices, TimeSpan? lifespan = null, MemoryCacheOptions? options = null)
    {
        _getServices = getServices;
        
        CacheOptions = options ?? new MemoryCacheOptions
        {
            SizeLimit = 100,
            ExpirationScanFrequency = TimeSpan.FromMinutes(10)
        };
        
        Cache = new MemoryCache(CacheOptions);
        Lifespan = lifespan ?? TimeSpan.FromDays(1);
    }

    public async Task<Session> GetSessionAsync(ulong discordId)
     {
        return await Cache.GetOrCreateAsync(discordId, async entry =>
        {
            entry.SlidingExpiration = Lifespan;
            entry.Size = 1;
            
            var db = Services.GetService(typeof(DataContext)) as DataContext 
                     ?? throw new NullReferenceException("DbContext service call error");
            
            var result = await db.Users.GetAsync((ulong) entry.Key);
            return Session.Create(result.IsSuccess ? result.Value.Id : null);
        }) ?? throw new NullReferenceException(); //TODO ???
    }

    public IServiceProvider Services => _getServices(); 
    public MemoryCacheOptions CacheOptions { get; }
    public IMemoryCache Cache { get; }
    public TimeSpan Lifespan { get; }
    
    private readonly Func<IServiceProvider> _getServices;
}