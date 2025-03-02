
using LMS.Modules.IAM.Application.Abstractions.Data;
using LMS.Modules.IAM.Application.Abstractions.Identity;
using LMS.Modules.IAM.Domain.Users;

namespace LMS.Modules.IAM.Application.Users.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserRequest, Guid>
{
    private readonly IIdentityService _identityService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IIdentityService idenityService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _identityService = idenityService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;

    }
    public async Task<Guid> HandleAsync(RegisterUserRequest request, CancellationToken cancellationToken)
    {

        //Create the user in the key cloak
        IdentityUser identityUser = new(request.FirstName, request.LastName, request.Email, request.Password);
        Guid identityId = await _identityService.RegisterUserAsync(identityUser, cancellationToken);

        //Create the user
        var user = User.Create(
            name: new Name(request.FirstName, request.LastName),
            email: new Email(request.Email),
            identityId: new IdentityId(identityId),
            role: new(request.role.ToString())
            );

        await _userRepository.InsertAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id.Value;
    }
}
