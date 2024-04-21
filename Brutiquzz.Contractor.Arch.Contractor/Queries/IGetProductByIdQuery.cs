using Brutiquzz.Contractor.Arch.Contractor.Entities;
using MediatR;

namespace Brutiquzz.Contractor.Arch.Contractor.Queries;

public interface IGetProductByIdQuery : IRequest<IQueryable<IProduct>>
{
    IQueryable<IProduct> Send();
}
