using Application.Exports.DTOs;
using Application.Interfaces;
using System.Text.Json;

namespace Infrastructure.Exports;

public class CompanyDataMigrationService(IHostRepository hostRepository) : ICompanyDataMigrationService
{
    public async Task ExportAsync(Stream output, CancellationToken ct = default)
    {
        await using var writer = new Utf8JsonWriter(output, new JsonWriterOptions
        { 
            Indented = true 
        });

        writer.WriteStartObject();

        writer.WriteString("version", "1.0");
        writer.WriteString("exportedAt", DateTime.UtcNow);

        writer.WritePropertyName("hosts");
        writer.WriteStartArray();

        await foreach (var host in hostRepository.StreamHostsAsync(ct))
        {
            WriteHost(writer, host);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();

        await writer.FlushAsync(ct);
    }

    private static void WriteHost(Utf8JsonWriter writer, HostExportDto host)
    {
        writer.WriteStartObject();

        writer.WriteString("externalId", host.ExternalId);
        writer.WriteString("email", host.Email);
        writer.WriteString("displayName", host.DisplayName);
        writer.WriteString("createdAt", host.CreatedAt);
        writer.WriteString("updatedAt", host.UpdatedAt);
        if (host.DeletedAt.HasValue)
        {
            writer.WriteString("deletedAt", host.DeletedAt.Value);
        }
        else
        {
            writer.WriteNull("deletedAt");
        }

        writer.WritePropertyName("apartments");
        writer.WriteStartArray();

        foreach (var apartment in host.Apartments)
        {
            writer.WriteStartObject();
            writer.WriteString("externalId", apartment.ExternalId);
            writer.WriteString("title", apartment.Title);
            writer.WriteString("description", apartment.Description);
            writer.WriteString("address", apartment.Address);
            writer.WriteNumber("pricePerNight", apartment.PricePerNight);
            writer.WriteBoolean("isAvailable", apartment.IsAvailable);
            writer.WriteString("createdAt", apartment.CreatedAt);
            writer.WriteString("updatedAt", apartment.UpdatedAt);
            if (apartment.DeletedAt.HasValue)
            {
                writer.WriteString("deletedAt", apartment.DeletedAt.Value);
            }
            else
            {
                writer.WriteNull("deletedAt");
            }
            writer.WriteEndObject();
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}
