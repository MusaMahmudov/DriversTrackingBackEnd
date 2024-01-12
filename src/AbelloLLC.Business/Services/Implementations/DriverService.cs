using AbelloLLC.Business.DTOs.DriverDTO;
using AbelloLLC.Business.Exceptions.DriverExceptions;
using AbelloLLC.Business.Exceptions.UserExceptions;
using AbelloLLC.Business.Exceptions.VehicleTypeExceptions;
using AbelloLLC.Business.Services.Inerfaces;
using AbelloLLC.Core.Entities;
using AbelloLLC.Core.Entities.Identity;
using AbelloLLC.DataAccess.Contexts;
using AbelloLLC.DataAccess.Enums;
using AbelloLLC.DataAccess.Repositories.Implementations;
using AbelloLLC.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Services.Implementations
{
    public class DriverService :  IDriverService
    {
        
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<AppUser> _userManager;
        
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        public DriverService(UserManager<AppUser> userManager,IHttpContextAccessor httpContext,IMapper mapper,IDriverRepository driverRepository,IVehicleTypeRepository vehicleTypeRepository ) 
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _vehicleTypeRepository = vehicleTypeRepository;
            _mapper =mapper;
            _driverRepository = driverRepository;
        }
        public async Task<List<GetDriverForDriversListDTO>> GetDriversAsync(string? search)
        {
          var drivers =  await _driverRepository.GetFiltered(dr=> search != null ? dr.Name.Contains(search) : true,"VehicleType").ToListAsync();
            var driversDTO =_mapper.Map<List<GetDriverForDriversListDTO>>(drivers);

            return driversDTO;
        }
        public async Task<GetDriverDTO> GetDriverByIdAsync(Guid Id)
        {
            var driver = await _driverRepository.GetSingleAsync(d=>d.Id == Id,"VehicleType");
            if (driver is null)
            {
                throw new DriverNotFoundByIdException("Driver not found");
            }
            var driverDTO = _mapper.Map<GetDriverDTO>(driver);
            return driverDTO;
        }

      
        public async Task CreateDriverAsync(PostDriverDTO postDriver)
        {
            var identity = _httpContext.HttpContext.User.Identity;
            if(!await _vehicleTypeRepository.IsExistsAsync(vt=>vt.Id == postDriver.VehicleTypeId))
            {
                throw new VehicleTypeNotFoundByIdException("Vehicle's type not found");
            }
            
            var newDriver = _mapper.Map<Driver>(postDriver);

            if(postDriver.isReserved)
            {
                newDriver.ReservedBy = identity.Name;
                newDriver.ReservedAt = DateTime.UtcNow;
            }
          
                await _driverRepository.CreateAsync(newDriver);
                await _driverRepository.SaveChangesAsync();


            
        }

        public async Task UpdateDriverAsync(Guid Id,PutDriverDTO putDriverDTO)
        {

            var driver =await _driverRepository.GetSingleAsync(d=>d.Id == Id);
            if(driver is null)
            {
                throw new DriverNotFoundByIdException("Driver not found");
            }
            if(!await _vehicleTypeRepository.IsExistsAsync(vt=>vt.Id == putDriverDTO.VehicleTypeId))
            {
                throw new VehicleTypeNotFoundByIdException("Vehicle's Type Not Found");
            }
            var identity = _httpContext.HttpContext.User.Identity;

            
            if(putDriverDTO.isReserved != driver.isReserved) 
            {
                if (putDriverDTO.isReserved)
                {


                    if (identity is not null)
                    {
                        driver.ReservedBy = identity.Name;

                    }

                    driver.ReservedAt = DateTime.UtcNow;
                }
                else
                {
                    var user = await _userManager.FindByNameAsync(identity.Name);
                    if (user is null)
                    {
                        throw new UserNotFoundByIdException("User not found");
                    }
                    var roles = await _userManager.GetRolesAsync(user);
                    if (driver.ReservedBy == identity.Name ||  roles.Any(r=>r == Roles.Admin.ToString()))
                    {
                        driver.ReservedBy = null;
                        driver.ReservedAt = null;
                    }
                    else
                    {
                        throw new DriverReservedByAnotherUserException("Driver Reserved By Another User");
                    }

                }
            }
            driver = _mapper.Map(putDriverDTO, driver);


            _driverRepository.Update(driver);
           await _driverRepository.SaveChangesAsync();


        }

        public async Task DeleteDriverAsync(Guid Id)
        {
            var driver = await _driverRepository.GetSingleAsync(d => d.Id == Id);
            if (driver is null)
            {
                throw new DriverNotFoundByIdException("Driver not found");
            }
            _driverRepository.Delete(driver);
          await  _driverRepository.SaveChangesAsync();

        }

        public async Task<GetDriverForUpdateDTO> GetDriverByIdForUpdateAsync(Guid Id)
        {
            var driver = await  _driverRepository.GetSingleAsync(d=>d.Id == Id,"VehicleType");
            if(driver is null)
            {
                throw new DriverNotFoundByIdException("Driver not found");
            }
            var driverDTO = _mapper.Map<GetDriverForUpdateDTO>(driver);
            return driverDTO;

        }

        public async Task<List<GetDriverForDriverAndMapPageDTO>> GetDriversForMapPageAsync(string? search)
        {
            var drivers = await _driverRepository.GetFiltered(d => d.IsActive == true,"VehicleType").ToListAsync();
            var driversDTO = _mapper.Map<List<GetDriverForDriverAndMapPageDTO>>(drivers);
            return driversDTO;
        }

        public async Task ReservDriverMapPageAsync(Guid Id,ReserveDriverMapPageDTO reserveDriverMapPageDTO)
        {
            var identity = _httpContext.HttpContext.User.Identity;
            var user = await _userManager.FindByNameAsync(identity.Name);
            if(user is null)
            {
                throw new UserNotFoundByIdException("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var driver = await _driverRepository.GetSingleAsync(d => d.Id == Id);
            if(driver is null)
            {
                throw new DriverNotFoundByIdException("Driver not found");
            }
            if(driver.isReserved && driver.ReservedBy != identity.Name )
            {
                if (!roles.Any(r => r == Roles.Admin.ToString()))
                {
                    throw new DriverAlreadyReservedException("Driver already reserved");
                }
            }


            driver = _mapper.Map(reserveDriverMapPageDTO, driver);
            if (reserveDriverMapPageDTO.isReserved)
            {
                driver.ReservedBy = identity.Name;
                driver.ReservedAt = DateTime.UtcNow;
            }
            else
            {
                
                driver.ReservedBy = null;
                driver.ReservedAt = null;
            }
            _driverRepository.Update(driver);
           await  _driverRepository.SaveChangesAsync();

        }
    }
}
