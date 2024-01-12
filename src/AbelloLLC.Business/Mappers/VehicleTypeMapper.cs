using AbelloLLC.Business.DTOs.VehicleTypeDTO;
using AbelloLLC.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Mappers
{
    public class VehicleTypeMapper : Profile
    {
        public VehicleTypeMapper() 
        {
         CreateMap<PostVehicleTypeDTO,VehicleType>().ReverseMap();
            CreateMap<VehicleType,GetVehicleTypeDTO>().ReverseMap();
            CreateMap<VehicleType, GetVehicleTypeForDriverDTO>().ReverseMap();
            CreateMap<PutVehicleTypeDTO, VehicleType>().ReverseMap();
            CreateMap<VehicleType,GetVehicleTypeForUpdateDTO>().ReverseMap();


        }

    }
}
