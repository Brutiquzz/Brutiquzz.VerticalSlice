using Brutiquzz.Contractor.Arch.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brutiquzz.Contractor.Arch.DataAccess.Contexts;

internal sealed class DatabaseContext : DbContext
{
    public DbSet<Product> Products { get; set; }
}
