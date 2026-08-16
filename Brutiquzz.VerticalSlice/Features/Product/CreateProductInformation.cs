using Brutiquzz.VerticalSlice.Domain;
using Cortex.Mediator;
using Cortex.Mediator.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

namespace Brutiquzz.VerticalSlice.Features.Product;

public record CreateProductInformation(Guid ProductId) : ICommand<ActionResult<ProductInformation>>
{

    private sealed class CreateProductInformationEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapPost("/product", async (CreateProductInformation request, IMediator mediator, CancellationToken cancellationToken) =>
            {
                return (await mediator.SendCommandAsync(request, cancellationToken));
            })
                .WithName("CreateProductInformation")
                .WithSummary("Creating a Product Information")
                .WithDescription("This is a description on how a product information gets created")
                .Produces<ProductInformation>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                // Authorization: endpoints default to anonymous access for maximum flexibility.
                // To require JWT authentication, replace .AllowAnonymous() with:
                //     .RequireAuthorization(AuthorizationPolicies.AuthenticatedUser)
                // Ensure your appsettings.json JwtBearer section is configured with your IdP's Authority and Audience.
                .AllowAnonymous()
                .Stable() // .Experimental() // .Deprecated() // .Hidden()
                .WithTags("Product", "Create");
        }
    }

    public sealed class CreateProductInformationHandler(CreateProductInformationValidator validator)
        : ICommandHandler<CreateProductInformation, ActionResult<ProductInformation>>
    {
        public async Task<ActionResult<ProductInformation>> Handle(CreateProductInformation request, CancellationToken cancellationToken)
        {
            validator.Validate(request);

            var product = new ProductInformation() { Id = request.ProductId, Name = "Sample Product", Description = "This is a sample product." };

            return product;
        }
    }

    public sealed class CreateProductInformationValidator : AbstractValidator<CreateProductInformation>
    {
        public CreateProductInformationValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductId).NotEqual(Guid.Empty);
        }
    }
}
