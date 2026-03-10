using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Interfaces;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetAllWithItemsAsync();
    Task<Order?> GetByIdWithItemsAsync(Guid id);
    Task AddAsync(Order order);
    Task<bool> DeleteAsync(Guid id);
    Task UpdateAsync(Order order);
}