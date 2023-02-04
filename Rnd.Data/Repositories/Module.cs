using Microsoft.EntityFrameworkCore;
using Rnd.Constants;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Modules : Repository<Module>
{
    public Modules(DataContext context, DbSet<Module> data) : base(context, data) { }

    public Task<Result<List<Module>>> ListAsync()
    {
        throw new NotImplementedException();
    }
}