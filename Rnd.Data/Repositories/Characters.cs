using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Characters : Repository<Character>
{
    public Characters(DataContext context, DbSet<Character> data) : base(context, data) { }

    public async Task<Result<List<Character>>> ListAsync(Guid memberId)
    {
        return Result.Success(
            await Data
                .Where(с => с.Owner.Id == memberId)
                .OrderByDescending(c => c.Selected)
                .Include(c => c.Owner)
                .Include(c => c.Module)
                .Include(c => c.Tokens)
                .ToListAsync(),
            "Ваши персонажи");
    }

    public async Task<Result<Character>> GetAsync(Guid memberId, Guid? characterId)
    {
        var result = await ListAsync(memberId);
        if (result.IsFailed) return Result.Fail<Character>(result.Message);

        return Result
            .Found(
                characterId == null
                    ? result.Value.FirstOrDefault()
                    : result.Value.FirstOrDefault(c => c.Id == characterId),
                "Персонаж",
                "Персонаж не найден")
            .OnSuccess(с => с.GetView());
    }

    public async Task<Result<Character>> CreateAsync(Guid memberId, Character.Form form)
    {
        //TODO выполнение от имени другого участника
        var member = await Context.Members.GetAsync(memberId);
        if (member.IsFailed) return Result.Fail<Character>(member.Message);
        
        var validation = await Data.ValidateAsync("Ошибка валидации",
            new Rule<Character>(c => form.Title != null && c.Owner.GameId == member.Value.GameId && c.Title == form.Title, 
                "Персонаж с таким именем уже существует в этой игре", 
                nameof(form.Title)));
        
        if (!validation.IsValid) return Result.Fail<Character>(validation.Message);

        var result = await Character.New.TryCreateAsync(form);
        if (result.IsFailed) return result;
        
        Data.Add(result.Value);
        await Context.SaveChangesAsync();

        return result.OnSuccess(u => u.GetView());
    }

    public async Task<Result<Character>> UpdateAsync(Guid memberId, Guid? characterId, Character.Form form)
    {
        //TODO выполнение от имени другого участника
        var member = await Context.Members.GetAsync(memberId);
        if (member.IsFailed) return Result.Fail<Character>(member.Message);
        
        var result = await GetAsync(memberId, characterId);
        if (result.IsFailed) return result;
        
        var validation = await Data.ValidateAsync("Ошибка валидации",
            new Rule<Character>(c => form.Title != null && c.Id != result.Value.Id && 
                                     c.Owner.GameId == member.Value.GameId && c.Title == form.Title, 
                "Персонаж с таким именем уже существует в этой игре", 
                nameof(form.Title)));
        
        if (!validation.IsValid) return Result.Fail<Character>(validation.Message);
        
        result.Update(await result.Value.TryUpdateAsync(form));
        if (result.IsFailed) return result;

        await Context.SaveChangesAsync();
        
        return result;
    }

    public async Task<Result<Character>> DeleteAsync(Guid memberId, Guid? characterId)
    {
        var result = await GetAsync(memberId, characterId);
        if (result.IsFailed) return result;
        
        Data.Remove(result.Value);
        await Context.SaveChangesAsync();

        return result;
    }

    public async Task<Result<Character>> SelectAsync(Guid memberId, Guid? characterId)
    {
        var result = await GetAsync(memberId, characterId);
        if (result.IsFailed) return result;

        result.Value.Select();
        await Context.SaveChangesAsync();

        return result;
    }
}