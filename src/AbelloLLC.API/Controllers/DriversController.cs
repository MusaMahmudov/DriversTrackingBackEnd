using AbelloLLC.Business.DTOs.Common;
using AbelloLLC.Business.DTOs.DriverDTO;
using AbelloLLC.Business.Services.Inerfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AbelloLLC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer",Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> GetAllDrivers([FromQuery] string? search)
        {
            var drivers = await _driverService.GetDriversAsync(search);
            return Ok(drivers);
        }
        [HttpGet("[Action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> GetDriversForMapPage([FromQuery]string? search) 
        {
            var drivers = await _driverService.GetDriversForMapPageAsync(search);
            return Ok(drivers);
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> CreateDriver(PostDriverDTO postDriverDTO)
        {
            await _driverService.CreateDriverAsync(postDriverDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO("Driver Created successefully", HttpStatusCode.OK));

        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> GetDriverById(Guid Id)
        {
        var driver =  await  _driverService.GetDriverByIdAsync(Id);
            return Ok(driver);
        }
        [HttpGet("[Action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> GetDriverByIdForUpdate(Guid Id)
        {
            var driver = await _driverService.GetDriverByIdForUpdateAsync(Id);
            return Ok(driver);
        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> UpdateDriver(Guid Id, PutDriverDTO putDriverDTO)
        {
           await _driverService.UpdateDriverAsync(Id, putDriverDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO("Driver Update successefully", HttpStatusCode.OK));

        }
        [HttpPut("[Action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> ReserveDriverMapPage(Guid Id,ReserveDriverMapPageDTO reserveDriverMapPageDTO)
        {
           await _driverService.ReservDriverMapPageAsync(Id, reserveDriverMapPageDTO);
            return Ok("Success");
        }


        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> DeleteDriver(Guid Id)
        {
          await  _driverService.DeleteDriverAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO("Driver Deleted successefully", HttpStatusCode.OK));

        }
    }
}
