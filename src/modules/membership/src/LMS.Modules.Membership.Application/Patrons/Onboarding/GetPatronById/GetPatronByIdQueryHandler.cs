using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GetPatronById;

public sealed class GetPatronByIdQueryHandler : IQueryHandler<Guid, Result<GetPatronByIdQueryResponse>>
{
    private readonly IPatronRepository _patronRepository;
    public GetPatronByIdQueryHandler(IPatronRepository patronRepository)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
    }
    public async Task<Result<GetPatronByIdQueryResponse>> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        Patron? patron = await _patronRepository.GetByIdAsync(id, cancellationToken);

        if (patron is null)
        {
            Error error = Error.NotFound("Membership.NotFound", $"The patron with id {id.ToString()} was not found.");
            return Result.Failure<GetPatronByIdQueryResponse>(error);
        }

        GetPatronByIdQueryResponse response = new(
            Id: id,
            Name: patron.Name.Value,
            Gender: patron.Gender.Value,
            DateOfBirth: patron.DateOfBirth.Value,
            Email: patron.Email.Value,
            NationalId: patron.NationalId.Value,
            PatronType: patron.PatronType.Name
            );

        return Result.Success(response);
    }
}
