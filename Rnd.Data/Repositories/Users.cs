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
                .FirstOrDefaultAsync(predicate ?? (user => user.Id == userId))
        ).WithSelector(Model.SelectView);
    }

    public override async Task<Result<List<User>>> ListAsync(Guid userId)
    {
        return Result.Ok(
            await Data
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