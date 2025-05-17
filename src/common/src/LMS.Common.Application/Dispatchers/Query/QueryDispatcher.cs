using LMS.Common.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Common.Application.Dispatchers.Query;

public sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

    }
    public Task<TQueryResult> DispatchAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken)
    {
        IQueryHandler<TQuery, TQueryResult> handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
        return handler.HandleAsync(query, cancellationToken);
    }
}
