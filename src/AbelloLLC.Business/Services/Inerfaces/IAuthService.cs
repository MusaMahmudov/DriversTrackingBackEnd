using AbelloLLC.Business.DTOs.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Services.Inerfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDTO> LoginAsync(LoginDTO loginDTO);
    }
}
