using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Users : Repository<User, UserData>
{
    public Users(DataContext context, DbSet<User> data) : base(context, data) { }

    public override async Task<Result<User>> GetAsync(Tree tree, Expression<Func<User, bool>> predicate)
    {
        return Result.Found(
            await Data
                .FirstOrDefaultAsync(predicate)
        ).WithSelector(Model.SelectView);
    }

    public override Task<Result<User>> GetAsync(Tree tree)
    {
        return tree.Users.IsAuto
            ? tree.User.IsId
                ? GetAsync(tree, user => user.Id == tree.User.Id!.Value)
                : GetAsync(tree, user => user.Name == tree.User.Name)
            : tree.Users.IsId
                ? GetAsync(tree, user => user.Id == tree.Users.Id!.Value)
                : GetAsync(tree, user => user.Name == tree.Users.Name);
    }

    public override async Task<Result<List<User>>> ListAsync(Tree tree)
    {
        return Result.Ok(
            await Data
                .OrderByDescending(u => u.Viewed)
                .ToListAsync()
        ).WithSelector(Model.SelectListView);
    }

    public override async Task<Result<User>> CreateAsync(Tree tree, UserData data)
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