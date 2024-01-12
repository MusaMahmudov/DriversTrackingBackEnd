using AbelloLLC.Business.DTOs.AuthDTO;
using AbelloLLC.Business.Services.Inerfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbelloLLC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthenticationsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var token =  await  _authService.LoginAsync(loginDTO);
            return Ok(token);
        }

    }
}
