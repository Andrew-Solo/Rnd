using Microsoft.EntityFrameworkCore;
using Rnd.Constants;
using Rnd.Models;
using Rnd.Results;

namespace Rnd.Data.Repositories;

public class Tokens : Repository<Token>
{
    public Tokens(DataContext context, DbSet<Token> data) : base(context, data) { }
}