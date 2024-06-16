using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinimapApi.Endpoints;
using MinimapApi.Options;
using MinimapApi.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

#region JWT

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        "MY_SUPER_SECRET_TOKEN_YAY_EXTRA_FOR_THE_LOVE_OF_GOD_PLEASE_FUCKING_WORK_FFS")),
            ValidateIssuerSigningKey = true,
            ValidIssuer = "Issuer",
            ValidAudience = "Audience",
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true
        };
    });

#endregion


builder.Services.AddApiVersioning(x =>
    {
        x.DefaultApiVersion = new ApiVersion(1.0);
        x.AssumeDefaultVersionWhenUnspecified = true;
        x.ReportApiVersions = true;
        x.ApiVersionReader = new QueryStringApiVersionReader("api-version");
    })
    .AddApiExplorer();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
builder.Services.AddSwaggerGen(x => { x.OperationFilter<SwaggerDefaultValues>(); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddSingleton<MyMinimapServiceXD>();


var app = builder.Build();

app.CreateApiVersionSet();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    foreach (var description in app.DescribeApiVersions())
    {
        x.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
    }
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// app.UseCors();
// app.MapApiEndpoints();
app.MapEndpoints();

app.Run();