using Infrastructure.Consts.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Consts;

public class Roles(IConfiguration configuration) : IRoles
{
    public string[] GetRoles()
    {
        return configuration.GetSection("Roles").Get<string[]>() ?? throw new ArgumentException("Roles section is missing in configuration.");
    }
}
