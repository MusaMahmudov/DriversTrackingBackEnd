using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.DTOs.UserDTO
{
    public class PutUserDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
        public List<string> RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}
