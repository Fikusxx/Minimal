using System.Reflection;
using MinimapApi.Endpoints.Internal;
using MinimapApi.Endpoints.Orders;

namespace MinimapApi.Endpoints;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapOrderEndpoints();

        return app;
    }

    public static void MapEndpoints(this IApplicationBuilder app)
    {
        var endpointTypes = Assembly.GetExecutingAssembly().DefinedTypes
            .Where(x => x is { IsAbstract: false, IsInterface: false } && typeof(IEndpoints).IsAssignableFrom(x));

        foreach (var type in endpointTypes)
        {
            type.GetMethod(nameof(IEndpoints.MapEndpoints))!
                .Invoke(null, [app]);
        }
    }
}