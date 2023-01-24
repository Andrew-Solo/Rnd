using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Result;

namespace Rnd.Data.Repositories;

public static class UserDbSet
{
    public static async Task<Result<User>> LoginAsync(this DbSet<User> dbSet, string login, string password)
    {
        var user = await dbSet.FirstOrDefaultAsync(u => u.PasswordHash == Hash.GenerateStringHash(password)
                                                        && (u.Login == login || u.Email == login));
        
        return user != null 
            ? Result<User>.Ok(user) 
            : Result<User>.Error("Пользователь не найден");
    }
    
    public static async Task<Result<User>> RegisterAsync(this DbSet<User> dbSet, User.Form form)
    {
        var result = await User.New.TryCreateAsync(form);

        if (form.Email != null && await dbSet.AnyAsync(x => x.Email == form.Email)) result.Message
            .AddProperty(nameof(form.Email), "Пользователь с таким Email уже существует");
        
        if (form.Login != null && await dbSet.AnyAsync(x => x.Login == form.Login)) result.Message
            .AddProperty(nameof(form.Login), "Пользователь с таким логином уже существует");
        
        if (form.DiscordId != null && await dbSet.AnyAsync(x => x.DiscordId == form.DiscordId)) result.Message
            .AddProperty(nameof(form.DiscordId), "К текущему аккаунту discordId уже привязан другой аккаунт RndId");

        if (result.IsFailed) return result;

        dbSet.Add(result.Value);

        return result;
    }
    
    public static async Task<EmptyResult> EditAsync(this DbSet<User> dbSet, Guid userId, User.Form form)
    {
        var user = await dbSet.FirstOrDefaultAsync(x => x.Id == userId);

        if (user == null) return EmptyResult.Error("Пользователь не найден");
        
        var result = await user.TryUpdateAsync(form);

        if (form.Email != null && await dbSet.AnyAsync(x => x.Email == form.Email && x.Id != userId)) result.Message
            .AddProperty(nameof(form.Email), "Пользователь с таким Email уже существует");
        
        if (form.Login != null && await dbSet.AnyAsync(x => x.Login == form.Login && x.Id != userId)) result.Message
            .AddProperty(nameof(form.Login), "Пользователь с таким логином уже существует");
        
        if (form.DiscordId != null && await dbSet.AnyAsync(x => x.DiscordId == form.DiscordId && x.Id != userId)) result.Message
            .AddProperty(nameof(form.DiscordId), "К текущему аккаунту discordId уже привязан другой аккаунт RndId");

        return result;
    }
    
    public static async Task<EmptyResult> BindDiscord(this DbSet<User> dbSet, User user, ulong discordId)
    {
        var exist = await dbSet.FirstOrDefaultAsync(u => u.DiscordId == discordId && u.Id != user.Id);
        
        if (exist != null) return EmptyResult.Error(new Message(
            "Аккаунт не привязан", 
            "К текущему аккаунту discordId уже привязан другой аккаунт RndId")
        );

        return await user.TryUpdateAsync(new User.Form(DiscordId: discordId));
    }
    
    public static async Task<EmptyResult> UnbindDiscord(this DbSet<User> dbSet, Guid userId)
    {
        var user = await dbSet.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return EmptyResult.Error("Пользователь не найден");
        
        return await user.TryClearAsync(new User.Form(DiscordId: 0));
    }
}