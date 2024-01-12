using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.DTOs.UserDTO
{
    public class ChangePasswordDTO
    {
        [DataType(DataType.Password)]
        public string oldPassword {  get; set; }
        [DataType(DataType.Password)]
        public string newPassword { get; set; }
        [DataType(DataType.Password),Compare(nameof(newPassword))]
        public string confirmPassword { get; set; }
    }
}
