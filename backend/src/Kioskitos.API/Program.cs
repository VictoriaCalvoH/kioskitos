using Microsoft.OpenApi;
using Kioskitos.API.Middleware;
using Kioskitos.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("Default"));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Kioskitos API",
        Version = "v1",
        Description = "API del sistema Kioskitos."
    });
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Kioskitos API v1");
    });
}

app.MapControllers();

app.Run();

public partial class Program
{
}
