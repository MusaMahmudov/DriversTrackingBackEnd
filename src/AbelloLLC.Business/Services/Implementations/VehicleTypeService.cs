using AbelloLLC.Business.DTOs.VehicleTypeDTO;
using AbelloLLC.Business.Exceptions.VehicleTypeExceptions;
using AbelloLLC.Business.Services.Inerfaces;
using AbelloLLC.Core.Entities;
using AbelloLLC.DataAccess.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Services.Implementations
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly IMapper _mapper;
        public VehicleTypeService(IVehicleTypeRepository vehicleTypeRepository,IMapper mapper)
        {
            _mapper = mapper;
            _vehicleTypeRepository = vehicleTypeRepository;
        }

   

        public async Task<List<GetVehicleTypeDTO>> GetVehicleTypesAsync(string? search)
        {
           var vehicleTypes = await _vehicleTypeRepository.GetFiltered(vh=> search != null ? vh.Name.Contains(search) : true,"Drivers").ToListAsync();
            var vehicleTypesDTO = _mapper.Map<List<GetVehicleTypeDTO>>(vehicleTypes);
            return vehicleTypesDTO;

        }
        public async Task<GetVehicleTypeForUpdateDTO> GetAllVehicleTypeForUpdateAsync(Guid Id)
        {
            var vehicleType = await _vehicleTypeRepository.GetSingleAsync(vt=>vt.Id == Id);
            if(vehicleType is null)
            {
                throw new VehicleTypeNotFoundByIdException("VehicleType not found");
            }
            var vehicleTypeDTO = _mapper.Map<GetVehicleTypeForUpdateDTO>(vehicleType);
            return vehicleTypeDTO;
        }
        public async Task CreateVehicleTypeAsync(PostVehicleTypeDTO postVehicleTypeDTO)
        {
            var newVehicleType = _mapper.Map<VehicleType>(postVehicleTypeDTO);
           await _vehicleTypeRepository.CreateAsync(newVehicleType);
          await  _vehicleTypeRepository.SaveChangesAsync();

        }

        public async Task<GetVehicleTypeDTO> GetVehicleTypeByIdAsync(Guid Id)
        {
            var vehicheType = await _vehicleTypeRepository.GetSingleAsync(vt=>vt.Id == Id,"Drivers");
            if(vehicheType is null)
            {
                throw new VehicleTypeNotFoundByIdException("Vehicle's Type Not Found");
            }
            var vehicleTypeDTO = _mapper.Map<GetVehicleTypeDTO>(vehicheType);
            return vehicleTypeDTO;
        }

        public async Task UpdateVehicleTypeAsync(Guid Id, PutVehicleTypeDTO putVehicleTypeDTO)
        {
            var vehicheType = await _vehicleTypeRepository.GetSingleAsync(vt => vt.Id == Id);
            if (vehicheType is null)
            {
                throw new VehicleTypeNotFoundByIdException("Vehicle's Type Not Found");
            }
            vehicheType = _mapper.Map(putVehicleTypeDTO, vehicheType);
            _vehicleTypeRepository.Update(vehicheType);
           await _vehicleTypeRepository.SaveChangesAsync();
        }

        public async Task DeleteVehicleTypeAsync(Guid Id)
        {
            var vehicheType = await _vehicleTypeRepository.GetSingleAsync(vt => vt.Id == Id);
            if (vehicheType is null)
            {
                throw new VehicleTypeNotFoundByIdException("Vehicle's Type Not Found");
            }
            _vehicleTypeRepository.Delete(vehicheType);
          await  _vehicleTypeRepository.SaveChangesAsync();
        }

        public async Task<List<GetVehicleTypeForDriverDTO>> GetAllVehicleTypesForDriverAsync()
        {
           var vehicleTypes = await  _vehicleTypeRepository.GetAll().ToListAsync();
            var vehicleTypesDTO = _mapper.Map<List<GetVehicleTypeForDriverDTO>>(vehicleTypes);
            return vehicleTypesDTO;
        }

     
    }
}
