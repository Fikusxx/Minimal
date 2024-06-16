using System.Net;
using FluentAssertions;
using MinimapApi.Endpoints;

namespace MinimapApi.Tests.Integration.OrdersApi;

public sealed class PlaceOrderTests : BaseOrdersApiTests
{
    public PlaceOrderTests(MinimalApiFactory factory) : base(factory)
    { }

    [Fact]
    public async Task Post_ReturnsWhatever_WhenWhenever()
    {
        var response = await httpClient.PostAsync(ApiEndpoints.Orders.Place, null);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
    
    [Fact]
    public async Task Post_ReturnsWhatever_When2_0()
    {
        var response = await httpClient.PostAsync(requestUri: $"{ApiEndpoints.Orders.Place}?api-version=2.0", null);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}