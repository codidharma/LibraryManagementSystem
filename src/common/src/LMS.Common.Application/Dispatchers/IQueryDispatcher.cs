namespace LMS.Common.Application.Dispatchers;

public interface IQueryDispatcher
{
    Task<TQueryResult> DispatchAsync<TQuery, TQueryResult>(TQuery command, CancellationToken cancellationToken);
}
