using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("login")] // POST : /api/auth/login
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await serviceManager.authService.LoginAsync(loginDto);
            return Ok(result);
        }


        [HttpPost("register")] // POST : /api/auth/register
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await serviceManager.authService.LoginAsync(registerDto);
            return Ok(result);
        }
    }
}
