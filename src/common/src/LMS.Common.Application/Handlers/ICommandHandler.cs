namespace LMS.Common.Application.Handlers;

public interface ICommandHandler<in TCommand, TCommandResult>
{
    Task<TCommandResult> HandleAsync(TCommand command, CancellationToken cancellationToken);

}
