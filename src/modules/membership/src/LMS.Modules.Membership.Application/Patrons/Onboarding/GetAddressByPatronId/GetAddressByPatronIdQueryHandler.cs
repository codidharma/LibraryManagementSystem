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

        Patron? patron = await _patronRepository.GetByIdAsync(patronId, cancellationToken);

        if (patron is null)
        {
            Error error = Error.NotFound("Membership.NotFound", $"The patron with id {patronId.ToString()} was not found.");
            return Result.Failure<GetAddressByPatronIdQueryResponse>(error);
        }



        if (patron.KycStatus == KycStatus.Pending)
        {
            Error error = Error.NotFound("Membership.NotFound", $"There was no address found on the patron with id {patronId.ToString()}.");
            return Result.Failure<GetAddressByPatronIdQueryResponse>(error);
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
