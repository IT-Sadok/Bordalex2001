using Application.Exports.DTOs;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Infrastructure.Repositories;

public class HostRepository(UserManager<AppUser> userManager, AppDbContext dbContext) : IHostRepository
{
    public async IAsyncEnumerable<HostExportDto> StreamHostsAsync([EnumeratorCancellation] CancellationToken ct = default) 
    { 
        var hostRoleId = await dbContext.Roles.Where(r => r.Name == "Host").Select(r => r.Id).SingleAsync(ct);

        var hostsQuery = from user in dbContext.Users.AsNoTracking()
                         join userRole in dbContext.UserRoles on user.Id equals userRole.UserId
                         where userRole.RoleId == hostRoleId
                         select user;

        await foreach (var host in hostsQuery.AsAsyncEnumerable().WithCancellation(ct))
        {
            var apartments = await dbContext.Apartments
                .AsNoTracking()
                .Where(a => a.HostId == Guid.Parse(host.Id))
                .Select(a => new ApartmentExportDto
                {
                    ExternalId = a.Id.ToString()!,
                    Title = a.Title,
                    Description = a.Description,
                    Address = a.Address,
                    PricePerNight = a.PricePerNight,
                    IsAvailable = a.IsAvailable,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt,
                    DeletedAt = a.DeletedAt
                }).ToListAsync(ct);

            yield return new HostExportDto
            {
                ExternalId = host.Id!,
                Email = host.Email!,
                DisplayName = host.DisplayName,
                Apartments = apartments
            };
        }
    }
}
