using ProductApi.Application.Interfaces.Identity;
using ProductApi.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.VM;
namespace ProductApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authService.Login(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegisterationRequest request)
        {
            return Ok(await _authService.Register(request));
        }
        [HttpPost("verification")]
        public async Task<IActionResult> VerifyWithVerifyTokenAsync(VerificationVM verificationVM)
        {
            await _authService.Verification(verificationVM);
            return Ok("now, you can login to your account");
        }
        [HttpPost("SendANewTokenToConsole")]
        public async Task<IActionResult> SendANewTokenToConsole(AuthRequest request)
        {
            await _authService.RegenerateVerificationToken(request);
            return Ok();
        }
    }
}
