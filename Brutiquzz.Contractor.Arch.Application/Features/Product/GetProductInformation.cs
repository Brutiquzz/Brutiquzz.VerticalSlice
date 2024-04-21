using Ardalis.ApiEndpoints;
using Brutiquzz.Contractor.Arch.Contractor.Domain;
using Brutiquzz.Contractor.Arch.Contractor.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Brutiquzz.VerticalSlice.Features.Product;

public record GetProductInformation([FromRoute]Guid ProductId) : IRequest<IProductInformation>
{
    internal class GetProductInformationRequest(IMediator mediator) : EndpointBaseAsync
        .WithRequest<GetProductInformation>
        .WithResult<IProductInformation>
    {
        [HttpGet("product/{productId}")]
        public override async Task<IProductInformation> HandleAsync(GetProductInformation request, CancellationToken cancellationToken = default)
        => await mediator.Send(request, cancellationToken);
    }

    internal class GetProductInformationHandler(GetProductInformationValidator validator, IGetProductByIdQuery getProductByIdQuery)
        : IRequestHandler<GetProductInformation, IProductInformation>
    {
        public async Task<IProductInformation> Handle(GetProductInformation request, CancellationToken cancellationToken)
        {
            validator.Validate(request);

            var product = getProductByIdQuery
                .Send()
                .Single();

            
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
