namespace LMS.Common.Application;

public interface ICommandDispatcher
{
    Task<TCommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken);
}
