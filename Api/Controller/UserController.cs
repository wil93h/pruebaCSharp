using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pruebaCSharp.DataService;
using pruebaCSharp.Entities;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] User user)
    {
        user.SetPassword(user.Password);

        await _userRepository.AddAsync(user); 

        return Ok(user);
    }

    [HttpDelete("{id}")]
public async Task<IActionResult> Delete(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return NotFound();
        
        await _userRepository.DeleteAsync(user);
        return NoContent();
    }
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
            return NotFound(); // Si no se encuentra el usuario, se retorna NotFound

        return Ok(user); // Retorna el usuario si se encuentra
    }
}
