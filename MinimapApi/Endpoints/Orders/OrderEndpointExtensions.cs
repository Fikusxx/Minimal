using MinimapApi.Endpoints.Internal;

namespace MinimapApi.Endpoints.Orders;

public static class OrderEndpointExtensions
{
    private const string OrderTag = "Orders";
    
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(string.Empty);
        group.WithApiVersionSet(ApiVersioning.VersionSet);
        group.WithTags(OrderTag);
        
        group.MapPlaceOrder();
        group.MapGetOrder();

        return app;
    }
}

public sealed class OrderEndpoints : IEndpoints
{
    private const string OrderTag = "Orders";
    
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(string.Empty);
        group.WithApiVersionSet(ApiVersioning.VersionSet);
        group.WithTags(OrderTag);
        
        group.MapPlaceOrder();
        group.MapGetOrder();
    }
}