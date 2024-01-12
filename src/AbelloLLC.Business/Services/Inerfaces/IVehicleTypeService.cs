using AbelloLLC.Business.DTOs.VehicleTypeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Services.Inerfaces
{
    public interface IVehicleTypeService
    {
        Task<List<GetVehicleTypeDTO>> GetVehicleTypesAsync(string? search);
        Task<GetVehicleTypeDTO> GetVehicleTypeByIdAsync(Guid Id);
        Task<List<GetVehicleTypeForDriverDTO>> GetAllVehicleTypesForDriverAsync();
        Task<GetVehicleTypeForUpdateDTO> GetAllVehicleTypeForUpdateAsync(Guid Id);
        Task CreateVehicleTypeAsync(PostVehicleTypeDTO postVehicleTypeDTO);
        Task UpdateVehicleTypeAsync(Guid Id,PutVehicleTypeDTO putVehicleTypeDTO);
        Task DeleteVehicleTypeAsync(Guid Id);
    }
}
