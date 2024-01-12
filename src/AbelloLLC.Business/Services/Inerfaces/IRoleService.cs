using AbelloLLC.Business.DTOs.RolesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Services.Inerfaces
{
    public interface IRoleService
    {

       Task<List<GetRoleForUserActions>> GetAllRolesForUserActionsAsync();
    }
}
