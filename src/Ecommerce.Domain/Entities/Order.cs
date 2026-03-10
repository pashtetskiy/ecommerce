using Ecommerce.Domain.Errors;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class Order
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public ICollection<OrderItem> Items = new List<OrderItem>();

    public decimal TotalAmount => Items.Sum(i => i.UnitPrice * i.Quantity);
}

public enum OrderStatus { Pending, Paid, Shipped, Cancelled }