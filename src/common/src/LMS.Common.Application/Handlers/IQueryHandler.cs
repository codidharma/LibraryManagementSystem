namespace LMS.Common.Application.Handlers;

public interface IQueryHandler<in TQuery, TQueryResult>
{
    Task<TQueryResult> HandleAsync(TQuery query, CancellationToken cancellationToken);

}
