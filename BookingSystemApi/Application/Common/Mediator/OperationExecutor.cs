using MediatR;

namespace Application.Common.Mediator;

public class OperationExecutor(IServiceProvider serviceProvider)
{
    public async Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, CancellationToken ct = default)
        where TRequest : IRequest<TResult>
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        var handlerObj = serviceProvider.GetService(typeof(IOperationHandler<TRequest, TResult>));
        if (handlerObj is not IOperationHandler<TRequest, TResult> handler)
        {
            throw new InvalidOperationException($"No handler found for request of type {typeof(TRequest).FullName}");
        }
        return await handler.HandleAsync(request, ct);
    }
}
