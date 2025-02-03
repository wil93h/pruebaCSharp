using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pruebaCSharp.Entities;

namespace pruebaCSharp.DataService;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.FromSqlRaw("SELECT * FROM GetAllProducts()").ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var products = await _context.Products
            .FromSqlRaw("SELECT * FROM GetProductById({0})", id)
            .ToListAsync();
        
        return products.FirstOrDefault();
    }

    public async Task AddAsync(Product entity)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "CALL AddProduct({0}, {1}, {2}, {3})",
            entity.Name, entity.Description, entity.Price, entity.Stock);
    }

    public async Task UpdateAsync(Product entity)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "CALL UpdateProduct({0}, {1}, {2}, {3}, {4})",
            entity.Id, entity.Name, entity.Description, entity.Price, entity.Stock);
    }

    public async Task DeleteAsync(Product entity)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "CALL DeleteProduct({0})", entity.Id);
    }

    public async Task IncrementStock(int productId, int quantity)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "CALL IncrementProductStock({0}, {1})", productId, quantity);
    }

    public async Task DecrementStock(int productId, int quantity)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "CALL DecrementProductStock({0}, {1})", productId, quantity);
    }
}