using Microsoft.EntityFrameworkCore;
using Rnd.Constants;
using Rnd.Models;
using Rnd.Result;

namespace Rnd.Data.Repositories;

public static class Generic
{
    public static async Task<Result<Game>> CreateGameAsync(this DataContext db, Guid userId, Game.Form form)
    {
        var result = await Game.New.TryCreateAsync(form);
        
        if (form.Name != null && await db.Games.AnyAsync(g => g.Name == form.Name)) result.Message
            .AddProperty(nameof(form.Name), "Игра с таким именем уже существет");
        
        if (result.IsFailed) return result;

        var game = result.Value;

        var userResult = await db.Users.GetByIdAsync(userId);
        if (userResult.IsFailed) return Result<Game>.Error(userResult.Message);

        var user = userResult.Value;
        
        //TODO номральный креэйт
        var memberResult = await Member.New.TryCreateAsync(new Member.Form(game.Id, userId, MemberRole.Owner, user.Login));
        if (memberResult.IsFailed) return Result<Game>.Error(memberResult.Message);
        
        db.Games.Add(result.Value);
        game.Members.Add(memberResult.Value);

        return result;
    }
}