using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Auths;
using AspNet.Tutorial.Core.Interfaces.IServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Tutorial.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            return Ok(await _authService.VerifyLogin(loginDto.Email, loginDto.Password));
        }

        [Authorize]
        [HttpGet("refresh")]
        public async Task<IActionResult> Refresh([FromHeader(Name = "Authorization")] string token)
        {
            return Ok(await _authService.RefreshToken(token));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            return Ok(await _authService.Register(userRegisterDto));
        }
    }
}
