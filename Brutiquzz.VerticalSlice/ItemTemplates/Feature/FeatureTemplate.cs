#if (isQuery)
using Cortex.Mediator.Queries;
#endif
#if (isCommand)
using Cortex.Mediator.Commands;
#endif
using Cortex.Mediator;
using FluentValidation;
using Scalar.AspNetCore;
using static TEMPLATE_NAMESPACE.FeatureTemplate;

namespace TEMPLATE_NAMESPACE;

#if (isQuery)
public record FeatureTemplate(Guid ProductId) : IQuery<FeatureTemplateResponse>
#endif
#if (isCommand)
public record FeatureTemplate(Guid ProductId) : ICommand<FeatureTemplateResponse>
#endif
{

    private sealed class FeatureTemplateEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder builder)
        {
#if (isRouteIdBound && operation == "GET")
            builder.MapGet("/*#(route)*/{id}", async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
#endif
#if (isRouteIdBound && operation == "DELETE")
            builder.MapDelete("/*#(route)*/{id}", async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
#endif
#if (isBodyBound && operation == "POST")
            builder.MapPost("/*#(route)*/", async (FeatureTemplate request, IMediator mediator, CancellationToken cancellationToken) =>
#endif
#if (isBodyBound && operation == "PUT")
            builder.MapPut("/*#(route)*/", async (FeatureTemplate request, IMediator mediator, CancellationToken cancellationToken) =>
#endif
#if (isBodyBound && operation == "PATCH")
            builder.MapPatch("/*#(route)*/", async (FeatureTemplate request, IMediator mediator, CancellationToken cancellationToken) =>
#endif
            {
#if (isRouteIdBound && isQuery)
                return (await mediator.SendQueryAsync(new FeatureTemplate(id), cancellationToken));
#endif
#if (isRouteIdBound && isCommand)
                return (await mediator.SendCommandAsync(new FeatureTemplate(id), cancellationToken));
#endif
#if (isBodyBound && isCommand)
                return (await mediator.SendCommandAsync(request, cancellationToken));
#endif
            })
                .WithName("FeatureTemplate")
#if (isPostOperation)
                .WithSummary("Creating a Product Information")
                .WithDescription("This is a description on how a product information gets created")
#endif
                .Produces<FeatureTemplateResponse>(StatusCodes.Status200OK)
#if (isPostOperation)
                .Produces(StatusCodes.Status400BadRequest)
#endif
#if (isQuery)
                .Produces(StatusCodes.Status404NotFound)
#endif
                // Authorization: endpoints default to anonymous access for maximum flexibility.
                // To require JWT authentication, replace .AllowAnonymous() with:
                //     .RequireAuthorization(AuthorizationPolicies.AuthenticatedUser)
                // Ensure your appsettings.json JwtBearer section is configured with your IdP's Authority and Audience.
                .AllowAnonymous()
                .Stable() // .Experimental() // .Deprecated() // .Hidden()
#if (isPostOperation)
                .WithTags("TEMPLATE_TAG", "Create");
#endif
#if (!isPostOperation)
                .WithTags("TEMPLATE_TAG");
#endif
        }
    }

#if (isQuery)
    public sealed class FeatureTemplateHandler(FeatureTemplateValidator validator)
        : IQueryHandler<FeatureTemplate, FeatureTemplateResponse>
#endif
#if (isCommand)
    public sealed class FeatureTemplateHandler(FeatureTemplateValidator validator)
        : ICommandHandler<FeatureTemplate, FeatureTemplateResponse>
#endif
    {
        public async Task<FeatureTemplateResponse> Handle(FeatureTemplate request, CancellationToken cancellationToken)
        {
            validator.Validate(request);

            return new FeatureTemplateResponse
            {
                ProductId = request.ProductId,
                Name = "Sample Product",
                Description = "This is a sample product."
            };
        }
    }

    public sealed class FeatureTemplateValidator : AbstractValidator<FeatureTemplate>
    {
        public FeatureTemplateValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductId).NotEqual(Guid.Empty);
        }
    }

    public sealed class FeatureTemplateResponse
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
