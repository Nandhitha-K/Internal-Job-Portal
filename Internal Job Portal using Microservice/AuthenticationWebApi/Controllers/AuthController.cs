using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecuredWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private string GenerateToken(string userName, string role, string secretKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creadentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };
            var token = new JwtSecurityToken(

                claims: claims,
                issuer: "http://www.snrao.com",
                audience: "http://www.snrao.com",
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creadentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet]
        public ActionResult GetToken(string userName, string role, string secretKey)
        {
            string jwt = GenerateToken(userName, role, secretKey);
            return Ok(jwt);
        }
    }
}
