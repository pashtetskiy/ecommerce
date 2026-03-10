using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Application.Interfaces;

public interface IProductService
{
    Task<Result<IReadOnlyList<Contracts.ProductDto>>> GetAllAsync();
    Task<Result<Contracts.ProductDto>> GetByIdAsync(Guid id);
    Task<Result<Contracts.ProductDto>> CreateAsync(Contracts.CreateProductDto dto);
    Task<Result> PatchAsync(Guid id, Contracts.PatchProductDto dto);
    Task<Result> DeleteAsync(Guid id);
}