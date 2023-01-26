using Microsoft.EntityFrameworkCore;
using Rnd.Constants;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Members : Repository<Member>
{
    public Members(DataContext context, DbSet<Member> data) : base(context, data) { }
    
    public async Task<Result<List<Member>>> ListAsync(Guid userId, Guid? gameId = null)
    {
        var result = await Context.Games.GetAsync(userId, gameId);
        if (result.IsFailed) return Result<List<Member>>.Fail(result.Message);
        
        var members = await Data
            .Where(m => m.UserId == userId && m.GameId == gameId)
            .OrderByDescending(m => m.Selected)
            .ToListAsync();
        
        return Result<List<Member>>.Ok(members);
    }
}