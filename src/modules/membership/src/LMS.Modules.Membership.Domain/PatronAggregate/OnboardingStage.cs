using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record OnboardingStage : Enumeration
{
    public static readonly OnboardingStage PatronAdded = new(1, nameof(PatronAdded));
    public static readonly OnboardingStage AddressAdded = new(2, nameof(AddressAdded));
    public static readonly OnboardingStage DocumentAdded = new(3, nameof(DocumentAdded));
    public static readonly OnboardingStage DocumentsVerified = new(4, nameof(DocumentsVerified));
    public static readonly OnboardingStage Completed = new(5, nameof(Completed));
    private OnboardingStage(int id, string name) : base(id, name) { }
}
