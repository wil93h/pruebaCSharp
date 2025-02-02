using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pruebaCSharp.Entities;

namespace pruebaCSharp.DataService;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
   
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();
    
    public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);
    
    public async Task AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(User entity)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(User entity)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}