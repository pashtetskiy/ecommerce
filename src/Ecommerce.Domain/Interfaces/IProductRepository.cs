using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);
    Task<Dictionary<Guid, Product>> GetProductsByIdsAsync(IEnumerable<Guid> ids);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> HasOrdersAsync(Guid id);

}