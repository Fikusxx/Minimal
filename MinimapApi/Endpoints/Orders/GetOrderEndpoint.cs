using Microsoft.AspNetCore.Mvc;
using MinimapApi.Services;

namespace MinimapApi.Endpoints.Orders;

public static class GetOrderEndpoint
{
    public const string Name = "GetOrder";

    public static IEndpointRouteBuilder MapGetOrder(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.Orders.GetById, (
                [FromRoute] Guid id,
                [FromServices] MyMinimapServiceXD service,
                HttpContext context,
                CancellationToken whynot) =>
            {
                service.DoWork();

                if (false)
                    return Results.NotFound();

                return Results.Text("done");
            })
            .WithName(Name);
            // .RequireAuthorization();

        return app;
    }
}