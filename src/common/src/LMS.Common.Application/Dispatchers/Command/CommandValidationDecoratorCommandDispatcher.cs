
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using LMS.Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LMS.Common.Application.Dispatchers.Command;

public sealed class CommandValidationDecoratorCommandDispatcher : ICommandDispatcher
{
    private readonly ICommandDispatcher _next;
    private readonly IServiceProvider _serviceProvider;

    public CommandValidationDecoratorCommandDispatcher(ICommandDispatcher next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
    public async Task<TCommandResult> DispatchAsync<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken)
    {
        IValidator<TCommand> validator = _serviceProvider.GetRequiredService<IValidator<TCommand>>();

        ILogger logger = _serviceProvider.GetRequiredService<ILogger<CommandValidationDecoratorCommandDispatcher>>();

        string commandName = typeof(TCommand).Name;

        logger.LogInformation("Starting {Command} command validation.", commandName);

        ValidationResult validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid)
        {
            return await _next.DispatchAsync<TCommand, TCommandResult>(command, cancellationToken);
        }

        logger.LogInformation("Finished {Command} command validation with status {Status}.", commandName, validationResult.IsValid);

        ValidationError validationError = GetValidationError(validationResult);

        //Determine if the type is generic result or normal result

        if (typeof(TCommandResult).IsGenericType
            && typeof(TCommandResult).GetGenericTypeDefinition() == typeof(Result<>))
        {
            Type resultType = typeof(TCommandResult).GetGenericArguments()[0];
            Type genericResultType = typeof(Result<>).MakeGenericType(resultType);

            MethodInfo[] methodInfos = typeof(Result).GetMethods();

            MethodInfo? validationErrorMethod = methodInfos
                .FirstOrDefault(x => x.ReturnType.Name == genericResultType.Name && x.Name == "Failure");

            if (validationErrorMethod is not null)
            {
                MethodInfo methodInfo = validationErrorMethod.MakeGenericMethod(resultType);
                return (TCommandResult)methodInfo.Invoke(null, [validationError]);
            }
        }
        else if (typeof(TCommandResult) == typeof(Result))
        {
            return (TCommandResult)(object)Result.Failure(validationError);
        }

        ValidationFailure[] validationFailures = validationResult.Errors.ToArray();

        throw new ValidationException(validationFailures);

    }

    private static ValidationError GetValidationError(ValidationResult validationResult)
    {
        var errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage, ErrorType.Validation)).ToList();

        ValidationError validationError = new(errors);
        return validationError;
    }
}
