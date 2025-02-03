using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using pruebaCSharp.DataService;
using pruebaCSharp.Entities;
using System.Text;

namespace pruebaCSharp.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository; // ✅ Usa la interfaz
    private readonly IConfiguration _configuration;
public AuthService(IUserRepository userRepository, IConfiguration configuration) 
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<User?> RegisterUser(string username, string email, string password)
    {
        Console.WriteLine($"Hola, {email}");
        var existingUser = await _userRepository.GetByEmailAsync(email);
        Console.WriteLine($"existingUser::::, {existingUser}");
        if (existingUser != null) return null;

        var user = new User
        {
            Username = username,
            Email = email
        };
        user.SetPassword(password);

        await _userRepository.AddAsync(user);
        return user;
    }

    public async Task<string?> Authenticate(string email, string password)
    {
        Console.WriteLine($"Hola, {email}");

        var user = await _userRepository.GetByEmailAsync(email);
    if (user == null || !user.VerifyPassword(password)) return null;

    try
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    catch (Exception ex)
    {
        // Log the exception or handle it here
        Console.WriteLine($"Error generating token: {ex.Message}");
        return null;
    }
    }
}