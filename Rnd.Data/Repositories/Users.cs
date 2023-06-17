using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Users : ModelRepository<User, UserData>
{
    public Users(DataContext context, DbSet<User> data) : base(context, data) { }

    public override async Task<Result<User>> GetAsync(Guid userId, Expression<Func<User, bool>>? predicate = null)
    {
        return Result.Found(
            await Data
                .Include(user => user.Memberships)
                .FirstOrDefaultAsync(predicate ?? (user => user.Id == userId))
        ).WithSelector(Model.SelectView);
    }
    
    // public Task<Result<User>> GetAsync(string name, string password)
    // {
    //     return GetAsync(user => user.Name == name && user.PasswordHash == Hash.GenerateStringHash(password));
    // }
    //
    // public Task<Result<User>> GetAsync(Provider provider, string identifier, string? secret = null)
    // {
    //     return GetAsync(user => user.Associations.Contains(new Association(provider, identifier, secret)));
    // }

    public override async Task<Result<List<User>>> ListAsync(Guid userId)
    {
        return Result.Ok(
            await Data
                .Include(user => user.Memberships)
                .OrderByDescending(user => user.Viewed)
                .ToListAsync()
        ).WithSelector(Model.SelectListView);
    }

    public override async Task<Result<User>> CreateAsync(Guid userId, UserData data)
    {
        var userResult = User.Create(data);
        if (userResult.Failed) return userResult;
        
        Data.Add(userResult.Value);
        await Context.SaveChangesAsync();
        
        return userResult;
    }

    public override async Task<Result<User>> UpdateAsync(User user, UserData data)
    {
        var result = user.Update(data);
        if (result.Failed) return result.Cast<User>();
        
        Data.Update(user);
        await Context.SaveChangesAsync();
        
        return result.Cast<User>();
    }

    public override async Task<Result<User>> DeleteAsync(User user)
    {
        var result = user.Delete();
        if (result.Failed) return result.Cast<User>();
        
        Data.Remove(user);
        await Context.SaveChangesAsync();
        
        return result.Cast<User>();
    }
}