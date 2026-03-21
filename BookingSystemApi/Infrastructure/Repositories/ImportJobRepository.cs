using Application.Features.Imports.Interfaces;
using Dapper;
using Domain.Entities;
using Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Repositories;

public class ImportJobRepository(IDbConnection dbConnection) : IImportJobRepository
{
    public async Task CreateAsync(ImportJob job, CancellationToken ct = default)
    {
        await dbConnection.ExecuteAsync(
            "INSERT INTO \"ImportJobs\" (\"Id\", \"FilePath\", \"Status\", \"TotalRecords\", \"ProcessedRecords\", \"CreatedAt\") VALUES (@Id, @FilePath, @Status, @TotalRecords, @ProcessedRecords, @CreatedAt)",
            new
            {
                job.Id,
                job.FilePath,
                job.Status,
                job.TotalRecords,
                job.ProcessedRecords,
                job.CreatedAt
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<ImportJob?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await dbConnection.QueryFirstOrDefaultAsync<ImportJob>(
            "SELECT * FROM \"ImportJobs\" WHERE \"Id\" = @Id",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task<IReadOnlyList<ImportJob>> GetPendingAsync(CancellationToken ct = default)
    {
        return [..await dbConnection.QueryAsync<ImportJob>(
            "SELECT * FROM \"ImportJobs\" WHERE \"Status\" = @Status",
            new { Status = ImportStatus.Pending },
            commandType: CommandType.StoredProcedure
        )];
    }

    public async Task MarkInProgressAsync(Guid id, CancellationToken ct = default)
    {
        var job = await dbConnection.QueryFirstOrDefaultAsync<ImportJob>(
            "SELECT * FROM \"ImportJobs\" WHERE \"Id\" = @Id",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        ) ?? throw new InvalidOperationException($"Import job with ID {id} not found.");

        job.Status = ImportStatus.InProgress;
        job.StartedAt = DateTime.UtcNow;

        await dbConnection.ExecuteAsync(
            "UPDATE \"ImportJobs\" SET \"Status\" = @Status, \"StartedAt\" = @StartedAt WHERE \"Id\" = @Id",
            new
            {
                job.Status,
                job.StartedAt,
                job.Id
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task MarkCompletedAsync(Guid id, CancellationToken ct = default)
    {
        var job = await dbConnection.QueryFirstOrDefaultAsync<ImportJob>(
            "SELECT * FROM \"ImportJobs\" WHERE \"Id\" = @Id",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        ) ?? throw new InvalidOperationException($"Import job with ID {id} not found.");

        job.Status = ImportStatus.Completed;
        job.CompletedAt = DateTime.UtcNow;

        await dbConnection.ExecuteAsync(
            "UPDATE \"ImportJobs\" SET \"Status\" = @Status, \"CompletedAt\" = @CompletedAt WHERE \"Id\" = @Id",
            new
            {
                job.Status,
                job.CompletedAt,
                job.Id
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task MarkFailedAsync(Guid id, string errorMessage, CancellationToken ct = default)
    {
        var job = await dbConnection.QueryFirstOrDefaultAsync<ImportJob>(
            "SELECT * FROM \"ImportJobs\" WHERE \"Id\" = @Id",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        ) ?? throw new InvalidOperationException($"Import job with ID {id} not found.");

        job.Status = ImportStatus.Failed;
        job.ErrorMessage = errorMessage;

        await dbConnection.ExecuteAsync(
            "UPDATE \"ImportJobs\" SET \"Status\" = @Status, \"ErrorMessage\" = @ErrorMessage WHERE \"Id\" = @Id",
            new
            {
                job.Status,
                job.ErrorMessage,
                job.Id
            },
            commandType: CommandType.StoredProcedure
        );
    }

    public async Task IncrementProcessedAsync(Guid id, int count, CancellationToken ct = default)
    {
        var job = await dbConnection.QueryFirstOrDefaultAsync<ImportJob>(
            "SELECT * FROM \"ImportJobs\" WHERE \"Id\" = @Id",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        ) ?? throw new InvalidOperationException($"Import job with ID {id} not found.");

        job.ProcessedRecords += count;

        await dbConnection.ExecuteAsync(
            "UPDATE \"ImportJobs\" SET \"ProcessedRecords\" = @ProcessedRecords WHERE \"Id\" = @Id",
            new
            {
                job.ProcessedRecords,
                job.Id
            },
            commandType: CommandType.StoredProcedure
        );
    }
}
