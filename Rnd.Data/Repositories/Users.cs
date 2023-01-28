using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Users : Repository<User>
{
    public Users(DataContext context, DbSet<User> data) : base(context, data) { }
    
    public async Task<Result<User>> GetAsync(Guid id)
    {
        return Result
            .Found(
                await Data.FirstOrDefaultAsync(u => u.Id == id),
            "Пользователь",
            "Пользователь не найден")
            .OnSuccess(u => u.GetView());
    }
    
    public async Task<Result<User>> GetByDiscordAsync(ulong id)
    {
        return Result
            .Found(
                await Data.FirstOrDefaultAsync(u => u.DiscordId == id),
                "Пользователь",
                "Пользователь не найден")
            .OnSuccess(u => u.GetView());
    }
    
    public async Task<Result<User>> LoginAsync(string login, string password)
    {
        var user = await Data.FirstOrDefaultAsync(u => u.PasswordHash == Hash.GenerateStringHash(password)
                                                        && (u.Login == login || u.Email == login));
        
        return Result
            .Found(user,
                "Пользователь",
                "Пользователь не найден")
            .OnSuccess(u => u.GetView());
    }
    
    public async Task<Result<User>> CreateAsync(User.Form form)
    {
        var validation = await Data.ValidateAsync("Ошибка валидации",
            new Rule<User>(u => form.Email != null && u.Email == form.Email, 
                "Пользователь с таким Email уже существует", 
                nameof(form.Email)),
            new Rule<User>(u => form.Login != null && u.Login == form.Login, 
                "Пользователь с таким логином уже существует", 
                nameof(form.Login)),
            new Rule<User>(u => form.DiscordId != null && u.DiscordId == form.DiscordId, 
                "К текущему аккаунту discordId уже привязан другой аккаунт RndId", 
                nameof(form.DiscordId)));

        if (!validation.IsValid) return Result.Fail<User>(validation.Message);
        
        var result = await User.New.TryCreateAsync(form);
        if (result.IsFailed) return result;
        
        Data.Add(result.Value);
        await Context.SaveChangesAsync();

        return result.OnSuccess(u => u.GetView());
    }
    
    public async Task<Result<User>> UpdateAsync(Guid userId, User.Form form)
    {
        var validation = await Data.ValidateAsync("Ошибка валидации",
            new Rule<User>(u => form.Email != null && u.Email == form.Email && u.Id != userId, 
                "Пользователь с таким Email уже существует", 
                nameof(form.Email)),
            new Rule<User>(u => form.Login != null && u.Login == form.Login && u.Id != userId, 
                "Пользователь с таким логином уже существует", 
                nameof(form.Login)),
            new Rule<User>(u => form.DiscordId != null && u.DiscordId == form.DiscordId && u.Id != userId, 
                "К текущему аккаунту discordId уже привязан другой аккаунт RndId", 
                nameof(form.DiscordId)));
        
        if (!validation.IsValid) return Result.Fail<User>(validation.Message);
        
        var result = await GetAsync(userId);
        if (result.IsFailed) return result;

        result.Update(await result.Value.TryUpdateAsync(form));
        if (result.IsFailed) return result;

        await Context.SaveChangesAsync();
        
        return result;
    }
    
    public async Task<Result<User>> BindDiscordAsync(User user, ulong discordId)
    {
        var validation = await Data.ValidateAsync("Аккаунт не привязан",
            new Rule<User>(u => u.DiscordId == discordId  && u.Id != user.Id, 
                "К текущему аккаунту discordId уже привязан другой аккаунт RndId", 
                nameof(user.DiscordId)));

        if (!validation.IsValid) return Result.Fail<User>(validation.Message);

        var result = await user.TryUpdateAsync(new User.Form(DiscordId: discordId));
        if (result.IsFailed) return result;

        await Context.SaveChangesAsync();
            
        return result;
    }
    
    public async Task<Result<User>> UnbindDiscordAsync(Guid userId)
    {
        var result = await GetAsync(userId);
        if (result.IsFailed) return result;

        var form = result.Value.GetForm();
        form.DiscordId = null;
        
        result.Update(await result.Value.TryClearAsync(form));
        if (result.IsFailed) return result;

        await Context.SaveChangesAsync();
        
        return result;
    }
}