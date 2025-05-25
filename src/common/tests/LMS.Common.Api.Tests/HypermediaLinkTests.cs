using LMS.Common.Api.Results;

namespace LMS.Common.Api.Tests;

public class HypermediaLinkTests
{
    [Theory]
    [InlineData(HttpMethodConstants.Get)]
    [InlineData(HttpMethodConstants.Post)]
    [InlineData(HttpMethodConstants.Put)]
    [InlineData(HttpMethodConstants.Patch)]
    [InlineData(HttpMethodConstants.Delete)]
    public void New_Returns_ValidHypermediaLinkInstance(string method)
    {
        //Arrange
        string href = "http://localhost:7001/someresource/1";
        string relation = "self";


        //Act
        HypermediaLink link = new(href, relation, method);

        //Assert
        Assert.Equal(href, link.Href);
        Assert.Equal(relation, link.Rel);
        Assert.Equal(method, link.Method);
    }

    [Fact]
    public void New_Returns_ValidBaseResponseInstance()
    {
        //Arrange
        string relation = "Self";
        string href = "http://localhost:7001/someresource/1";
        string method = "GET";

        //Act
        HypermediaLink link = new(href, relation, method);
        BaseResponse baseResponse = new() { Links = [link] };


        //Assert
        Assert.NotEmpty(baseResponse.Links);
        HypermediaLink actualLink = baseResponse.Links[0];

        Assert.Equal(href, actualLink.Href);
        Assert.Equal(relation, actualLink.Rel);
        Assert.Equal(method, actualLink.Method);
    }
}
