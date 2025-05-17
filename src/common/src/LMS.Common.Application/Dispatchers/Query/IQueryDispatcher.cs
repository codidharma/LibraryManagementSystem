namespace LMS.Common.Application.Dispatchers.Query;

public interface IQueryDispatcher
{
    Task<TQueryResult> DispatchAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken);
}
