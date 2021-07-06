using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            if (model == null) return BadRequest("invalid client request");

            if (model.UserName == "mrc" && model.Password == "test@pcp")
            {
                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("pcpSecretKey-dev-test"));
                SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(1),
                    signingCredentials: signingCredentials
                 );


                string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}
