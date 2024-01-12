using AbelloLLC.Business.DTOs.AuthDTO;
using AbelloLLC.Business.Exceptions.LoginExceptions;
using AbelloLLC.Business.HelperServices.Interfaces;
using AbelloLLC.Business.Services.Inerfaces;
using AbelloLLC.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<TokenResponseDTO> LoginAsync(LoginDTO loginDTO)
        {

            var user = await _userManager.FindByNameAsync(loginDTO.UserName);
            if(user is null)
            {
                throw new SignInFailException("Username or password incorrect");
            }
            if(!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
            {
                throw new SignInFailException("Username or password incorrect");
            }
            if (!user.IsActive)
            {
                throw new UserIsNotActiveException("Username or password is incorrect");
            }
           var result =  await  _signInManager.PasswordSignInAsync(user,loginDTO.Password,false,false);
            if(!result.Succeeded)
            {
                throw new SignInFailException("Username or password incorrect");
            }
            var token = await _tokenService.GenerateJTWTokenAsync(user);

            return token;


        }
    }
}
