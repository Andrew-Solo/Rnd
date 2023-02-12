using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;
using Rnd.Script.Compiler;

namespace Rnd.Data.Repositories;

public class Modules : Repository<Module>
{
    public Modules(DataContext context, DbSet<Module> data) : base(context, data) { }

    public async Task<Result<List<Module>>> ListAsync()
    {
        return Result.Success(
            await Data
                .OrderByDescending(m => m.Created)
                .Include(m => m.Units)
                .ToListAsync(),
            "Модули");
    }

    public async Task<Result<Module>> GetAsync(Guid id)
    {
        return await GetAsync(m => m.Id == id);
    }
    
    public async Task<Result<Module>> GetAsync(string name, string version)
    {
        return await GetAsync(m => m.Name == name && m.Version == version);
    }
    
    private async Task<Result<Module>> GetAsync(Expression<Func<Module, bool>> predicate)
    {
        return Result
            .Found(
                await Data
                    .Include(m => m.Units)
                    .FirstOrDefaultAsync(predicate),
                "Модуль",
                "Модуль не найден")
            .OnSuccess(u => u.GetView());
    }

    public async Task<Result<List<Module>>> UpdateAllAsync(string path)
    {
        await using var transaction = await Context.Database.BeginTransactionAsync();
        
        try
        {
            var directory = new DirectoryInfo(path);
            var files = directory.GetFiles();

            var modules = new List<Module>();

            foreach (var file in files.Where(f => f.Extension == ".rnd"))
            {
                var module = await UpdateAsync(file.FullName);
                
                if (module.IsFailed)
                {
                    await transaction.RollbackAsync();
                    return Result.Fail<List<Module>>(module.Message);
                }

                if (module.Value != null) modules.Add(module.Value);
            }

            await transaction.CommitAsync();
            return Result.Success(modules, "Модули обновлены");
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return Result.Fail<List<Module>>(new Message("Ошибка компиляции", e.Message));
        }
    }
    
    public async Task<Result<Module?>> UpdateAsync(string path)
    {
        var module = await Module.New.ParseAsync(path);
        if (module.IsFailed) return Result.Fail<Module?>(module.Message);

        var exist = await GetAsync(module.Value.Name, module.Value.Version);
        if (exist.IsSuccess) return Result.Success<Module?>(null, "Модуль уже обновлен");

        var result = module.Value.Compile();
        if (result.IsFailed) return  Result.Fail<Module?>(result.Message);
        
        Data.Add(result.Value);
        await Context.SaveChangesAsync();
        
        return Result.Success<Module?>(result.Value, "Модуль обновлен");
    }
}