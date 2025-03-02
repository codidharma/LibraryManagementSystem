namespace LMS.Modules.IAM.Application.Users.RegisterUser;

public interface ICommandHandler<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}
