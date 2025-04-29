
using LMS.Common.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace LMS.Common.Application.Dispatchers;

public sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

    }
    public Task<TCommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken)
    {
        ICommandHandler<TCommand, TCommandResult> handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TCommandResult>>();
        return handler.HandleAsync(command, cancellationToken);
    }
}
