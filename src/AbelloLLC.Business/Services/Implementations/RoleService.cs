using AbelloLLC.Business.DTOs.RolesDTO;
using AbelloLLC.Business.Services.Inerfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        public RoleManager<IdentityRole> _roleManager { get; set; }
        public RoleService(RoleManager<IdentityRole> roleManager,IMapper mapper) 
        {
            _mapper = mapper;
         _roleManager = roleManager;
        }
        public async Task<List<GetRoleForUserActions>> GetAllRolesForUserActionsAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var rolesDTO = _mapper.Map<List<GetRoleForUserActions>>(roles);
            return rolesDTO;
        }
    }
}
