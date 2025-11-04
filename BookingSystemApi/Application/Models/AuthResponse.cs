namespace Application.Models;

public class AuthResponse(string token)
{
    public string Token { get; set; } = token;
}
