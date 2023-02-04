using Microsoft.EntityFrameworkCore;
using Rnd.Models;

namespace Rnd.Data.Repositories;

public class Tokens : Repository<Token>
{
    public Tokens(DataContext context, DbSet<Token> data) : base(context, data) { }
}