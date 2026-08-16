using Brutiquzz.VerticalSlice.Domain;
using Cortex.Mediator;
using Cortex.Mediator.Queries;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

namespace Brutiquzz.VerticalSlice.Features.Product;

public record GetProductInformation(Guid ProductId) : IQuery<ActionResult<ProductInformation>>
{

    private sealed class GetProductInformationEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapGet("/product/{id}", async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return (await mediator.SendQueryAsync(new GetProductInformation(id), cancellationToken));
            })
                .WithName("GetProductInformation")
                .WithSummary("Getting a Product Information")
                .WithDescription("This is a description on how a product information gets retrieved")
                .Produces<ProductInformation>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                // Authorization: endpoints default to anonymous access for maximum flexibility.
                // To require JWT authentication, replace .AllowAnonymous() with:
                //     .RequireAuthorization(AuthorizationPolicies.AuthenticatedUser)
                // Ensure your appsettings.json JwtBearer section is configured with your IdP's Authority and Audience.
                .AllowAnonymous()
                .Stable() // .Experimental() // .Deprecated() // .Hidden()
                .WithTags("Product", "Get");
        }
    }

    public sealed class GetProductInformationHandler(GetProductInformationValidator validator)
        : IQueryHandler<GetProductInformation, ActionResult<ProductInformation>>
    {
        public async Task<ActionResult<ProductInformation>> Handle(GetProductInformation request, CancellationToken cancellationToken)
        {
            validator.Validate(request);

            var product = new ProductInformation() { Id = request.ProductId, Name = "Sample Product", Description = "This is a sample product." };

            return product;
        }
    }

    public sealed class GetProductInformationValidator : AbstractValidator<GetProductInformation>
    {
        public GetProductInformationValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductId).NotEqual(Guid.Empty);
        }
    }
}
