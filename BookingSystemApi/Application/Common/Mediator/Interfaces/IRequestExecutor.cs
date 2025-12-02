namespace Application.Common.Mediator.Interfaces;

public interface IRequestExecutor
{
    Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, CancellationToken ct = default)
        where TRequest : IRequest<TResult>;
}
