using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class ProductRepository(EcommerceDbContext db) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetAllAsync() => 
        await db.Products.AsNoTracking().ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid id) => 
        await db.Products.FirstOrDefaultAsync(x=> x.Id == id);

    public async Task<Dictionary<Guid, Product>> GetProductsByIdsAsync(IEnumerable<Guid> ids) =>
        await db.Products
            .Where(p => ids.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

    public async Task<bool> HasOrdersAsync(Guid id)
    {
        return await db.OrderItems.Where(o => o.ProductId == id).AnyAsync();
    }

    public async Task AddAsync(Product product)
    {
        db.Products.Add(product);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        db.Entry(product).Property(p => p.Version).OriginalValue = product.Version;
        db.Products.Update(product);
        await db.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var rows = await db.Products.Where(p => p.Id == id).ExecuteDeleteAsync();
        return rows > 0;
    }
}