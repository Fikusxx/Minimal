using ErrorOr;
using MinimapApi.Endpoints.ErrorOrEndpoints.Errors;

namespace MinimapApi.Endpoints.ErrorOrEndpoints;

public static class TestErrorOrEndpoint
{
    public const string Name = "TestErrorOr";

    public static IEndpointRouteBuilder MapTestErrorOr(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.ErrorOrs.Test, (int number) =>
            {
                ErrorOr<int> result = new MyService().Calc(number);

                return result.Match(Results.Ok, ApiResults.Problem);

                return result.Match(x => Results.Ok(x),
                    x => Results.BadRequest(x));
            })
            .WithName(Name);

        return app;
    }
}

public class MyService
{
    public ErrorOr<int> Calc(int number)
    {
        return number switch
        {
            0 => MyErrors.BusinessError,
            1 => MyErrors.ResourceNotFoundError,
            _ => number
        };
    }
}

// or implement conversion from errors to some common api result inside that class
public static class ApiResults
{
    public static IResult Problem(IEnumerable<Error> errors)
    {
        // any logic to handle different type of errors. Seems verbose?
        return Results.BadRequest(errors);
    }
}