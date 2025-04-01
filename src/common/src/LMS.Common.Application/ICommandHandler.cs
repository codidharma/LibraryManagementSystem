namespace LMS.Common.Application;

public interface ICommandHandler<in TCommand, TCommandResult>
{
    Task<TCommandResult> HandleAsync(TCommand command, CancellationToken cancellationToken);

}
