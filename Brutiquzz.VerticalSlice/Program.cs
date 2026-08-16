using Brutiquzz.VerticalSlice;
using Cortex.Mediator.DependencyInjection;
using FluentValidation;
using Scalar.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.AddCortexMediator(
            new[] { typeof(Program) },
            options => options
                .AddDefaultBehaviors()
);


builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapApiEndpoints(Assembly.GetExecutingAssembly());

app.Run();


