using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models;
using Rnd.Results;

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