using Ecommerce.Api.ErrorHandlers;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Ecommerce.Application.Validators;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IValidator<Contracts.CreateOrderDto>, CreateOrderDtoValidator>();
builder.Services.AddScoped<IValidator<Contracts.CreateProductDto>, ProductDtoValidator>();
builder.Services.AddScoped<IValidator<Contracts.PatchProductDto>, PatchProductDtoValidator>();
builder.Services.AddScoped<IValidator<Contracts.OrderItemDto>, OrderItemDtoValidator>();


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<EcommerceDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("ecommerce-db-pg"))
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information));
}
else
{
    builder.Services.AddDbContext<EcommerceDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("ecommerce-db-pg")));
}


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();