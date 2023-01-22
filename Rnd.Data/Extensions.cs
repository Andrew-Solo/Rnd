using Microsoft.EntityFrameworkCore;
using Rnd.Core;
using Rnd.Result;

namespace Rnd.Data;

public static class Extensions
{
    public static async Task<TModel> GetByIdAsync<TModel>(this DbSet<TModel> dbSet, Guid id) where TModel : Model
    {
        return await dbSet.FirstAsync(m => m.Id == id);
    }
    
    public static async Task<Result<TModel>> TryGetByIdAsync<TModel>(this DbSet<TModel> dbSet, Guid id) where TModel : Model
    {
        var model = await dbSet.FirstOrDefaultAsync(m => m.Id == id);

        return model == null
            ? Result<TModel>.NotFound($"Обьект {typeof(TModel).Name} не найден")
            : Result<TModel>.Ok(model);
    }
}