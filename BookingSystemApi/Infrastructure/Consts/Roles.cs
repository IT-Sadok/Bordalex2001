using Microsoft.Extensions.Configuration;

namespace Infrastructure.Consts;

public class Roles(IConfiguration configuration)
{
    public string[] GetRoles()
    {
        return configuration.GetSection("Roles").Get<string[]>() ?? throw new ArgumentException("Roles section is missing in configuration.");
    }
}
