
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rnd.Models.Nodes;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Modules : ModelRepository<Module, ModuleData>
{
    public Modules(DataContext context, DbSet<Module> data) : base(context, data) { }

    public override Task<Result<Module>> GetAsync(Guid userId, Expression<Func<Module, bool>>? predicate = null)
    {
        throw new NotImplementedException();
    }

    public override Task<Result<List<Module>>> ListAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public override Task<Result<Module>> CreateAsync(Guid userId, ModuleData data)
    {
        throw new NotImplementedException();
    }

    public override Task<Result<Module>> UpdateAsync(Module model, ModuleData data)
    {
        throw new NotImplementedException();
    }

    public override Task<Result<Module>> DeleteAsync(Module model)
    {
        throw new NotImplementedException();
    }
}