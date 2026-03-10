using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Domain.Entities;

public class Product
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public ICollection<OrderItem> OrderItems { get; init; } = [];
    [Timestamp]
    public uint Version { get; set; }
}