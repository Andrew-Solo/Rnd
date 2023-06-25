using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Units : Repository<Unit, UnitData>
{
    public Units(DataContext context, DbSet<Unit> data) : base(context, data) { }

    public override async Task<Result<Unit>> GetAsync(Request request, Expression<Func<Unit, bool>> predicate)
    {
        return Result.Found(
            await Data
                .FirstOrDefaultAsync(predicate)
        ).WithSelector(Model.SelectView);
    }

    public override Task<Result<Unit>> GetAsync(Request request)
    {
        return request.Units.IsId
            ? GetAsync(request, unit => unit.Id == request.Units.Id!.Value)
            : GetAsync(request, unit => unit.Name == request.Units.Value);
    }

    public override async Task<Result<List<Unit>>> ListAsync(Request request)
    {
        var module = await Context.Modules.GetAsync(request);
        if (module.Failed) return module.Cast<List<Unit>>();
        
        return Result.Ok(
            await Data
                .Where(unit => unit.ModuleId == module.Value.Id)
                .OrderBy(unit => unit.Order)
                .ToListAsync()
        ).WithSelector(Model.SelectListView);
    }

    public override async Task<Result<Unit>> CreateAsync(Request request, UnitData data)
    {
        var unitResult = Unit.Create(data);
        if (unitResult.Failed) return unitResult;
        
        Data.Add(unitResult.Value);
        await Context.SaveChangesAsync();
        
        return unitResult;
    }

    public override async Task<Result<Unit>> UpdateAsync(Unit unit, UnitData data)
    {
        var result = unit.Update(data);
        if (result.Failed) return result.Cast<Unit>();
        
        Data.Update(unit);
        await Context.SaveChangesAsync();
        
        return result.Cast<Unit>();
    }

    public override async Task<Result<Unit>> DeleteAsync(Unit unit)
    {
        var result = unit.Delete();
        if (result.Failed) return result.Cast<Unit>();
        
        Data.Remove(unit);
        await Context.SaveChangesAsync();
        
        return result.Cast<Unit>();
    }
}