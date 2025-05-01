using LMS.Modules.Membership.Application.Patrons.Onboarding.AddAddress;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.AddAddress;

internal static class MappingExtensions
{
    public static AddAddressCommand ToCommand(this Request request, Guid patronId)
    {
        AddAddressCommand command = new(
            patronId,
            request.BuildingNumber,
            request.StreetName,
            request.City,
            request.State,
            request.Country,
            request.ZipCode
            );

        return command;
    }
}
