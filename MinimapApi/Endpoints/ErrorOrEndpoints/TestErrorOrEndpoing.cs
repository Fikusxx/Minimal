using ErrorOr;
using LanguageExt;
using LanguageExt.Common;
using MinimapApi.Endpoints.ErrorOrEndpoints.Errors;
using Error = ErrorOr.Error;

namespace MinimapApi.Endpoints.ErrorOrEndpoints;

public static class TestErrorOrEndpoint
{
    public const string ErrorOrName = "TestErrorOr";
    public const string LangExtName = "TestLangExt";

    public static IEndpointRouteBuilder MapTestErrorOr(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.ErrorOrs.ErrorOr, (int number) =>
            {
                ErrorOr<int> result = new MyService().Calc(number);

                return result.Match(Results.Ok, ApiResults.Problem);

                return result.Match(x => Results.Ok(x),
                    x => Results.BadRequest(x));
            })
            .WithName(ErrorOrName);
        
        app.MapGet(ApiEndpoints.ErrorOrs.LangExt, (int number) =>
            {
                var service = new MyService();
                var nullResult = service.GetNullResult();
                var exResult = service.GetExceptionResult();
                var result = service.GetResult();
                var r = result.Match(success => "ok", failure => "not ok");
                var nullOption = service.GetNullOption();
                var option = service.GetOption();

                return Results.Ok(new
                {
                    nullResult, exResult, result, nullOption, option
                });
            })
            .WithName(LangExtName);

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

    public Result<int?> GetNullResult() => null;
    public Result<int?> GetExceptionResult() => new Result<int?>(new Exception("Bad"));
    public Result<int?> GetResult() => 1;
    public Option<int?> GetNullOption() => null;
    public Option<int?> GetOption() => 1;
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