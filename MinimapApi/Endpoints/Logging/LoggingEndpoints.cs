using Microsoft.AspNetCore.Mvc;
using MinimapApi.Services;
using Serilog.Context;
using SerilogTimings;

namespace MinimapApi.Endpoints.Logging;

public static class LoggingEndpoints
{
    private static readonly Guid orderId = Guid.NewGuid();

    private static readonly Action<ILogger<LoggingEndpointLogger>, string, int, DateOnly, Exception?> logUselessInfo =
        LoggerMessage.Define<string, int, DateOnly>(LogLevel.Warning, new EventId(0, "MethodName"),
            "Customer {Name} placed order {OrderId} at {Date}");

    public static IEndpointRouteBuilder MapLogging(this IEndpointRouteBuilder app)
    {
        app.MapGet("not_serilog", (
            [FromServices] ILogger<LoggingEndpointLogger> logger) =>
        {
            using var scope = logger.BeginScope("{OrderId}", orderId);
            using var timedScope = logger.BeginTimedOperation(LogLevel.Warning, "Placing order {OrderId}", orderId);
            logger.LogWarning("And its done xd");

            return Results.Ok();
        });

        app.MapGet("serilog", (
            [FromServices] ILogger<LoggingEndpointLogger> logger) =>
        {
            using var prop = LogContext.PushProperty("OrderId", orderId);
            logger.LogWarning("And its done xd");

            return Results.Ok();
        });

        app.MapGet("serilog-transform", (
            [FromServices] ILogger<LoggingEndpointLogger> logger) =>
        {
            var secretClass = new MyClass() { Id = Guid.NewGuid(), Name = "Vasya", SuperSecretFieldDontShow = "hehe" };
            logger.LogWarning("Class with super secret fields is : {@SecretClass}", secretClass);

            return Results.Ok();
        });

        app.MapGet("serilog-timings", (
            [FromServices] ILogger<LoggingEndpointLogger> logger) =>
        {
            // change default log level to Information to make this work 
            // appends "{status} in {ms}ms" to the end of the template
            using var op = Operation.Time("Operation 1 for order with id {OrderId}", orderId);
            using var op1 = Operation.Begin("Operation 2 for order with id {OrderId}", orderId);

            logger.LogWarning("Operation is successful");
            op1.Complete();

            return Results.Ok();
        });
        
        app.MapGet("serilog-cached", (
            [FromServices] ILogger<LoggingEndpointLogger> logger) =>
        {
            logUselessInfo(logger, "Vasya", 15, DateOnly.FromDateTime(DateTime.Now), null);
            logger.LogUselessInfo("Vasya", 15, DateOnly.FromDateTime(DateTime.Now));

            return Results.Ok();
        });
            
            
        return app;
    }
}

public class MyClass
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SuperSecretFieldDontShow { get; set; }

    public static object TransformToLog(MyClass myClass)
    {
        return new { myClass.Id, myClass.Name };
    }
}

public record LoggingEndpointLogger;

public static partial class LogOrderExtensions
{
    [LoggerMessage(LogLevel.Warning, "Customer {Name} placed order {OrderId} at {Date}")]
    public static partial void LogUselessInfo(this ILogger<LoggingEndpointLogger> logger, string name, int orderId, DateOnly date);
}