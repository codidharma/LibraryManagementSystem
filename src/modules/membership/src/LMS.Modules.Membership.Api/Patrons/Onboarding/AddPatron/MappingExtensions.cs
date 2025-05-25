using LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.AddPatron;

internal static class MappingExtensions
{
    public static AddPatronCommand ToCommand(this Request request)
    {
        AddPatronCommand command = new(
            request.Name,
            request.Gender,
            request.DateOfBirth,
            request.Email,
            request.NationalId,
            request.PatronType);
        return command;
    }

    public static Response ToDto(this CommandResult commandResult)
    {
        Response response = new(commandResult.Id);
        return response;
    }
}
