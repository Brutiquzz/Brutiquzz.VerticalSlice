using Cortex.Mediator;
using Cortex.Mediator.Commands;
using FluentValidation;
using Scalar.AspNetCore;
using static Brutiquzz.VerticalSlice.Features.Product.CreateProductInformation;

namespace Brutiquzz.VerticalSlice.Features.Product;

public record CreateProductInformation(Guid ProductId) : ICommand<CreateProductInformationResponse>
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
                .Produces<CreateProductInformationResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
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
        : ICommandHandler<CreateProductInformation, CreateProductInformationResponse>
    {
        public async Task<CreateProductInformationResponse> Handle(CreateProductInformation request, CancellationToken cancellationToken)
        {
            validator.Validate(request);

            return new CreateProductInformationResponse
            {
                ProductId = request.ProductId,
                Name = "Sample Product",
                Description = "This is a sample product."
            };
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

    public sealed class CreateProductInformationResponse
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
