using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Transactions;

public class EfTransactionManager(AppDbContext dbContext) : ITransactionManager
{
    public async Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken ct = default)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async token =>
        {
            token.ThrowIfCancellationRequested();
            
            await using var transaction = await dbContext.Database.BeginTransactionAsync(token);

            try
            {
                var result = await operation(token);
                await transaction.CommitAsync(token);
                return result;
            }
            catch
            {
                await transaction.RollbackAsync(CancellationToken.None);
                dbContext.ChangeTracker.Clear();
                throw;
            }
        }, ct);
    }
}
