using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using RepositoryPatternWithUOW.Application;
using RepositoryPatternWithUOW.Application.Common.Interfaces;
using RepositoryPatternWithUOW.Infranstructure.Data;
using RepositoryPatternWithUOW.Infranstructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Repository Pattern API",
            Version = "v1",
            Description =
                "A learning Web API demonstrating Repository Pattern, " +
                "Unit of Work, CQRS, MediatR, and Entity Framework Core."
        });

    options.IncludeXmlComments(
        Path.Combine(
            AppContext.BaseDirectory,
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(ApplicationAssemblyReference).Assembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_1;
    });

    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Repository Pattern API";
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Repository Pattern API v1");
    });
}

app.MapControllers();

app.Run();