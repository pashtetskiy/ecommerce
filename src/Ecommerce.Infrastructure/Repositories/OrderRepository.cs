using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class OrderRepository(EcommerceDbContext db) : IOrderRepository
{
    public async Task<IReadOnlyList<Order>> GetAllWithItemsAsync() =>
        await db.Orders.Include(o => o.Items).AsNoTracking().ToListAsync();

    public async Task<Order?> GetByIdWithItemsAsync(Guid id) =>
        await db.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);
    
    public async Task AddAsync(Order order)
    {
        db.Orders.Add(order);
        await db.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var rows = await db.Orders.Where(o => o.Id == id).ExecuteDeleteAsync();
        return rows > 0;
    }

    public async Task UpdateAsync(Order order)
    {
        db.Orders.Update(order);
        await db.SaveChangesAsync();
    }
}