namespace Ecommerce.Application.DTOs;

public class Contracts
{
    public record CreateProductDto(string Name, decimal Price);

    public record PatchProductDto(string? Name, decimal? Price, uint Version);

    public record CreateOrderDto(List<OrderItemDto> Items);

    public record OrderItemDto(Guid ProductId, int Quantity);

    public record ProductDto(Guid Id, string Name, decimal Price, uint Version);

    public record OrderResponseDto(
        Guid Id,
        DateTimeOffset CreatedAt,
        string Status,
        decimal TotalAmount,
        List<OrderItemResponseDto> Items
    );

    public record OrderItemResponseDto(
        Guid ProductId,
        int Quantity,
        decimal UnitPrice
    );
}