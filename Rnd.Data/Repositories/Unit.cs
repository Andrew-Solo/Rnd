using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Units : Repository<Unit>
{
    public Units(DataContext context, DbSet<Unit> data) : base(context, data) { }

    public async Task<Result<List<Unit>>> ListAsync(Guid memberId, Guid? characterId)
    {
        var character = await Context.Characters.GetAsync(memberId, characterId);
        if (character.IsFailed) return Result.Fail<List<Unit>>(character.Message);
        
        return Result.Success(
            await Data
                .Where(u => u.ModuleId == character.Value.ModuleId)
                .OrderBy(u => u.Fullname)
                .Include(u => u.Module)
                .Include(u => u.Parent)
                .Include(u => u.Children) //Then many?
                .ToListAsync(),
            "Юниты персонажа");
    }

    public async Task<Result<Unit>> GetAsync(Guid memberId, Guid? characterId, Guid unitId)
    {
        var result = await ListAsync(memberId, characterId);
        if (result.IsFailed) return Result.Fail<Unit>(result.Message);

        return Result
            .Found(
                result.Value.FirstOrDefault(u => u.Id == unitId),
                "Юнит",
                "Юнит не найден")
            .OnSuccess(g => g.GetView());
    }
}