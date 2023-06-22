using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Models.Nodes;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Modules : Repository<Module, ModuleData>
{
    public Modules(DataContext context, DbSet<Module> data) : base(context, data) { }

    public override async Task<Result<Module>> GetAsync(Request request, Expression<Func<Module, bool>> predicate)
    {
        return Result.Found(
            await Data
                .FirstOrDefaultAsync(predicate)
        ).WithSelector(Model.SelectView);
    }

    public override Task<Result<Module>> GetAsync(Request request)
    {
        return request.Modules.IsId
            ? GetAsync(request, module => module.Id == request.Modules.Id!.Value)
            : GetAsync(request, module => module.Name == request.Modules.Value);
    }

    public override async Task<Result<List<Module>>> ListAsync(Request request)
    {
        return Result.Ok(
            await Data
                .OrderBy(module => module.Order)
                .ToListAsync()
        ).WithSelector(Model.SelectListView);
    }

    public override async Task<Result<Module>> CreateAsync(Request request, ModuleData data)
    {
        var moduleResult = Module.Create(data);
        if (moduleResult.Failed) return moduleResult;
        
        Data.Add(moduleResult.Value);
        await Context.SaveChangesAsync();
        
        return moduleResult;
    }

    public override async Task<Result<Module>> UpdateAsync(Module module, ModuleData data)
    {
        var result = module.Update(data);
        if (result.Failed) return result.Cast<Module>();
        
        Data.Update(module);
        await Context.SaveChangesAsync();
        
        return result.Cast<Module>();
    }

    public override async Task<Result<Module>> DeleteAsync(Module module)
    {
        var result = module.Delete();
        if (result.Failed) return result.Cast<Module>();
        
        Data.Remove(module);
        await Context.SaveChangesAsync();
        
        return result.Cast<Module>();
    }
}