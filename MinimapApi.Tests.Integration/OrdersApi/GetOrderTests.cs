using System.Net;
using FluentAssertions;
using MinimapApi.Endpoints;

namespace MinimapApi.Tests.Integration.OrdersApi;

public sealed class GetOrderTests : BaseOrdersApiTests
{
    public GetOrderTests(MinimalApiFactory factory) : base(factory)
    { }

    [Fact]
    public async Task Get_ReturnsWhatever_WhenWhenever()
    {
        var uri = ApiEndpoints.Orders.GetById.Replace("{id:guid}", Guid.NewGuid().ToString());
        var response = await httpClient.GetAsync(uri);
        var content = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content.Should().Be("done");
    }
}