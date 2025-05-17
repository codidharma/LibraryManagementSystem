namespace LMS.Common.Application.Dispatchers.Command;

public interface ICommandDispatcher
{
    Task<TCommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken);
}
