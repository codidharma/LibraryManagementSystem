using LMS.Modules.Membership.Application.Patrons.UpdateInformation.UpdatePatron;

namespace LMS.Modules.Membership.Api.Patrons.UpdateInformation.UpdatePatron;

internal static class MappingExtensions
{
    public static UpdatePatronCommand ToCommand(this Request request, Guid patronId)
    {
        UpdatePatronCommand command = new(patronId, request.Name, request.Email);
        return command;
    }
}
