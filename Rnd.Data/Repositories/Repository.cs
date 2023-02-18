using Microsoft.EntityFrameworkCore;
using Rnd.Core;

namespace Rnd.Data.Repositories;

public abstract class Repository<TModel> where TModel : Model
{
    protected Repository(DataContext context, DbSet<TModel> data)
    {
        Context = context;
        Data = data;
    }
    
    protected DataContext Context { get; }
    protected DbSet<TModel> Data { get; }
}