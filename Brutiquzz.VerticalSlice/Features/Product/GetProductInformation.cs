using Ardalis.ApiEndpoints;
using Brutiquzz.VerticalSlice.DataAccess.Contexts;
using Brutiquzz.VerticalSlice.Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Brutiquzz.VerticalSlice.Features.Product;

public record GetProductInformation([FromRoute] Guid ProductId) : IRequest<ProductInformation>
{
    internal class GetProductInformationRequest(IMediator mediator) : EndpointBaseAsync
        .WithRequest<GetProductInformation>
        .WithActionResult<ProductInformation>
    {
        [HttpGet("product/{productId}")]
        public override async Task<ActionResult<ProductInformation>> HandleAsync(GetProductInformation request, CancellationToken cancellationToken = default)
        => await mediator.Send(request, cancellationToken);
    }

    internal class GetProductInformationHandler(DatabaseContext databaseContext)
        : IRequestHandler<GetProductInformation, ProductInformation>
    {
        private readonly GetProductInformationValidator _validator = new();
        public async Task<ProductInformation> Handle(GetProductInformation request, CancellationToken cancellationToken)
        {
            _validator.Validate(request);

            var product = await databaseContext.Products
                .SingleOrDefaultAsync(x => x.ProductId == request.ProductId, cancellationToken);

            if (product == null) new NotFoundResult();

            return new ProductInformation(product);
        }
    }

    internal class GetProductInformationValidator : AbstractValidator<GetProductInformation>
    {
        public GetProductInformationValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
