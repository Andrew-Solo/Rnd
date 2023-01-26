using Microsoft.EntityFrameworkCore;
using Rnd.Constants;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Games : Repository<Game>
{
    public Games(DataContext context, DbSet<Game> data) : base(context, data) { }
    
    public async Task<Result<List<Game>>> ListAsync(Guid userId)
    {
        var games = await Data
            .Where(g => g.Members.Any(m => m.UserId == userId))
            .OrderByDescending(g => g.Members.First(m => m.UserId == userId).Selected)
            .ToListAsync();
        
        return Result<List<Game>>.Ok(games);
    }
    
    public async Task<Result<Game>> GetAsync(Guid userId, Guid? gameId)
    {
        var result = await ListAsync(userId);
        if (!result.IsSuccess) return Result<Game>.Fail(result.Message);

        if (gameId == null)
        {
            var game = result.Value.FirstOrDefault();
            return game == null 
                ? Result<Game>.Fail("У вас нет игр") 
                : Result<Game>.Ok(game);
        }
        else
        {
            var game = result.Value.FirstOrDefault(g => g.Id == gameId);
            return game == null 
                ? Result<Game>.Fail("Игра не найдена") 
                : Result<Game>.Ok(game);
        }
    }
    
    //TODO возвращать саму модеь
    public async Task<Result<Game>> CreateAsync(Guid userId, Game.Form form)
    {
        var result = await Game.New.TryCreateAsync(form);
        
        if (form.Name != null && await Data.AnyAsync(g => g.Name == form.Name)) result.Message
            .AddTooltips(nameof(form.Name), "Игра с таким именем уже существет");
        
        if (result.IsFailed) return result;

        var game = result.Value;

        var userResult = await Context.Users.GetAsync(userId);
        if (userResult.IsFailed) return Result<Game>.Fail(userResult.Message);

        var user = userResult.Value;
        
        //TODO номральный креэйт
        var memberResult = await Member.New.TryCreateAsync(new Member.Form(game.Id, userId, MemberRole.Owner, user.Login));
        if (memberResult.IsFailed) return Result<Game>.Fail(memberResult.Message);
        
        game.Members.Add(memberResult.Value);
        
        Data.Add(result.Value);
        await Context.SaveChangesAsync();

        return result;
    }
    
    //TODO возвращать саму модеь
    public async Task<EmptyResult> UpdateAsync(Guid userId, Guid? gameId, Game.Form form)
    {
        var gameResult = await GetAsync(userId, gameId);
        if (gameResult.IsFailed) return EmptyResult.Error(gameResult.Message);

        var game = gameResult.Value;
        
        //TODO это надо делать после проверок БД
        var result = await game.TryUpdateAsync(form);

        //TODO должно делать результ неудачным
        if (form.Name != null && await Data.AnyAsync(g => g.Name == form.Name && g.Id != gameId)) result.Message
            .AddTooltips(nameof(form.Name), "Игра с таким именем уже существет");

        //TODO
        await Context.SaveChangesAsync();
        
        return result;
    }
    
    public async Task<EmptyResult> DeleteAsync(Guid userId, Guid? gameId)
    {
        var gameResult = await GetAsync(userId, gameId);
        if (gameResult.IsFailed) return EmptyResult.Error(gameResult.Message);

        var game = gameResult.Value;

        Data.Remove(game);
        await Context.SaveChangesAsync();

        return EmptyResult.Empty();
    }
    
    public async Task<Result<Game>> SelectAsync(Guid userId, Guid? gameId)
    {
        var result = await GetAsync(userId, gameId);
        if (!result.IsSuccess) return result;
        
        var member = result.Value.Members.First(m => m.UserId == userId);
        member.Select();
        
        await Context.SaveChangesAsync();

        return result;
    }
}