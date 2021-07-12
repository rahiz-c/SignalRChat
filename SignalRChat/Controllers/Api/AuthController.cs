using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SignalRChat.Models;
using SignalRChat.Services;
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
        private UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(AuthenticateRequest authenticateRequest)
        {
            var response = _userService.Authenticate(authenticateRequest);
            if(response == null)
            {
                return BadRequest(new { message = "Invalid Username or Password" });
            }

            return Ok(response);
        }
    }
}
