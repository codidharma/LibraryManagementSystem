namespace LMS.Common.Application.Dispatchers;

public interface ICommandDispatcher
{
    Task<TCommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken);
}
