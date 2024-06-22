namespace MinimapApi.Endpoints.Logging;

public static class LoggingEndpointExtensions
{
    private const string LoggingTag = "Logging";
    
    public static IEndpointRouteBuilder MapLoggingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(string.Empty);
        group.WithTags(LoggingTag);

        group.MapLogging();

        return app;
    }
}