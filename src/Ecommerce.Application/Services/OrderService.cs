using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Errors;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Application.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IProductRepository productRepository) : IOrderService
{
    public async Task<Result<IReadOnlyList<Contracts.OrderResponseDto>>> GetAllAsync()
    {
        var orders = await orderRepository.GetAllWithItemsAsync();

        return orders.Select(MapToDto).ToList();
    }

    public async Task<Result<Contracts.OrderResponseDto>> GetByIdAsync(Guid id)
    {
        var order = await orderRepository.GetByIdWithItemsAsync(id);

        if (order is null) return DomainErrors.Order.NotFound;

        return MapToDto(order);
    }

    public async Task<Result<Contracts.OrderResponseDto>> CreateAsync(Contracts.CreateOrderDto dto)
    {
        var productIds = dto.Items.Select(i => i.ProductId).ToHashSet();
        var products = await productRepository.GetProductsByIdsAsync(productIds);

        if (products.Count != productIds.Count)
        {
            return DomainErrors.Order.ProductsNotFound;
        }
        
        var order = new Order
        {
            Items = [.. dto.Items.Select(i => new OrderItem 
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = products[i.ProductId].Price 
            })]
        };

        await orderRepository.AddAsync(order);

        return MapToDto(order);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var deleted = await orderRepository.DeleteAsync(id);
        return deleted ? Result.Success() : DomainErrors.Order.NotFound;
    }

    private static Contracts.OrderResponseDto MapToDto(Order order) => new(
        order.Id,
        order.CreatedAt,
        order.Status.ToString(),
        order.TotalAmount,
        order.Items.Select(i => new Contracts.OrderItemResponseDto(
            i.ProductId,
            i.Quantity,
            i.UnitPrice
        )).ToList()
    );
}