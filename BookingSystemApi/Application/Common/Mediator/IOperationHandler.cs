using MediatR;

namespace Application.Common.Mediator;

public interface IOperationHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    Task<TResult> HandleAsync(TRequest request, CancellationToken ct = default);
}
