using System.Text.Json;
using MinimapApi.Endpoints.Logging;
using Serilog;

namespace MinimapApi;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddLogging(this IHostApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        if (builder.Environment.IsDevelopment())
        {
            builder.Logging.AddJsonConsole(x =>
            {
                x.IncludeScopes = true;
                x.JsonWriterOptions = new JsonWriterOptions { Indented = true };
            });
        }
        else
        {
            // add provider
        }

        return builder;
    }

    public static void AddSerilogLogging(this WebApplicationBuilder app)
    {
        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .ReadFrom.Configuration(app.Configuration)
            .Destructure.ByTransforming<MyClass>(x => MyClass.TransformToLog(x))
            .CreateLogger();

        Log.Logger = logger;
        
        app.Host.UseSerilog(logger);
    }
}