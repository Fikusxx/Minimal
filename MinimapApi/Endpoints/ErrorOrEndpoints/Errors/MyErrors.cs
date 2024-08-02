using ErrorOr;

namespace MinimapApi.Endpoints.ErrorOrEndpoints.Errors;

public static class MyErrors
{
    public static Error BusinessError = Error.Failure(code: "Business.SpecificFailure", description: "Explanation & how to resolve.");
    public static Error ResourceNotFoundError = Error.Failure(code: "Business.NotFound", description: "Resource not found.");
}