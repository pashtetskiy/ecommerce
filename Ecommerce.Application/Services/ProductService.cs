using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Errors;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Services;

public class ProductService(
    IProductRepository productRepository) : IProductService
{
    public async Task<Result<IReadOnlyList<Contracts.ProductDto>>> GetAllAsync()
    {
        var products = await productRepository.GetAllAsync();

        var dtos = products.Select(p =>
            new Contracts.ProductDto(p.Id, p.Name, p.Price, p.Version)).ToList();

        return dtos;
    }

    public async Task<Result<Contracts.ProductDto>> GetByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return DomainErrors.Product.NotFound;

        return new Contracts.ProductDto(product.Id, product.Name, product.Price, product.Version);
    }

    public async Task<Result<Contracts.ProductDto>> CreateAsync(Contracts.CreateProductDto dto)
    {
        var product = new Product { Name = dto.Name, Price = dto.Price };
        await productRepository.AddAsync(product);

        return new Contracts.ProductDto(product.Id, product.Name, product.Price, product.Version);
    }

    public async Task<Result> PatchAsync(Guid id, Contracts.PatchProductDto dto)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return DomainErrors.Product.NotFound;

        if (dto.Name is not null) product.Name = dto.Name;
        if (dto.Price is not null) product.Price = dto.Price.Value;
        product.Version = dto.Version;

        try
        {
            await productRepository.UpdateAsync(product);
            return Result.Success();
        }
        catch (DbUpdateConcurrencyException)
        {
            return DomainErrors.Product.ConcurrencyConflict;
        }
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var deleted = await productRepository.DeleteAsync(id);
        return deleted ? Result.Success() : DomainErrors.Product.NotFound;
    }
}