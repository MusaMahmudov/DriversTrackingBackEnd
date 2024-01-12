using AbelloLLC.Business.DTOs.RolesDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.DTOs.UserDTO
{
    public class GetUserForUpdateDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public List<string>? RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
