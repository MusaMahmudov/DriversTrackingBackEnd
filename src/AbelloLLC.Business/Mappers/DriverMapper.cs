using AbelloLLC.Business.DTOs.DriverDTO;
using AbelloLLC.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Mappers
{
    public class DriverMapper : Profile
    { 
        public DriverMapper() 
        {
        CreateMap<Driver, GetDriverDTO>().ForMember(gd => gd.reservedAgo, x => x.MapFrom(d => DateTime.UtcNow - d.ReservedAt)).ReverseMap();
            CreateMap<Driver, GetDriverForDriversListDTO>().ReverseMap();

            CreateMap<Driver, GetDriverForDriverAndMapPageDTO>().ForMember(gd=>gd.reservedAgo,x=>x.MapFrom(d=>  DateTime.UtcNow - d.ReservedAt)).ReverseMap();

            CreateMap<PostDriverDTO, Driver>().ForMember(d=>d.ReservedBy,x=>x.Ignore()).ForMember(d=>d.ReservedAt,x=>x.Ignore()).ReverseMap();
            CreateMap<PutDriverDTO, Driver>().ForMember(d => d.ReservedBy, x => x.Ignore()).ForMember(d => d.ReservedAt, x => x.Ignore()).ReverseMap();
            CreateMap<Driver,GetDriverForVehicleTypeDTO>().ReverseMap();
            CreateMap<Driver,GetDriverForUpdateDTO>().ReverseMap();
            CreateMap<ReserveDriverMapPageDTO,Driver>().ReverseMap();
            
         }
    }
}
