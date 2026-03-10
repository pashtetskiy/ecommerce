namespace Ecommerce.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid OrderId { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; init; }
    public Order Order { get; init; } = null!;
    public Product Product { get; init; } = null!;
}