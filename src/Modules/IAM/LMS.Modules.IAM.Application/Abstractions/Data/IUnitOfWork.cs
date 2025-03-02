namespace LMS.Modules.IAM.Application.Abstractions.Data;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
