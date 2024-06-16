using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TokenController : ControllerBase
{
    private const string Secret = "MY_SUPER_SECRET_TOKEN_YAY_EXTRA_FOR_THE_LOVE_OF_GOD_PLEASE_FUCKING_WORK_FFS";
    private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(1);

    [HttpGet]
    public IActionResult GetToken()
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Secret);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenLifeTime),
            Issuer = "Issuer",
            Audience = "Audience",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = handler.CreateToken(descriptor);
        var jwt = handler.WriteToken(token);

        return Ok(jwt);
    }
}