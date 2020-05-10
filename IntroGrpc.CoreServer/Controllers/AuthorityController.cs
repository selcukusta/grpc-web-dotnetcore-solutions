using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace IntroGrpc.CoreServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorityController : Controller
    {
        private IConfiguration _config;

        public AuthorityController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var tokenString = GenerateJSONWebToken();
            return Ok(new { token = tokenString });
        }

        private string GenerateJSONWebToken()
        {
            var secretKey = _config.GetValue<string>("SecretKey");
            var issuer = _config.GetValue<string>("Issuer");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: issuer,
                audience: issuer,
                claims: null,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}