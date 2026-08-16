# Brutiquzz Vertical Slice API Template

A local .NET project template for creating .NET 10 ASP.NET Core APIs organized by vertical slice. Generated APIs include Cortex Mediator, FluentValidation, OpenAPI, Scalar, endpoint discovery, and a working Product feature example.

The template emits only the API project. It does not include the repository's solution or `Brutiquzz.VerticalSlice.DataAccess` project.

An item template is also included to generate individual vertical-slice feature files within an API project.

## Prerequisites

- .NET 10 SDK
- PowerShell to run the verification script

## Install the local templates

Run this command from the repository root:

```powershell
dotnet new install .\Brutiquzz.VerticalSlice
```

Confirm that the templates are available:

```powershell
dotnet new list brutiquzz
```

You should see both `brutiquzz-verticalslice-api` (project template) and `brutiquzz-verticalslice-feature` (item template).

## Create an API

```powershell
dotnet new brutiquzz-verticalslice-api -n MyApi
```

The template creates `MyApi\MyApi.csproj` and replaces the source project name in filenames and namespaces with `MyApi`.

Build and run the generated API:

```powershell
dotnet build .\MyApi\MyApi.csproj
dotnet run --project .\MyApi\MyApi.csproj
```

In the Development environment, the launch profile opens the sample endpoint at:

```text
/api/product/11111111-1111-1111-1111-111111111111
```

Scalar API documentation is available at `/scalar/v1` while the application is running in Development.

## Create a feature within an API project

Navigate to a feature folder inside your API project, then generate a new feature file:

```powershell
cd MyApi\Features\Order
dotnet new brutiquzz-verticalslice-feature `
  -n CreateOrder `
  --operation POST `
  --route /order `
  --Tag Order
```

This creates `CreateOrder.cs` with:

- A `CreateOrder` record implementing `ICommand<CreateOrderResponse>`
- A nested `CreateOrderEndpoint` mapping `POST /order`
- A nested `CreateOrderHandler` invoking validation and returning a sample response
- A nested `CreateOrderValidator` with ProductId validation
- A nested `CreateOrderResponse` record

The namespace is inferred from the project and current folder. You can override it with `--namespace MyApi.Features.Order`.

### Feature template parameters

| Parameter | Alias | Required | Default | Description |
|-----------|-------|----------|---------|-------------|
| `--operation` | `-op` | Yes | — | HTTP method: `GET`, `POST`, `PUT`, `PATCH`, or `DELETE` |
| `--route` | `-r` | No | `/product` | Endpoint route |
| `--Tag` | `-T` | No | `Product` | OpenAPI tag |
| `--namespace` | `-p:n` | No | Inferred | Feature namespace |

GET operations implement `IQuery<>` and use `SendQueryAsync`. POST, PUT, PATCH, and DELETE implement `ICommand<>` and use `SendCommandAsync`.

GET and DELETE bind `Guid id` from the route. POST, PUT, and PATCH bind the feature record from the request body.

POST features include Create-specific summary, description, and tags. Other operations omit those details but retain the required structure.

## Verify the templates

The verification script installs both templates into an isolated template hive, generates a temporary API with a different project name, generates POST and GET features inside it, checks their contents and name substitutions, and restores and builds them. Temporary files and the isolated hive are removed afterward.

```powershell
.\verify-template.ps1
```

## Update the installed templates

After changing the template source, reinstall them:

```powershell
dotnet new install .\Brutiquzz.VerticalSlice --force
```

## Uninstall the templates

```powershell
dotnet new uninstall .\Brutiquzz.VerticalSlice
```
