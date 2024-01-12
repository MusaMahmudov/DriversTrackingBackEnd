using AbelloLLC.Business.DTOs.RolesDTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Mappers
{
    public class RoleMapper : Profile
    {
        public RoleMapper() 
        {
            CreateMap<IdentityRole, GetRoleForUserActions>();
        }
    }
}
