using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Tokens : Repository<Token>
{
    public Tokens(DataContext context, DbSet<Token> data) : base(context, data) { }

    public async Task<Result<Token>> GetAsync(Guid userId, Guid unitId)
    {
        var member = await Context.Members.GetAsync(userId, null);
        if (member.IsFailed) return Result.Fail<Token>(member.Message);

        var character = await Context.Characters.GetAsync(member.Value.Id, null);
        if (character.IsFailed) return Result.Fail<Token>(character.Message);

        return await GetAsync(t => t.UnitId == unitId && t.CharacterId == character.Value.Id);
    }
    
    public async Task<Result<Token>> GetAsync(Expression<Func<Token, bool>> predicate)
    {
        return Result
            .Found(
                await Data
                    .Include(m => m.Character)
                    .Include(m => m.Unit)
                    .FirstOrDefaultAsync(predicate),
                "Токен",
                "Токен не найден")
            .OnSuccess(m => m.GetView());
    }

    public async Task<Result<Token>> SetAsync(Guid userId, Guid unitId, string value)
    {
        var result = await GetAsync(userId, unitId);
        if (result.IsFailed) return result;

        result.Update(await result.Value.TryUpdateAsync(new Token.Form {Value = value}));
        return result;
    }
}