using LMS.Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LMS.Common.Application.Dispatchers.Query;

public sealed class LoggingDecoratorQueryDispatcher : IQueryDispatcher
{
    private readonly IQueryDispatcher _next;
    private readonly IServiceProvider _serviceProvider;

    public LoggingDecoratorQueryDispatcher(IQueryDispatcher next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }
    public async Task<TQueryResult> DispatchAsync<TQuery, TQueryResult>(TQuery query, CancellationToken cancellationToken)
    {
        ILogger logger = _serviceProvider.GetRequiredService<ILogger<LoggingDecoratorQueryDispatcher>>();

        string queryName = typeof(TQuery).Name;

        logger.LogInformation("Processing query {Query}", queryName);

        TQueryResult result = await _next.DispatchAsync<TQuery, TQueryResult>(query, cancellationToken);

        if (IsQueryResultOfTypeResult<TQueryResult>())
        {
            var commandResult = result as Result;

            if (commandResult is not null)
            {
                if (commandResult.IsSuccess)
                {
                    logger.LogInformation("Processed query {Query} successfully.", queryName);
                }
                else
                {
                    logger.LogInformation("Processed query {Query} with errors.", queryName);
                }
            }
            else
            {
                throw new LmsException($"Unable to parse the {nameof(TQueryResult)} to {nameof(Result)}.");
            }
        }
        else
        {
            logger.LogInformation("Processed query {Query} successfully.", queryName);
        }
        return result;

    }

    private static bool IsQueryResultOfTypeResult<TQueryResult>()
    {
        return typeof(TQueryResult).IsGenericType
            && typeof(TQueryResult).GetGenericTypeDefinition() == typeof(Result<>) || typeof(TQueryResult) == typeof(Result);
    }
}
