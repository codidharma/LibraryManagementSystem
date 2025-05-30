using LMS.Common.Api.Results;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.GenerateCredentials;

internal sealed record Response(string Email, string TemporaryPassword) : BaseResponse;
