using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Members : Repository<Member>
{
    public Members(DataContext context, DbSet<Member> data) : base(context, data) { }
    
    public async Task<Result<List<Member>>> ListAsync(Guid userId, Guid? gameId = null)
    {
        var gameResult = await Context.Games.GetAsync(userId, gameId);
        if (gameResult.IsFailed) return Result.Fail<List<Member>>(gameResult.Message);

        return Result.Success(
            await Data
                .Where(m => m.UserId == userId && m.GameId == gameResult.Value.Id)
                .OrderByDescending(m => m.Selected)
                .ToListAsync(),
            "Участники игры");
    }
    
    public async Task<Result<Member>> GetAsync(Guid userId, Guid? gameId, Guid? memberId)
    {
        var result = await ListAsync(userId, gameId);
        if (result.IsFailed) return Result.Fail<Member>(result.Message);

        return Result
            .Found(
                memberId == null
                    ? result.Value.FirstOrDefault()
                    : result.Value.FirstOrDefault(m => m.Id == memberId),
                "Участник",
                "Участник не найден")
            .OnSuccess(m => m.GetView());
    }
    
    public async Task<Result<Member>> CreateAsync(Guid userId, Member.Form form)
    {
        var validation = await Data.ValidateAsync("Ошибка валидации",
            new Rule<Member>(m => form.UserId != null && form.GameId != null && 
                                  m.UserId == form.UserId && m.GameId == form.GameId, 
                "Пользователь уже является участником игры", 
                nameof(form.UserId)),
            new Rule<Member>(m => form.Nickname != null && form.GameId != null &&
                                  m.GameId == form.GameId && m.Nickname == form.Nickname, 
                "Участник с таким псевдонимом уже существует", 
                nameof(form.Nickname)));
        
        if (!validation.IsValid) return Result.Fail<Member>(validation.Message);
        
        //TODO Роли
        
        var result = await Member.New.TryCreateAsync(form);
        if (result.IsFailed) return result;
        
        Data.Add(result.Value);
        await Context.SaveChangesAsync();

        return result;
    }
    
    public async Task<Result<Member>> UpdateAsync(Guid userId, Guid? gameId, Guid? memberId, Member.Form form)
    {
        var validation = await Data.ValidateAsync("Ошибка валидации",
            new Rule<Member>(m => form.Nickname != null && form.GameId != null &&
                                  m.GameId == form.GameId && m.Nickname == form.Nickname, 
                "Участник с таким псевдонимом уже существует", 
                nameof(form.Nickname)));
        
        if (!validation.IsValid) return Result.Fail<Member>(validation.Message);
        
        var result = await GetAsync(userId, gameId, memberId);
        if (result.IsFailed) return result;

        //TODO Роли
        
        result.Update(await result.Value.TryUpdateAsync(form));
        if (result.IsFailed) return result;

        await Context.SaveChangesAsync();
        
        return result;
    }
    
    public async Task<Result<Member>> DeleteAsync(Guid userId, Guid? gameId, Guid? memberId)
    {
        var result = await GetAsync(userId, gameId, memberId);
        if (result.IsFailed) return result;

        //TODO Роли
        
        Data.Remove(result.Value);
        await Context.SaveChangesAsync();

        return result;
    }
}