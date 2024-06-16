namespace MinimapApi.Tests.Integration.OrdersApi;

[Collection(nameof(MinimalApiFixture))]
public abstract class BaseOrdersApiTests
{
    protected readonly HttpClient httpClient;

    public BaseOrdersApiTests(MinimalApiFactory factory)
    {
        this.httpClient = factory.HttpClient;
    }
}