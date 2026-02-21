using Application.Imports.Interfaces;
using Application.Imports.Models.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Imports.Processing;

public sealed class ImportBatchProcessor(AppDbContext dbContext) : IImportBatchProcessor
{
    public async Task<int> ProcessBatchAsync(Guid jobId, IReadOnlyCollection<ImportEnvelopeDto> batch, CancellationToken ct = default)
    {
        if (batch.Count == 0)
            return 0;

        await using var tx = await dbContext.Database.BeginTransactionAsync(ct);

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
                    };

                    dbContext.Users.Add(host);
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
                .Where(a => apartmentExternalIds.Contains(a.ExternalId)).ToDictionaryAsync(a => a.ExternalId, ct);

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
                            HostId = Guid.Parse(host.Id),
                            Title = aptDto.Title,
                            Description = aptDto.Description,
                            Address = aptDto.Address,
                            PricePerNight = aptDto.PricePerNight,
                            IsAvailable = aptDto.IsAvailable,
                            CreatedAt = aptDto.CreatedAt,
                            UpdatedAt = aptDto.UpdatedAt
                        };
                        
                        dbContext.Apartments.Add(apt);
                        existingApartments[aptDto.ExternalId] = apt;
                    }
                    else
                    {
                        apt.HostId = Guid.Parse(host.Id);
                        apt.Title = aptDto.Title;
                        apt.Description = aptDto.Description;
                        apt.Address = aptDto.Address;
                        apt.PricePerNight = aptDto.PricePerNight;
                        apt.IsAvailable = aptDto.IsAvailable;
                        apt.UpdatedAt = aptDto.UpdatedAt;
                    }

                    processed++;
                }
            }

            await dbContext.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);

            return processed;
        }
        catch
        {
            await tx.RollbackAsync(ct);
            throw;
        }
    }
}
