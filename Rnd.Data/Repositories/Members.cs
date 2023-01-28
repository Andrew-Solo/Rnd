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
        var gameResult = await Context.Games.GetAsync(userId, gameId);
        if (gameResult.IsFailed) return Result.Fail<List<Member>>(gameResult.Message);

        return Result.Success(
            await Data
                .Where(m => m.UserId == userId && m.GameId == gameResult.Value.Id)
                .OrderByDescending(m => m.Selected)
                .ToListAsync(),
            "Участники игры");
    }
    
    public async Task<Result<Member>> GetAsync(Guid id)
    {
        return Result
            .Found(
                await Data.FirstOrDefaultAsync(u => u.Id == id),
                "Участник",
                "Участник не найден")
            .OnSuccess(u => u.GetView());
    }
    
    public async Task<Result<Member>> GetAsync(Guid userId, Guid? gameId, Guid? memberId = null)
    {
        if (memberId != null) return await GetAsync(memberId.Value);
        
        var result = await ListAsync(userId, gameId);
        if (result.IsFailed) return Result.Fail<Member>(result.Message);

        return Result
            .Found(
                result.Value.FirstOrDefault(),
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
                nameof(form.Nickname)),
            new Rule<Member>(m => form.Role == MemberRole.Owner && form.GameId != null &&
                                  m.GameId == form.GameId && m.Role == MemberRole.Owner, 
                "Может существовать только один владелец игры", 
                nameof(form.Nickname)));
        
        if (!validation.IsValid) return Result.Fail<Member>(validation.Message);

        if (form.Role is not null and not MemberRole.Owner)
        {
            var isSuperior = await IsSuperior(userId, form.GameId, form.Role.Value);
            if (isSuperior.IsFailed) return isSuperior;
        }

        var result = await Member.New.TryCreateAsync(form);
        if (result.IsFailed) return result;
        
        Data.Add(result.Value);
        await Context.SaveChangesAsync();

        return result.OnSuccess(u => u.GetView());
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

        if (form.Role != null)
        {
            var isSuperior = await IsSuperior(userId, gameId, form.Role.Value);
            if (isSuperior.IsFailed) return isSuperior;
        }
        
        result.Update(await result.Value.TryUpdateAsync(form));
        if (result.IsFailed) return result;

        await Context.SaveChangesAsync();
        
        return result;
    }
    
    public async Task<Result<Member>> DeleteAsync(Guid userId, Guid? gameId, Guid? memberId)
    {
        var result = await GetAsync(userId, gameId, memberId);
        if (result.IsFailed) return result;

        var isInRole = await IsInRole(userId, gameId, MemberRole.Admin);
        if (isInRole.IsFailed) return isInRole;
        
        Data.Remove(result.Value);
        await Context.SaveChangesAsync();

        return result;
    }

    public async Task<Result<Member>> IsInRole(Guid userId, Guid? gameId, MemberRole role)
    {
        var result = await GetAsync(userId, gameId);
        if (result.IsFailed || !IsInRole(result.Value.Role, role)) return Result.Fail<Member>("Недостаточно прав");
        return result;
    }

    public bool IsInRole(MemberRole executor, MemberRole target)
    {
        return (int) executor >= (int) target;
    }
    
    public async Task<Result<Member>> IsSuperior(Guid userId, Guid? gameId, MemberRole role)
    {
        var result = await GetAsync(userId, gameId);
        if (result.IsFailed || !IsSuperior(result.Value.Role, role)) return Result.Fail<Member>("Недостаточно прав");
        return result;
    }
    
    public bool IsSuperior(MemberRole executor, MemberRole target)
    {
        return (int) executor > (int) target;
    }
    
    public async Task<Result<Member>> SelectAsync(Guid userId, Guid? gameId)
    {
        var result = await GetAsync(userId, gameId);
        if (result.IsFailed) return result;
        
        result.Value.Select();
        await Context.SaveChangesAsync();
        
        return result;
    }
}