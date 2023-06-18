using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Models.Nodes;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Fields : Repository<Field, FieldData>
{
    public Fields(DataContext context, DbSet<Field> data) : base(context, data) { }

    public override async Task<Result<Field>> GetAsync(Tree tree, Expression<Func<Field, bool>> predicate)
    {
        return Result.Found(
            await Data
                .FirstOrDefaultAsync(predicate)
        ).WithSelector(Model.SelectView);
    }

    public override Task<Result<Field>> GetAsync(Tree tree)
    {
        return tree.Fields.IsId
            ? GetAsync(tree, field => field.Id == tree.Fields.Id!.Value)
            : GetAsync(tree, field => field.Name == tree.Fields.Name);
    }

    public override async Task<Result<List<Field>>> ListAsync(Tree tree)
    {
        return Result.Ok(
            await Data
                .OrderByDescending(field => field.Viewed)
                .ToListAsync()
        ).WithSelector(Model.SelectListView);
    }

    public override async Task<Result<Field>> CreateAsync(Tree tree, FieldData data)
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