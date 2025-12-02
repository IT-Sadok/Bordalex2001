using Application.Common.Mediator.Interfaces;

namespace Application.Common.Mediator;

public class RequestExecutor(IServiceProvider serviceProvider)
{
    public async Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, CancellationToken ct = default)
        where TRequest : IRequest<TResult>
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        var handlerObj = serviceProvider.GetService(typeof(IRequestHandler<TRequest, TResult>));
        if (handlerObj is not IRequestHandler<TRequest, TResult> handler)
        {
            throw new InvalidOperationException($"No handler found for request of type {typeof(TRequest).FullName}");
        }
        return await handler.HandleAsync(request, ct);
    }
}
