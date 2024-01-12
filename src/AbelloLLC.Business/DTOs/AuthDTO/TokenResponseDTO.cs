using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.DTOs.AuthDTO
{
    public class TokenResponseDTO
    {
        public string Token { get; set; }
        public DateTime _expireDate { get; set; }
        public TokenResponseDTO(string token, DateTime expireDate)
        {
            Token = token;
            _expireDate = expireDate;

        }
    }
}
