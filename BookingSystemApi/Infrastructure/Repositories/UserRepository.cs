using Application.Features.Imports.DTOs;
using Application.Features.Users.Interfaces;
using Dapper;
using System.Data;
using System.Runtime.CompilerServices;

namespace Infrastructure.Repositories;

public class UserRepository(IDbConnection dbConnection) : IUserRepository
{
    public async IAsyncEnumerable<HostImportDto> StreamHostsAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        var hostRoleId = await dbConnection.QueryFirstAsync<Guid>("SELECT Id FROM AspNetRoles WHERE Name = @Name", new { Name = "Host" });

        var hostsQuery = @"
            SELECT 
                u.Id AS ExternalId,
                u.Email,
                u.DisplayName,
                u.CreatedAt,
                u.UpdatedAt,
                u.DeletedAt
            FROM AspNetUsers u
            INNER JOIN AspNetUserRoles ur ON u.Id = ur.UserId
            WHERE ur.RoleId = @HostRoleId";

        var hosts = await dbConnection.QueryAsync<HostImportDto>(hostsQuery, new { HostRoleId = hostRoleId });

        foreach (var host in hosts)
        {
            yield return host;
        }
    }
}
