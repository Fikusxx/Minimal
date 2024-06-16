using Api.Endpoints;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion(1.0)]
[ApiVersion(2.0)]
public class MyController : ControllerBase
{
    [HttpGet]
    [Route(ApiEndpoints.V1.Orders.GetAll)]
    [MapToApiVersion(1.0)]
    public IActionResult Test_V1()
    {
        return Ok("Version #1");
    }
    
    [HttpGet]
    [Route(ApiEndpoints.V1.Orders.GetAll)]
    [MapToApiVersion(2.0)]
    public IActionResult Test_V2()
    {
        return Ok("Version #2");
    }
}
