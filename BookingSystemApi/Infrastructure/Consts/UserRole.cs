using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Consts
{
    public static class UserRole
    {
        public const string Admin = "Admin";
        public const string Host = "Host";
        public const string Client = "Client";

        public static readonly IReadOnlyCollection<string> AllRoles = [Admin, Host, Client];

        public static bool IsValidRole(string role) => AllRoles.Contains(role);
    }
}
