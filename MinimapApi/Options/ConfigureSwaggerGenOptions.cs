using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MinimapApi.Options;

public sealed class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;
    private readonly IHostEnvironment environment;

    public ConfigureSwaggerGenOptions(IHostEnvironment environment, IApiVersionDescriptionProvider provider)
    {
        this.environment = environment;
        this.provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var desc in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                desc.GroupName,
                new OpenApiInfo()
                {
                    Title = environment.EnvironmentName,
                    Version = desc.ApiVersion.ToString()
                });
        }

        // Auth options for Swagger
        options.AddSecurityDefinition($"Bearer",
            new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Name = "Authorization",
                Description = "Authorization Token"
            });

        var apiSecurityRequirement = new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                Array.Empty<string>()
            }
        };

        options.AddSecurityRequirement(apiSecurityRequirement);
    }
}