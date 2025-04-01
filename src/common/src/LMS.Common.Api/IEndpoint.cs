using Microsoft.AspNetCore.Routing;

namespace LMS.Common.Api;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
