using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Models.Nodes;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Units : Repository<Unit, UnitData>
{
    public Units(DataContext context, DbSet<Unit> data) : base(context, data) { }

    public override async Task<Result<Unit>> GetAsync(Tree tree, Expression<Func<Unit, bool>> predicate)
    {
        return Result.Found(
            await Data
                .FirstOrDefaultAsync(predicate)
        ).WithSelector(Model.SelectView);
    }

    public override Task<Result<Unit>> GetAsync(Tree tree)
    {
        return tree.Units.IsId
            ? GetAsync(tree, unit => unit.Id == tree.Units.Id!.Value)
            : GetAsync(tree, unit => unit.Name == tree.Units.Name);
    }

    public override async Task<Result<List<Unit>>> ListAsync(Tree tree)
    {
        return Result.Ok(
            await Data
                .OrderByDescending(unit => unit.Viewed)
                .ToListAsync()
        ).WithSelector(Model.SelectListView);
    }

    public override async Task<Result<Unit>> CreateAsync(Tree tree, UnitData data)
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