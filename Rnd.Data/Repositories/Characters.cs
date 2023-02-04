using Microsoft.EntityFrameworkCore;
using Rnd.Constants;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Characters : Repository<Character>
{
    public Characters(DataContext context, DbSet<Character> data) : base(context, data) { }

    public Task<Result<List<Character>>> ListAsync(Guid memberId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Character>> GetAsync(Guid memberId, Guid? characterId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Character>> CreateAsync(Guid memberId, Character.Form form)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Character>> UpdateAsync(Guid memberId, Guid? characterId, Character.Form form)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Character>> DeleteAsync(Guid memberId, Guid? characterId)
    {
        throw new NotImplementedException();
    }
}