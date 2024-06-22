using Microsoft.AspNetCore.Mvc;
using MinimapApi.Services;

namespace MinimapApi.Endpoints.Orders;

public static class PlaceOrderEndpoint
{
    public const string Name = "PlaceOrder";

    private record PlaceOrderEndpointLogger;

    public static IEndpointRouteBuilder MapPlaceOrder(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.Orders.Place, (
                [FromServices] MyMinimapServiceXD service,
                [FromServices] ILogger<PlaceOrderEndpointLogger> logger,
                HttpContext context,
                CancellationToken whynot) =>
            {
                service.DoWork();
                using var scope = logger.BeginScope("{OrderId}", Guid.NewGuid());
                using var scope2 = logger.BeginScope("{Amount}", 777);
                
                try
                {
                    logger.LogInformation("Starting operation...");
                }
                finally
                {
                    logger.LogInformation("Ending operation...");
                }

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