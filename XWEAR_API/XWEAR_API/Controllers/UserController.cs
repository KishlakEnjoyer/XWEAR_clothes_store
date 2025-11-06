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
    public async Task<IActionResult> Register(string _email, string _phone, string _nickname, string _password)
    {
        if (await _context.Users.AnyAsync(u => u.Email == _email)) return BadRequest("Email уже используется");
        if (await _context.Users.AnyAsync(u => u.Phone == _phone)) return BadRequest("Номер телефона уже используется");

        var user = new User { Email = _email, Nickname = _nickname, PasswordHash = BCrypt.Net.BCrypt.HashPassword(_password), Phone = _phone };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Ok(new { message = "Пользователь зарегистрирован", email = user.Email, nickname = user.Nickname });
    }

    // POST: api/User/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(string _logEmail, string _logPassword)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == _logEmail);
        if (user == null || !BCrypt.Net.BCrypt.Verify(_logPassword, user.PasswordHash)) return Unauthorized("Неверный логин или пароль");
        return Ok(user);
    }

    // PUT: api/User/change-nickname
    [HttpPut("change-nickname")]
    public async Task<IActionResult> ChangeNickname(string _changeEmail, string _newNickname)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == _changeEmail);
        if (user == null) return NotFound("Пользователь не найден");
        user.Nickname = _newNickname;
        await _context.SaveChangesAsync();
        return Ok("Ник изменён");
    }

    // PUT: api/User/change-password
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(string _oldPass, string _email, string _newPass)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == _email);
        if (user == null) return NotFound("Пользователь не найден");
        if (!BCrypt.Net.BCrypt.Verify(_oldPass, user.PasswordHash)) return BadRequest("Старый пароль неверен");
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(_newPass);
        await _context.SaveChangesAsync();
        return Ok("Пароль изменён");
    }
}