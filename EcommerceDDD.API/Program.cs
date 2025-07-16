using EcommerceDDD.Infrastructure;
using EcommerceDDD.Application;
using Microsoft.OpenApi.Models;
using EcommerceDDD.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "🛒 Ecommerce DDD API",
        Version = "v1",
        Description = "API desenvolvida com Domain-Driven Design (DDD) para o curso de Arquiteturas de Software",
        Contact = new OpenApiContact
        {
            Name = "Prof. Danilo Aparecido",
            Url = new Uri("https://www.torneseumprogramador.com.br/")
        }
    });
});

// Registrar camadas da aplicação
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce DDD API v1");
        c.DocumentTitle = "🛒 Ecommerce DDD API - Documentação";
    });
}

app.UseHttpsRedirection();

// Adicionar middleware de tratamento de exceções
app.UseMiddleware<ValidationExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
