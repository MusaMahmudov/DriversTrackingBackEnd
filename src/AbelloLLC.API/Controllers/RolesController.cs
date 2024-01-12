using AbelloLLC.Business.Services.Inerfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AbelloLLC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("[Action]")]
        public async Task<IActionResult> GetAllRolesForUserActions() 
        {
            var roles  = await _roleService.GetAllRolesForUserActionsAsync();
            return Ok(roles);
        }

    }
}
