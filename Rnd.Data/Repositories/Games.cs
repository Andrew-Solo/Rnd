using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Games : Repository<Game>
{
    public Games(DataContext context, DbSet<Game> data) : base(context, data) { }
    
    public async Task<Result<List<Game>>> ListAsync(Guid userId)
    {
        return Result.Success(
            await Data
                .Where(g => g.Members.Any(m => m.UserId == userId))
                .OrderByDescending(g => g.Members.First(m => m.UserId == userId).Selected)
                .ToListAsync(),
            "Ваши игры");
    }
    
    public async Task<Result<Game>> GetAsync(Guid userId, Guid? gameId = null)
    {
        var result = await ListAsync(userId);
        if (result.IsFailed) return Result.Fail<Game>(result.Message);

        return Result
            .Found(
                gameId == null
                    ? result.Value.FirstOrDefault()
                    : result.Value.FirstOrDefault(g => g.Id == gameId),
                "Игра",
                "Игра не найдена")
            .OnSuccess(g => g.GetView());
    }
    
    public async Task<Result<Game>> CreateAsync(Guid userId, Game.Form form)
    {
        var validation = await Data.ValidateAsync("Ошибка валидации",
            new Rule<Game>(g => form.Name != null && g.Name == form.Name, 
                "Игра с таким именем уже существет", 
                nameof(form.Name)));
        
        if (!validation.IsValid) return Result.Fail<Game>(validation.Message);
        
        //TODO Роли
        
        var result = await Game.New.TryCreateAsync(form);
        if (result.IsFailed) return result;
        
        Data.Add(result.Value);
        await Context.SaveChangesAsync();

        return result;
    }
    
    public async Task<Result<Game>> UpdateAsync(Guid userId, Guid? gameId, Game.Form form)
    {
        var validation = await Data.ValidateAsync("Ошибка валидации",
            new Rule<Game>(g => form.Name != null && g.Name == form.Name && g.Id != gameId, 
                "Игра с таким именем уже существет", 
                nameof(form.Name)));
        
        if (!validation.IsValid) return Result.Fail<Game>(validation.Message);
        
        var result = await GetAsync(userId, gameId);
        if (result.IsFailed) return result;

        //TODO Роли
        
        result.Update(await result.Value.TryUpdateAsync(form));
        if (result.IsFailed) return result;

        await Context.SaveChangesAsync();
        
        return result;
    }
    
    public async Task<Result<Game>> DeleteAsync(Guid userId, Guid? gameId)
    {
        var result = await GetAsync(userId, gameId);
        if (result.IsFailed) return result;

        //TODO Роли
        
        Data.Remove(result.Value);
        await Context.SaveChangesAsync();

        return result;
    }
    
    public async Task<Result<Game>> SelectAsync(Guid userId, Guid? gameId)
    {
        var result = await GetAsync(userId, gameId);
        if (result.IsFailed) return result;
        
        //TODO Роли
        
        await Context.SaveChangesAsync();

        return result;
    }
}