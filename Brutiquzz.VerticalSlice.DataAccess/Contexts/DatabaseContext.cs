using Brutiquzz.VerticalSlice.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brutiquzz.VerticalSlice.DataAccess.Contexts;

public sealed class DatabaseContext : DbContext
{
    public DbSet<Product> Products { get; set; }
}
