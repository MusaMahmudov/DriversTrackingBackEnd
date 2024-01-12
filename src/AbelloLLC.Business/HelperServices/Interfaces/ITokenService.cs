using AbelloLLC.Business.DTOs.AuthDTO;
using AbelloLLC.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.HelperServices.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponseDTO> GenerateJTWTokenAsync(AppUser user);
    }
}
