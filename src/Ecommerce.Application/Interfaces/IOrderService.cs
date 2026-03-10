using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Application.Interfaces;

public interface IOrderService
{
    Task<Result<IReadOnlyList<Contracts.OrderResponseDto>>> GetAllAsync();
    Task<Result<Contracts.OrderResponseDto>> GetByIdAsync(Guid id);
    Task<Result<Contracts.OrderResponseDto>> CreateAsync(Contracts.CreateOrderDto dto);
    Task<Result> DeleteAsync(Guid id);
}