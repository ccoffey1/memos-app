using JWTTest.Contracts;
using JWTTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JWTTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserDto login)
        {
            IActionResult response = Unauthorized();

            string token = await _authenticationService.AuthenticateUserAsync(login);

            if (token != null)
            {
                response = Ok(new[] { token });
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDto login)
        {
            await _authenticationService.RegisterUserAsync(login);
            return Ok();
        }
    }
}
