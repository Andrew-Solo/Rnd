using Microsoft.EntityFrameworkCore;
using Rnd.Constants;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Characters : Repository<Character>
{
    public Characters(DataContext context, DbSet<Character> data) : base(context, data) { }
}