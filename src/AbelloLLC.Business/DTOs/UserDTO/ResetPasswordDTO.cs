﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.DTOs.UserDTO
{
    public class ResetPasswordDTO
    {
        public string email { get; set; }
        public string token { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string confirmPassword { get; set; }
    }
}
