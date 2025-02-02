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

    // Métodos para manejar el stock
    public async Task IncrementStock(int productId, int quantity)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null)
        {
            product.Stock += quantity;
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DecrementStock(int productId, int quantity)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product != null && product.Stock >= quantity)
        {
            product.Stock -= quantity;
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    // Implementación de IRepository optimizada
    public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();
    
    public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);
    
    public async Task AddAsync(Product entity)
    {
        await _context.Products.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Product entity)
    {
        _context.Products.Update(entity);
        await _context.SaveChangesAsync(); // Ahora sí usa await
    }
    
    public async Task DeleteAsync(Product entity)
    {
        _context.Products.Remove(entity);
        await _context.SaveChangesAsync(); // Ahora sí usa await
    }
    
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}