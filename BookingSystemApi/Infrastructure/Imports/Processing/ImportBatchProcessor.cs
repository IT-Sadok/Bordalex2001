using Application.Features.Imports.DTOs;
using Application.Features.Imports.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Imports.Processing;

public sealed class ImportBatchProcessor(
    AppDbContext dbContext, 
    IUserManagerWrapper<AppUser> userManager,
    IOptions<IdentityOptionsConfiguration> identityOptions) : IImportBatchProcessor
{
    public async Task<int> ProcessBatchAsync(
        Guid jobId, 
        IReadOnlyCollection<ImportEnvelopeDto> batch, 
        CancellationToken ct = default)
    {
        if (batch.Count == 0)
            return 0;

        var defaultPassword = identityOptions.Value.DefaultImportPassword;

        await using var transaction = await dbContext.Database.BeginTransactionAsync(ct);

        try
        {
            var processed = 0;

            var hostExternalIds = batch
                .Select(b => b.Host.ExternalId).Distinct().ToList();

            var existingHosts = await dbContext.Users
                .Where(u => hostExternalIds.Contains(u.ExternalId)).ToDictionaryAsync(u => u.ExternalId!, ct);

            foreach (var envelope in batch)
            { 
                var hostDto = envelope.Host;

                if (!existingHosts.TryGetValue(hostDto.ExternalId, out var host))
                {
                    host = new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        ExternalId = hostDto.ExternalId,
                        Email = hostDto.Email,
                        UserName = hostDto.Email,
                        DisplayName = hostDto.DisplayName,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(host, defaultPassword);

                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        throw new InvalidOperationException($"Failed to create user for Host {hostDto.Email}: {errors}");
                    }

                    existingHosts[hostDto.ExternalId] = host;
                }
                else
                {
                    host.Email = hostDto.Email;
                    host.UserName = hostDto.Email;
                    host.DisplayName = hostDto.DisplayName;
                }
            }

            await dbContext.SaveChangesAsync(ct);

            var apartmentExternalIds = batch
                .SelectMany(b => b.Apartments)
                .Select(a => a.ExternalId).Distinct().ToList();

            var existingApartments = await dbContext.Apartments
                .Where(a => apartmentExternalIds.Contains(a.ExternalId)).ToDictionaryAsync(a => a.ExternalId!, ct);

            foreach (var envelope in batch)
            {
                var host = existingHosts[envelope.Host.ExternalId];

                foreach (var aptDto in envelope.Apartments)
                {
                    if (!existingApartments.TryGetValue(aptDto.ExternalId, out var apt))
                    {
                        apt = new Apartment
                        {
                            Id = Guid.NewGuid(),
                            ExternalId = aptDto.ExternalId,
                            HostId = host.Id,       
                            Title = aptDto.Title,
                            Description = aptDto.Description,
                            Address = aptDto.Address,
                            PricePerNight = aptDto.PricePerNight,
                            IsAvailable = aptDto.IsAvailable,
                            CreatedAt = aptDto.CreatedAt,
                            UpdatedAt = aptDto.UpdatedAt,
                            DeletedAt = aptDto.DeletedAt
                        };
                        
                        dbContext.Apartments.Add(apt);
                        existingApartments[aptDto.ExternalId] = apt;
                    }
                    else
                    {
                        apt.HostId = host.Id;
                        apt.Title = aptDto.Title;
                        apt.Description = aptDto.Description;
                        apt.Address = aptDto.Address;
                        apt.PricePerNight = aptDto.PricePerNight;
                        apt.IsAvailable = aptDto.IsAvailable;
                        apt.UpdatedAt = aptDto.UpdatedAt;
                        apt.DeletedAt = aptDto.DeletedAt;
                    }

                    processed++;
                }
            }

            await dbContext.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);

            return processed;
        }
        catch
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
    }
}
