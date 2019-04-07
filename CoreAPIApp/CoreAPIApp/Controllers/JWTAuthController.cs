using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CoreAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTAuthController : ControllerBase
    {
        private IConfiguration _config;

        public JWTAuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("Token")]
        public ActionResult GetToken()
        {
            //Security Key
            string securityKey = _config.GetValue<string>("securityKey");

            //symmetric security key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //signing credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //create token
            var token = new JwtSecurityToken(
                issuer: _config.GetValue<string>("issuerKey"),
                audience: _config.GetValue<string>("audienceKey"),
                expires: DateTime.Now.AddHours(1),
                signingCredentials : signingCredentials
                );


            //return token
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            //return Ok("Hello From API");
        }
    }
}