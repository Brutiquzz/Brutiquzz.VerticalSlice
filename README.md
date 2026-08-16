# Brutiquzz Vertical Slice API Template

A local .NET project template for creating .NET 10 ASP.NET Core APIs organized by vertical slice. Generated APIs include Cortex Mediator, FluentValidation, OpenAPI, Scalar, endpoint discovery, and a working Product feature example.

The template emits only the API project. It does not include the repository's solution or `Brutiquzz.VerticalSlice.DataAccess` project.

## Prerequisites

- .NET 10 SDK
- PowerShell to run the verification script

## Install the local template

Run this command from the repository root:

```powershell
dotnet new install .\Brutiquzz.VerticalSlice
```

Confirm that the template is available:

```powershell
dotnet new list brutiquzz-verticalslice-api
```

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

## Verify the template

The verification script installs the template into an isolated template hive, generates a temporary API with a different project name, checks its contents and name substitutions, and restores and builds it. Temporary files and the isolated hive are removed afterward.

```powershell
.\verify-template.ps1
```

## Update the installed template

After changing the template source, reinstall it:

```powershell
dotnet new install .\Brutiquzz.VerticalSlice --force
```

## Uninstall the template

```powershell
dotnet new uninstall .\Brutiquzz.VerticalSlice
```
