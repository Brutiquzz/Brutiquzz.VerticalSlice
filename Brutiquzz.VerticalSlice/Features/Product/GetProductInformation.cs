using Cortex.Mediator;
using Cortex.Mediator.Queries;
using FluentValidation;
using Scalar.AspNetCore;
using static Brutiquzz.VerticalSlice.Features.Product.GetProductInformation;

namespace Brutiquzz.VerticalSlice.Features.Product;

public record GetProductInformation(Guid ProductId) : IQuery<GetProductInformationResponse>
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
                .Produces<GetProductInformationResponse>(StatusCodes.Status200OK)
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
        : IQueryHandler<GetProductInformation, GetProductInformationResponse>
    {
        public async Task<GetProductInformationResponse> Handle(GetProductInformation request, CancellationToken cancellationToken)
        {
            validator.Validate(request);

            return new GetProductInformationResponse
            {
                ProductId = request.ProductId,
                Name = "Sample Product",
                Description = "This is a sample product."
            };
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

    public sealed class GetProductInformationResponse
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
