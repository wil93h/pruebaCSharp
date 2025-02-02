using System.Collections.Generic;
using System.Threading.Tasks;
using pruebaCSharp.Entities;

namespace pruebaCSharp.DataService;

public interface IProductRepository
{
    Task IncrementStock(int productId, int quantity);
    Task DecrementStock(int productId, int quantity);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product entity);
    Task UpdateAsync(Product entity);
    Task DeleteAsync(Product entity);
    Task SaveChangesAsync();
}