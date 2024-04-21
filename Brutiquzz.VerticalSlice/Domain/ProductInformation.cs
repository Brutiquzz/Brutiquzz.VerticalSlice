using Brutiquzz.VerticalSlice.DataAccess.Entities;

namespace Brutiquzz.VerticalSlice.Domain;

public class ProductInformation
{
    public ProductInformation(Product product)
    {
        Id = product.ProductId;
        Name = product.Name;
        Description = product.Description;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
