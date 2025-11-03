using BookingSystemApi.Entities;
using BookingSystemApi.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    public static User user = new();

    [HttpPost("register")]
    public ActionResult<User> Register(RegisterDto request)
    {
        var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

        user.Email = request.Email;
        user.PasswordHash = hashedPassword;
        user.Name = request.Name;
        user.Role = request.Role;

        return Ok(user);
    }

    [HttpPost("login")]
    public ActionResult<User> Login(LoginDto request)
    {
        if (user.Email != request.Email)
        {
            return NotFound("User not found");
        }
        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
        {
            return BadRequest("Wrong password");
        }

        string token = "success";

        return Ok(token);
    }
}
