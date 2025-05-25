namespace LMS.Common.Api.Results;

public record BaseResponse
{
    public List<HypermediaLink> Links { get; set; }
}
