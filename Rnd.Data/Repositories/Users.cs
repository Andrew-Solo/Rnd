using System.Dynamic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Users : Repository<User>
{
    public Users(DataContext context, DbSet<User> data) : base(context, data) { }

    public Task<Result<User>> GetAsync(Guid userId)
    {
        return GetAsync(user => user.Id == userId);
    }
    
    public Task<Result<User>> GetAsync(string name)
    {
        return GetAsync(user => user.Name == name);
    }
    
    public Task<Result<User>> GetAsync(string name, string password)
    {
        return GetAsync(user => user.Name == name && user.PasswordHash == Hash.GenerateStringHash(password));
    }
    
    public Task<Result<User>> GetAsync(Provider provider, string identifier, string? secret = null)
    {
        return GetAsync(user => user.Associations.Contains(new Association(provider, identifier, secret)));
    }
    
    private async Task<Result<User>> GetAsync(Expression<Func<User, bool>> predicate)
    {
        return Result.Found(
            await Data
                .Include(user => user.Memberships)
                .FirstOrDefaultAsync(predicate)
        ).WithSelector(Model.SelectView);
    }
    
    public async Task<Result<List<User>>> ListAsync()
    {
        return Result.Ok(
            await Data
                .Include(user => user.Memberships)
                .OrderByDescending(user => user.Viewed)
                .ToListAsync()
        ).WithSelector(Model.SelectListView);
    }
    
    public async Task<Result<User>> CreateAsync(ExpandoObject data)
    {
        var user = User.Create(data);
        if (user.Failed) return user;
        
        Data.Add(user.Value);
        await Context.SaveChangesAsync();

        return user;
    }
    
    public async Task<Result<User>> UpdateAsync(Guid userId, ExpandoObject data)
    {
        var user = await GetAsync(userId);
        if (user.Failed) return user;
        
        var result = user.Value.Update(data);
        if (result.Failed) return result.Cast<User>();
        
        Data.Update(user.Value);
        await Context.SaveChangesAsync();

        return result.Cast<User>();
    }
    
    public async Task<Result<User>> DeleteAsync(Guid userId)
    {
        var user = await GetAsync(userId);
        if (user.Failed) return user;
        
        var result = user.Value.Delete();
        if (result.Failed) return result.Cast<User>();
        
        Data.Remove(user.Value);
        await Context.SaveChangesAsync();

        return result.Cast<User>();
    }
}