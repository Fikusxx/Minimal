namespace MinimapApi.Endpoints.ErrorOrEndpoints;

public static class ErrorOrEndpointExtensions
{
    private const string ErrorOrTag = "ErrorOr";
    
    public static IEndpointRouteBuilder MapErrorOrEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(string.Empty);
        group.WithTags(ErrorOrTag);

        group.MapTestErrorOr();

        return app;
    }
}