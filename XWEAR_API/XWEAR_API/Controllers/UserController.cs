using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XWEAR_API.Models;

namespace XWEAR_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly XwearDbContext _context;

    public UserController(XwearDbContext context)
    {
        _context = context;
    }

    // POST: api/User/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            return BadRequest(new { message = "Email уже используется" });
        if (await _context.Users.AnyAsync(u => u.Phone == request.Phone))
            return BadRequest(new { message = "Номер телефона уже используется"});

        var user = new User
        {
            Email = request.Email,
            Nickname = request.Nickname,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Phone = request.Phone
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Пользователь зарегистрирован", email = user.Email, nickname = user.Nickname });
    }

    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    // POST: api/User/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized(new { message = "Неверный логин или пароль" });

        return Ok(user);
    }

    [HttpPut("change-nickname")]
    public async Task<IActionResult> ChangeNickname([FromBody] ChangeNicknameRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.NewNickname))
            return BadRequest(new { message = "Email и новый никнейм обязательны"});

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null)
            return NotFound(new { message = "Пользователь не найден" });

        user.Nickname = request.NewNickname;
        await _context.SaveChangesAsync();

        return Ok(new { message = "Ник изменён" });
    }

    // Модель для запроса
    public class ChangeNicknameRequest
    {
        public string Email { get; set; } = string.Empty;
        public string NewNickname { get; set; } = string.Empty;
    }

    // PUT: api/User/change-password
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        if (request == null) return BadRequest(new { message = "Данные не указаны" });

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null) return NotFound(new { message = "Пользователь не найден" });

        if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash))
            return BadRequest(new { message = "Старый пароль неверен" });

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Пароль изменён" });
    }
}

public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class ChangeNicknameRequest
{
    public string Email { get; set; } = string.Empty;
    public string NewNickname { get; set; } = string.Empty;
}

public class ChangePasswordRequest
{
    public string Email { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}