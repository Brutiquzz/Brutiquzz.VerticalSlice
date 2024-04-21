namespace Brutiquzz.Contractor.Arch.Contractor.Entities;

public interface IProduct
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
