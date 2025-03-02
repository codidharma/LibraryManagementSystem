namespace LMS.Modules.IAM.Domain.Users;

public interface IUserRepository
{
    Task<Guid> InsertAsync(User user);
}
