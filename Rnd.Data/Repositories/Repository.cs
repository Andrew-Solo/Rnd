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

    public abstract Task<Result<TModel>> GetAsync(Request request, Expression<Func<TModel, bool>> predicate);

    public abstract Task<Result<TModel>> GetAsync(Request request);

    public abstract Task<Result<List<TModel>>> ListAsync(Request request);
    
    public abstract Task<Result<TModel>> CreateAsync(Request request, TData data);
    
    public abstract Task<Result<TModel>> UpdateAsync(TModel model, TData data);
    
    public async Task<Result<TModel>> UpdateAsync(Request request, TData data)
    {
        var model = await GetAsync(request);
        if (model.Failed) return model;
        
        return await UpdateAsync(model.Value, data);
    }

    public abstract Task<Result<TModel>> DeleteAsync(TModel model);
    
    public async Task<Result<TModel>> DeleteAsync(Request request)
    {
        var model = await GetAsync(request);
        if (model.Failed) return model;
        
        return await DeleteAsync(model.Value);
    }
}