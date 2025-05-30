using LMS.Modules.Membership.Application.Patrons.Onboarding.GenerateCredentials;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.GenerateCredentials;

internal static class MappingExtensions
{
    public static GenerateCredentialsCommand ToCommand(this Request request)
    {
        GenerateCredentialsCommand command = new(request.PatronId);
        return command;
    }

    public static Response ToDto(this CommandResponse commandResponse)
    {
        Response response = new(commandResponse.Email, commandResponse.TemporaryPassword);
        return response;
    }
}
