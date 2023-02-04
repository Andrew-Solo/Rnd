using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Units : Repository<Unit>
{
    public Units(DataContext context, DbSet<Unit> data) : base(context, data) { }

    public async Task<Result<List<Unit>>> ListAsync(Guid userId)
    {
        var member = await Context.Members.GetAsync(userId, null);
        if (member.IsFailed) return Result.Fail<List<Unit>>(member.Message);

        var character = await Context.Characters.GetAsync(member.Value.Id, null);
        if (character.IsFailed) return Result.Fail<List<Unit>>(character.Message);
        
        return Result.Success(
            await Data
                .Where(u => u.ModuleId == character.Value.ModuleId)
                .OrderBy(u => u.Fullname)
                .Include(u => u.Module)
                .Include(u => u.Parent)
                .Include(u => u.Children) //Recursive load
                .ToListAsync(),
            "Юниты персонажа");
    }

    public async Task<Result<Unit>> GetAsync(Guid userId, Guid? unitId)
    {
        var result = await ListAsync(userId);
        if (result.IsFailed) return Result.Fail<Unit>(result.Message);

        return Result
            .Found(
                result.Value.FirstOrDefault(u => u.Id == unitId),
                "Юнит",
                "Юнит не найден")
            .OnSuccess(g => g.GetView());
    }

    public async Task<Result<Unit>> SetAsync(Guid userId, Guid? unitId, string value)
    {
        var result = await GetAsync(userId, unitId);
        if (result.IsFailed) return result;

        var token = await Context.Tokens.SetAsync(userId, result.Value.Id, value);
        if (token.IsFailed) return Result.Fail<Unit>(token.Message);

        return result;
    }

    public async Task<Result<Unit>> ActAsync(Guid userId, Guid? unitId, string parameters)
    {
        var result = await GetAsync(userId, unitId);
        if (result.IsFailed) return result;
        
        result.Update(await result.Value.ActAsync(parameters));
        if (result.IsFailed) return result;
        
        return result;
    }
}