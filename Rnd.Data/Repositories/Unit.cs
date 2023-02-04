using Microsoft.EntityFrameworkCore;
using Rnd.Models;

namespace Rnd.Data.Repositories;

public class Units : Repository<Unit>
{
    public Units(DataContext context, DbSet<Unit> data) : base(context, data) { }
}