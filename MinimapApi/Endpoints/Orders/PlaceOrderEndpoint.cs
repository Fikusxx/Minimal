using Microsoft.AspNetCore.Mvc;
using MinimapApi.Services;

namespace MinimapApi.Endpoints.Orders;

public static class PlaceOrderEndpoint
{
    public const string Name = "PlaceOrder";

    public static IEndpointRouteBuilder MapPlaceOrder(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Orders.Place, (
                [FromServices] MyMinimapServiceXD service,
                HttpContext context,
                CancellationToken whynot) =>
            {
                service.DoWork();
                
                return TypedResults.CreatedAtRoute(new { Text = "Privet :)" }, GetOrderEndpoint.Name,
                    new { Id = Guid.NewGuid() });
            })
            .WithName($"{Name}V1")
            // .RequireAuthorization()
            .Produces<object>(StatusCodes.Status201Created)
            .HasApiVersion(1.0);
        
        app.MapPost(ApiEndpoints.Orders.Place, (
                [FromServices] MyMinimapServiceXD service,
                HttpContext context,
                CancellationToken whynot) =>
            {
                service.DoWork();
        
                return TypedResults.CreatedAtRoute(new { Text = "Privet :)" }, GetOrderEndpoint.Name,
                    new { Id = Guid.NewGuid() });
            })
            .WithName($"{Name}V2")
            .RequireAuthorization()
            .Produces<object>(StatusCodes.Status201Created)
            .HasApiVersion(2.0);

        return app;
    }
}