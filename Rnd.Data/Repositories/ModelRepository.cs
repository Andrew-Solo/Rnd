using System.Dynamic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public abstract class ModelRepository<TModel, TData> where TModel : Model where TData : ModelData
{
    protected ModelRepository(DataContext context, DbSet<TModel> data)
    {
        Context = context;
        Data = data;
    }

    protected DataContext Context { get; }
    protected DbSet<TModel> Data { get; }

    public abstract Task<Result<TModel>> GetAsync(Guid userId, Expression<Func<TModel, bool>>? predicate = null);
    public Task<Result<TModel>> GetAsync(Guid userId, Guid id) => GetAsync(userId, model => model.Id == id);
    public Task<Result<TModel>> GetAsync(Guid userId, string name) => GetAsync(userId, model => model.Name == name);
    
    public abstract Task<Result<List<TModel>>> ListAsync(Guid userId);
    
    public abstract Task<Result<TModel>> CreateAsync(Guid userId, TData data);
    
    public abstract Task<Result<TModel>> UpdateAsync(TModel model, TData data);
    
    public async Task<Result<TModel>> UpdateAsync(Guid userId, TData data)
    {
        var model = await GetAsync(userId);
        if (model.Failed) return model;
        
        return await UpdateAsync(model.Value, data);
    }
    
    public async Task<Result<TModel>> UpdateAsync(Guid userId, Guid id, TData data)
    {
        var model = await GetAsync(userId, id);
        if (model.Failed) return model;
        
        return await UpdateAsync(model.Value, data);
    }
    
    public async Task<Result<TModel>> UpdateAsync(Guid userId, string name, TData data)
    {
        var model = await GetAsync(userId, name);
        if (model.Failed) return model;
        
        return await UpdateAsync(model.Value, data);
    }

    public abstract Task<Result<TModel>> DeleteAsync(TModel model);
    
    public async Task<Result<TModel>> DeleteAsync(Guid userId)
    {
        var model = await GetAsync(userId);
        if (model.Failed) return model;
        
        return await DeleteAsync(model.Value);
    }
    
    public async Task<Result<TModel>> DeleteAsync(Guid userId, Guid id)
    {
        var model = await GetAsync(userId, id);
        if (model.Failed) return model;
        
        return await DeleteAsync(model.Value);
    }
    
    public async Task<Result<TModel>> DeleteAsync(Guid userId, string name)
    {
        var model = await GetAsync(userId, name);
        if (model.Failed) return model;
        
        return await DeleteAsync(model.Value);
    }
}