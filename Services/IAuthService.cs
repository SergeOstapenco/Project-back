using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto request);
    Task<AuthResponseDto> LoginAsync(LoginDto request);
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto request)
    {
        // Check if user already exists
        if (await _context.Users.AnyAsync(u => u.Username == request.Username))
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Username already exists"
            };
        }

        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Email already exists"
            };
        }

        // Create new user
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = HashPassword(request.Password),
            Role = "user"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = GenerateJwtToken(user);
        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };

        return new AuthResponseDto
        {
            Success = true,
            Message = "User registered successfully",
            Token = token,
            User = userDto
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Invalid username or password"
            };
        }

        var token = GenerateJwtToken(user);
        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };

        return new AuthResponseDto
        {
            Success = true,
            Message = "Login successful",
            Token = token,
            User = userDto
        };
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new System.Security.Claims.Claim("sub", user.Id.ToString()),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Username),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, user.Email),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    private bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput == hash;
    }
}
