using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using Microsoft.Extensions.Logging;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GetAddressByPatronId;

public sealed class GetAddressByPatronIdQueryHandler : IQueryHandler<Guid, Result<GetAddressByPatronIdQueryResponse>>
{
    private readonly ILogger _logger;
    private readonly IPatronRepository _patronRepository;

    public GetAddressByPatronIdQueryHandler(IPatronRepository patronRepository, ILogger<GetAddressByPatronIdQueryHandler> logger)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<GetAddressByPatronIdQueryResponse>> HandleAsync(Guid patronId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting process {Name}", nameof(GetAddressByPatronIdQueryHandler));

        EntityId id = new(patronId);
        Patron? patron = await _patronRepository.GetPatronByIdAsync(id, cancellationToken);

        if (patron is null)
        {
            return Result.Failure<GetAddressByPatronIdQueryResponse>(PatronErrors.PatronNotFound(patronId));
        }

        if (patron.KycStatus == KycStatus.Pending)
        {
            return Result.Failure<GetAddressByPatronIdQueryResponse>(PatronErrors.AddressNotFound(patronId));
        }

        GetAddressByPatronIdQueryResponse queryResponse = new(
            patron.Address.BuildingNumber,
            patron.Address.Street,
            patron.Address.City,
            patron.Address.State,
            patron.Address.Country,
            patron.Address.ZipCode
            );
        return Result.Success(queryResponse);
    }

}
