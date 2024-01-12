using AbelloLLC.Business.DTOs.DriverDTO;
using AbelloLLC.Core.Entities;
using AbelloLLC.DataAccess.Repositories.Implementations;
using AbelloLLC.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Services.Inerfaces
{
    public interface IDriverService
    {
        Task<List<GetDriverForDriversListDTO>> GetDriversAsync(string? search);
        Task<GetDriverDTO> GetDriverByIdAsync(Guid Id);
        Task<GetDriverForUpdateDTO> GetDriverByIdForUpdateAsync(Guid Id);

        Task<List<GetDriverForDriverAndMapPageDTO>> GetDriversForMapPageAsync(string? search);
        Task ReservDriverMapPageAsync(Guid Id,ReserveDriverMapPageDTO reserveDriverMapPageDTO);
        Task CreateDriverAsync(PostDriverDTO postDriver);
        Task UpdateDriverAsync(Guid Id,PutDriverDTO putDriverDTO);
        Task DeleteDriverAsync(Guid Id);

    }
}
