using LMS.Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LMS.Common.Application.Dispatchers;

public sealed class LoggingDecoratorCommandDispatcher : ICommandDispatcher
{
    private readonly ICommandDispatcher _next;
    private readonly IServiceProvider _serviceProvider;



    public LoggingDecoratorCommandDispatcher(ICommandDispatcher next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task<TCommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken)
    {
        ILogger logger = _serviceProvider.GetRequiredService<ILogger<LoggingDecoratorCommandDispatcher>>();

        logger.LogInformation("Processing command {Command}", nameof(command));

        TCommandResult result = await _next.DispatchAsync<TCommand, TCommandResult>(command, cancellationToken);

        if (IsCommandResultOfTypeResult<TCommandResult>())
        {
            Result? commandResult = result as Result;

            if (commandResult is not null)
            {
                if (commandResult.IsSuccess)
                {
                    logger.LogInformation("Processed command {Command} successfully.", nameof(command));
                }
                else
                {
                    logger.LogInformation("Processed command {Command} with errors.", nameof(command));
                }
            }
            else
            {
                throw new LmsException($"Unable to parse the {nameof(TCommandResult)} to {nameof(Result)}.");
            }
        }
        else
        {
            logger.LogInformation("Processed command {Command} successfully.", nameof(command));
        }
        return result;

    }

    private static bool IsCommandResultOfTypeResult<TCommandResult>()
    {
        return typeof(TCommandResult).IsGenericType
            && typeof(TCommandResult).GetGenericTypeDefinition() == typeof(Result<>) || typeof(TCommandResult) == typeof(Result);
    }
}
