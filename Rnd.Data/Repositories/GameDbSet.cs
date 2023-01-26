using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Result;

namespace Rnd.Data.Repositories;

public static class GameDbSet
{
    public static async Task<Result<List<Game>>> ListByUserIdAsync(this DbSet<Game> dbSet, Guid userId)
    {
        var games = await dbSet
            .Where(g => g.Members.Any(m => m.UserId == userId))
            .OrderByDescending(g => g.Members.First(m => m.UserId == userId).Selected)
            .ToListAsync();
        
        return Result<List<Game>>.Ok(games);
    }
    
    public static async Task<Result<Game>> GetByIdAsync(this DbSet<Game> dbSet, Guid userId, Guid? gameId)
    {
        var result = await ListByUserIdAsync(dbSet, userId);
        if (!result.IsSuccess) return Result<Game>.Error(result.Message);

        if (gameId == null)
        {
            var game = result.Value.FirstOrDefault();
            return game == null 
                ? Result<Game>.Error("У вас нет игр") 
                : Result<Game>.Ok(game);
        }
        else
        {
            var game = result.Value.FirstOrDefault(g => g.Id == gameId);
            return game == null 
                ? Result<Game>.Error("Игра не найдена") 
                : Result<Game>.Ok(game);
        }
    }

    public static async Task<Result<Game>> SelectAsync(this DbSet<Game> dbSet, Guid userId, Guid? gameId)
    {
        var result = await GetByIdAsync(dbSet, userId, gameId);
        if (!result.IsSuccess) return result;
        
        var member = result.Value.Members.First(m => m.UserId == userId);
        member.Select();

        return result;
    }
    
    public static async Task<EmptyResult> UpdateAsync(this DbSet<Game> dbSet, Guid userId, Guid? gameId, Game.Form form)
    {
        var gameResult = await GetByIdAsync(dbSet, userId, gameId);
        if (gameResult.IsFailed) return EmptyResult.Error(gameResult.Message);

        var game = gameResult.Value;
        
        var result = await game.TryUpdateAsync(form);

        if (form.Name != null && await dbSet.AnyAsync(g => g.Name == form.Name && g.Id != gameId)) result.Message
            .AddProperty(nameof(form.Name), "Игра с таким именем уже существет");

        return result;
    }
    
    public static async Task<EmptyResult> DeleteAsync(this DbSet<Game> dbSet, Guid userId, Guid? gameId)
    {
        var gameResult = await GetByIdAsync(dbSet, userId, gameId);
        if (gameResult.IsFailed) return EmptyResult.Error(gameResult.Message);

        var game = gameResult.Value;

        dbSet.Remove(game);

        return EmptyResult.Empty();
    }
}