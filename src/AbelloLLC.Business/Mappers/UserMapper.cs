using AbelloLLC.Business.DTOs.UserDTO;
using AbelloLLC.Core.Entities.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper() 
        {
            CreateMap<AppUser, GetUserDTO>().ReverseMap();
            CreateMap<PostUserDTO,AppUser>().ReverseMap();
            CreateMap<PutUserDTO,AppUser>().ReverseMap();
            CreateMap<AppUser,GetUserForUpdateDTO>().ForMember(gu=>gu.RoleId,x=>x.Ignore()).ReverseMap();
        }
    }
}
