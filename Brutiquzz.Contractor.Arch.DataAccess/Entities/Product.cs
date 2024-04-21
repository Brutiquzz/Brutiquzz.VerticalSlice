

using Brutiquzz.Contractor.Arch.Contractor.Entities;

namespace Brutiquzz.Contractor.Arch.DataAccess.Entities;

internal sealed class Product : IProduct
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
