using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pruebaCSharp.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto dto)
    {
        var user = await _authService.RegisterUser(dto.Username, dto.Email, dto.Password);
        if (user == null) return BadRequest("User already exists");
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var token = await _authService.Authenticate(dto.Email, dto.Password);
        if (token == null) return Unauthorized();
        return Ok(new { Token = token });
    }
}

public class UserRegistrationDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}