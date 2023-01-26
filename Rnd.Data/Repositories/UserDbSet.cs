using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Result;

namespace Rnd.Data.Repositories;

public static class UserDbSet
{
    public static async Task<Result<User>> GetByIdAsync(this DbSet<User> dbSet, Guid id)
    {
        var user = await dbSet.FirstOrDefaultAsync(u => u.Id == id);
        
        return user != null 
            ? Result<User>.Ok(user) 
            : Result<User>.Error("Пользователь не найден");
    }
    
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

        if (form.Email != null && await dbSet.AnyAsync(u => u.Email == form.Email)) result.Message
            .AddProperty(nameof(form.Email), "Пользователь с таким Email уже существует");
        
        if (form.Login != null && await dbSet.AnyAsync(u => u.Login == form.Login)) result.Message
            .AddProperty(nameof(form.Login), "Пользователь с таким логином уже существует");
        
        if (form.DiscordId != null && await dbSet.AnyAsync(u => u.DiscordId == form.DiscordId)) result.Message
            .AddProperty(nameof(form.DiscordId), "К текущему аккаунту discordId уже привязан другой аккаунт RndId");

        if (result.IsFailed) return result;

        dbSet.Add(result.Value);

        return result;
    }
    
    public static async Task<EmptyResult> EditAsync(this DbSet<User> dbSet, Guid userId, User.Form form)
    {
        var userResult = await GetByIdAsync(dbSet, userId);
        if (userResult.IsFailed) return EmptyResult.Error(userResult.Message);

        var user = userResult.Value;
        
        var result = await user.TryUpdateAsync(form);

        if (form.Email != null && await dbSet.AnyAsync(u => u.Email == form.Email && u.Id != userId)) result.Message
            .AddProperty(nameof(form.Email), "Пользователь с таким Email уже существует");
        
        if (form.Login != null && await dbSet.AnyAsync(u => u.Login == form.Login && u.Id != userId)) result.Message
            .AddProperty(nameof(form.Login), "Пользователь с таким логином уже существует");
        
        if (form.DiscordId != null && await dbSet.AnyAsync(u => u.DiscordId == form.DiscordId && u.Id != userId)) result.Message
            .AddProperty(nameof(form.DiscordId), "К текущему аккаунту discordId уже привязан другой аккаунт RndId");

        return result;
    }
    
    public static async Task<EmptyResult> BindDiscordAsync(this DbSet<User> dbSet, User user, ulong discordId)
    {
        var exist = await dbSet.FirstOrDefaultAsync(u => u.DiscordId == discordId && u.Id != user.Id);
        
        if (exist != null) return EmptyResult.Error(new Message(
            "Аккаунт не привязан", 
            "К текущему аккаунту discordId уже привязан другой аккаунт RndId")
        );

        return await user.TryUpdateAsync(new User.Form(DiscordId: discordId));
    }
    
    public static async Task<EmptyResult> UnbindDiscordAsync(this DbSet<User> dbSet, Guid userId)
    {
        var result = await GetByIdAsync(dbSet, userId);
        if (result.IsFailed) return EmptyResult.Error(result.Message);
        
        return await result.Value.TryClearAsync(new User.Form(DiscordId: 0));
    }
}