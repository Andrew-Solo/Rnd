using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Users : Repository<User, UserData>
{
    public Users(DataContext context, DbSet<User> data) : base(context, data) { }

    public override async Task<Result<User>> GetAsync(Request request, Expression<Func<User, bool>> predicate)
    {
        return Result.Found(
            await Data
                .FirstOrDefaultAsync(predicate)
        ).WithSelector(Model.SelectView);
    }

    public override Task<Result<User>> GetAsync(Request request)
    {
        return request.Users.IsAuto
            ? request.User.IsId
                ? GetAsync(request, user => user.Id == request.User.Id!.Value)
                : GetAsync(request, user => user.Name == request.User.Value)
            : request.Users.IsId
                ? GetAsync(request, user => user.Id == request.Users.Id!.Value)
                : GetAsync(request, user => user.Name == request.Users.Value);
    }

    public override async Task<Result<List<User>>> ListAsync(Request request)
    {
        return Result.Ok(
            await Data
                .OrderByDescending(u => u.Viewed)
                .ToListAsync()
        ).WithSelector(Model.SelectListView);
    }

    public override async Task<Result<User>> CreateAsync(Request request, UserData data)
    {
        var treeResult = User.Create(data);
        if (treeResult.Failed) return treeResult;
        
        Data.Add(treeResult.Value);
        await Context.SaveChangesAsync();
        
        return treeResult;
    }

    public override async Task<Result<User>> UpdateAsync(User tree, UserData data)
    {
        var result = tree.Update(data);
        if (result.Failed) return result.Cast<User>();
        
        Data.Update(tree);
        await Context.SaveChangesAsync();
        
        return result.Cast<User>();
    }

    public override async Task<Result<User>> DeleteAsync(User tree)
    {
        var result = tree.Delete();
        if (result.Failed) return result.Cast<User>();
        
        Data.Remove(tree);
        await Context.SaveChangesAsync();
        
        return result.Cast<User>();
    }
}