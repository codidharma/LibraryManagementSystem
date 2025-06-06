namespace LMS.Modules.Membership.Domain.Common;

public static class ErrorCodes
{
    private const string Module = "Membership";
    public const string InvalidDomainValue = $"{Module}.InvalidDomainValue";
    public const string NotFound = $"{Module}.NotFound";
    public const string Conflict = $"{Module}.Conflict";
    public const string Validation = $"{Module}.Validation";
}
