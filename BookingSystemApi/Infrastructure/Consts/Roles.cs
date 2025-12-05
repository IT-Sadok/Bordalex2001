namespace Infrastructure.Consts;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Host = "Host";
    public const string Client = "Client";

    public static readonly IReadOnlyCollection<string> AllRoles = [Admin, Host, Client];
}
