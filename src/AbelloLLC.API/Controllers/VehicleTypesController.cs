using AbelloLLC.Business.DTOs.Common;
using AbelloLLC.Business.DTOs.VehicleTypeDTO;
using AbelloLLC.Business.Services.Inerfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AbelloLLC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypesController : ControllerBase
    {
        private readonly IVehicleTypeService _vehicleTypeService;
        public VehicleTypesController(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;

        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> CreateVehicleType(PostVehicleTypeDTO postVehicleTypeDTO)
        {
            await _vehicleTypeService.CreateVehicleTypeAsync(postVehicleTypeDTO);
            return Ok("Vehicle Type Created Successefully");
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> GetAllVehicleTypes([FromQuery] string? search)
        {
            var vehicleTypes = await _vehicleTypeService.GetVehicleTypesAsync(search);
            return Ok(vehicleTypes);

        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> GetVehicleTypeById(Guid Id)
        {
          var vehicleType = await _vehicleTypeService.GetVehicleTypeByIdAsync(Id);
            return Ok(vehicleType);
        }
        [HttpGet("[Action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> GetVehicleTypeForUpdate(Guid Id)
        {
            var vehicleType = await _vehicleTypeService.GetAllVehicleTypeForUpdateAsync(Id);
            return Ok(vehicleType);
        }
        [HttpGet("[Action]")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> GetAllVehicleTypesForDriver()
        {
            var vehicleTypes = await _vehicleTypeService.GetAllVehicleTypesForDriverAsync();
            return Ok(vehicleTypes);
        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> UpdateVehicleType(Guid Id,PutVehicleTypeDTO putVehicleTypeDTO)
        {
           await _vehicleTypeService.UpdateVehicleTypeAsync(Id, putVehicleTypeDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO("Driver Udpated successefully", HttpStatusCode.OK));

        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> DeleteVehicleType(Guid Id)
        {
            await _vehicleTypeService.DeleteVehicleTypeAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO("Driver Deleted successefully", HttpStatusCode.OK));

        }

    }
}
