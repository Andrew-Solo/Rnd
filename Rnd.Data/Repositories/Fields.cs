using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Fields : Repository<Field, FieldData>
{
    public Fields(DataContext context, DbSet<Field> data) : base(context, data) { }

    public override async Task<Result<Field>> GetAsync(Request request, Expression<Func<Field, bool>> predicate)
    {
        return Result.Found(
            await Data
                .FirstOrDefaultAsync(predicate)
        ).WithSelector(Model.SelectView);
    }

    public override Task<Result<Field>> GetAsync(Request request)
    {
        return request.Fields.IsId
            ? GetAsync(request, field => field.Id == request.Fields.Id!.Value)
            : GetAsync(request, field => field.Name == request.Fields.Value);
    }

    public override async Task<Result<List<Field>>> ListAsync(Request request)
    {
        var unit = await Context.Units.GetAsync(request);
        if (unit.Failed) return unit.Cast<List<Field>>();
        
        return Result.Ok(
            await Data
                .Where(field => field.UnitId == unit.Value.Id)
                .OrderBy(field => field.Order)
                .ToListAsync()
        ).WithSelector(Model.SelectListView);
    }

    public override async Task<Result<Field>> CreateAsync(Request request, FieldData data)
    {
        var fieldResult = Field.Create(data);
        if (fieldResult.Failed) return fieldResult;
        
        Data.Add(fieldResult.Value);
        await Context.SaveChangesAsync();
        
        return fieldResult;
    }

    public override async Task<Result<Field>> UpdateAsync(Field field, FieldData data)
    {
        var result = field.Update(data);
        if (result.Failed) return result.Cast<Field>();
        
        Data.Update(field);
        await Context.SaveChangesAsync();
        
        return result.Cast<Field>();
    }

    public override async Task<Result<Field>> DeleteAsync(Field field)
    {
        var result = field.Delete();
        if (result.Failed) return result.Cast<Field>();
        
        Data.Remove(field);
        await Context.SaveChangesAsync();
        
        return result.Cast<Field>();
    }
}