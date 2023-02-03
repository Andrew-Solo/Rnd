using Microsoft.EntityFrameworkCore;
using Rnd.Constants;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Units : Repository<Unit>
{
    public Units(DataContext context, DbSet<Unit> data) : base(context, data) { }
}