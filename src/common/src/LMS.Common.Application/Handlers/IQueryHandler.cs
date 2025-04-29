namespace LMS.Common.Application.Handlers;

public interface IQueryHandler<in TQuery, TQueryResult>
{
    Task<TQueryResult> HandleAsync(TQuery command, CancellationToken cancellationToken);

}
