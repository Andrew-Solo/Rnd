using Microsoft.EntityFrameworkCore;

namespace RnDBot.Data;

public sealed class DataContext : DbContext
{
    public DataContext()
    {
        Database.EnsureCreated();
    }
}