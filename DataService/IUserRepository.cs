using System.Collections.Generic;
using System.Threading.Tasks;
using pruebaCSharp.Entities;

namespace pruebaCSharp.DataService;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User entity);
    Task UpdateAsync(User entity);
    Task DeleteAsync(User entity);
    Task SaveChangesAsync();
}