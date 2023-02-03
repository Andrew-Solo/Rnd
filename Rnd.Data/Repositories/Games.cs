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
        return Result.Success(
            await Data
                .Where(g => g.Members.Any(m => m.UserId == userId))
                .OrderByDescending(g => g.Members.First(m => m.UserId == userId).Selected)
                .Include(g => g.Module)
                .Include(g => g.Members)
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

        var result = await Game.New.TryCreateAsync(form);
        if (result.IsFailed) return result;

        await using var transaction = await Context.Database.BeginTransactionAsync();
        
        Data.Add(result.Value);
        await Context.SaveChangesAsync();

        var userResult = await Context.Users.GetAsync(userId);
        if (userResult.IsFailed) return result;

        var memberForm = new Member.Form
        {
            UserId = userId,
            GameId = result.Value.Id,
            Nickname = userResult.Value.Login,
            Role = MemberRole.Owner
        };

        var memberResult = await Context.Members.CreateAsync(userId, memberForm);
        if (memberResult.IsFailed)
        {
            await transaction.RollbackAsync();
            return result;
        }

        await transaction.CommitAsync();

        return result.OnSuccess(u => u.GetView());
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

        var memberResult = await Context.Members.IsInRole(userId, result.Value.Id, MemberRole.Admin);
        if (memberResult.IsFailed) return Result.Fail<Game>(memberResult.Message);
        
        result.Update(await result.Value.TryUpdateAsync(form));
        if (result.IsFailed) return result;

        await Context.SaveChangesAsync();
        
        return result;
    }
    
    public async Task<Result<Game>> DeleteAsync(Guid userId, Guid? gameId)
    {
        var result = await GetAsync(userId, gameId);
        if (result.IsFailed) return result;

        var memberResult = await Context.Members.IsInRole(userId, result.Value.Id, MemberRole.Owner);
        if (memberResult.IsFailed) return Result.Fail<Game>(memberResult.Message);
        
        Data.Remove(result.Value);
        await Context.SaveChangesAsync();

        return result;
    }
    
    public async Task<Result<Game>> SelectAsync(Guid userId, Guid? gameId)
    {
        var result = await GetAsync(userId, gameId);
        if (result.IsFailed) return result;

        var memberResult = await Context.Members.SelectAsync(userId, result.Value.Id);

        return memberResult.IsFailed 
            ? Result.Fail<Game>(memberResult.Message) 
            : result;
    }
}