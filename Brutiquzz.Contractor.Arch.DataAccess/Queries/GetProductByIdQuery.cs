using Brutiquzz.Contractor.Arch.Contractor.Entities;
using Brutiquzz.Contractor.Arch.Contractor.Queries;
using Brutiquzz.Contractor.Arch.DataAccess.Contexts;
using MediatR;

namespace Brutiquzz.Contractor.Arch.DataAccess.Queries;

internal sealed record GetProductByIdQuery(Guid productId, IMediator Mediator) : IGetProductByIdQuery
{
    public IQueryable<IProduct> Send()
    {
        return Mediator.Send(this).Result;
    }

    internal sealed class GetProductByIdQueryHandler(DatabaseContext databaseContext) : IRequestHandler<GetProductByIdQuery, IQueryable<IProduct>>
    {
        public async Task<IQueryable<IProduct>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
          => databaseContext.Products.Where(x => x.ProductId == request.productId);
    }
}
