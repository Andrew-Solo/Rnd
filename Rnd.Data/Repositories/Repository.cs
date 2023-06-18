using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public abstract class Repository<TModel, TData> where TModel : Model where TData : ModelData
{
    protected Repository(DataContext context, DbSet<TModel> data)
    {
        Context = context;
        Data = data;
    }

    protected DataContext Context { get; }
    protected DbSet<TModel> Data { get; }

    public abstract Task<Result<TModel>> GetAsync(Tree tree, Expression<Func<TModel, bool>> predicate);

    public abstract Task<Result<TModel>> GetAsync(Tree tree);

    public abstract Task<Result<List<TModel>>> ListAsync(Tree tree);
    
    public abstract Task<Result<TModel>> CreateAsync(Tree tree, TData data);
    
    public abstract Task<Result<TModel>> UpdateAsync(TModel model, TData data);
    
    public async Task<Result<TModel>> UpdateAsync(Tree tree, TData data)
    {
        var model = await GetAsync(tree);
        if (model.Failed) return model;
        
        return await UpdateAsync(model.Value, data);
    }

    public abstract Task<Result<TModel>> DeleteAsync(TModel model);
    
    public async Task<Result<TModel>> DeleteAsync(Tree tree)
    {
        var model = await GetAsync(tree);
        if (model.Failed) return model;
        
        return await DeleteAsync(model.Value);
    }
}