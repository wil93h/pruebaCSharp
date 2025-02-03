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
        return await _context.Users
            .FromSqlRaw("SELECT * FROM get_user_by_email({0})", email)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .FromSqlRaw("SELECT * FROM get_all_users()")
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .FromSqlRaw("SELECT * FROM users WHERE id = {0}", id)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(User entity)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "SELECT add_user({0}, {1}, {2})",
            entity.Username, entity.Email, entity.Password
        );
    }

    public async Task UpdateAsync(User entity)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "SELECT update_user({0}, {1}, {2})",
            entity.Id, entity.Username, entity.Email
        );
    }

    public async Task DeleteAsync(User entity)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "SELECT delete_user({0})",
            entity.Id
        );
    }

}