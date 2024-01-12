using AbelloLLC.Business.DTOs.Common;
using AbelloLLC.Business.DTOs.UserDTO;
using AbelloLLC.Business.Exceptions.UserExceptions;
using AbelloLLC.Business.HelperServices.Interfaces;
using AbelloLLC.Business.Services.Inerfaces;
using AbelloLLC.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AbelloLLC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UsersController(IMailService mailService,UserManager<AppUser> userManager,IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _mailService = mailService;
            _userManager = userManager;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);

        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetUserById(string Id)
        {
         var user =  await _userService.GetUserByIdAsync(Id);
            return Ok(user);
        }
        [HttpGet("[Action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetUserByIdForUpdate(string Id)
        {
            var user = await _userService.GetUserByIdForUpdateAsync(Id);
            return Ok(user);
        }
        [HttpGet("[Action]/{Id}")]
     

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateUser(PostUserDTO postUserDTO)
        {
            await _userService.CreateUserAsync(postUserDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO("User Created successefully", HttpStatusCode.OK));

        }
        [HttpPut("[Action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Moderator,Employee")]
        public async Task<IActionResult> ChangePassword(string Id,ChangePasswordDTO changePasswordDTO)
        {
            await _userService.ChangePasswordAsync(Id,changePasswordDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO("Password changed successefully", HttpStatusCode.OK));

        }
        [HttpPut("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string Id,PutUserDTO putUserDTO) 
        {
           await _userService.UpdateUserAsync(Id,putUserDTO);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO("User Updated successefully", HttpStatusCode.OK));

        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string Id)
        {
           await _userService.DeleteUserAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDTO("User Deleted successefully", HttpStatusCode.OK));

        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
            if(user is null)
            {
                throw new UserNotFoundByEmailException("User Not Found");
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath,"assets","templates","reset-password.html");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = System.Web.HttpUtility.UrlEncode(token);
            var link = $"http://localhost:3000/ResetPassword?token={token}&email={forgotPasswordDTO.Email}";
            StreamReader str = new StreamReader(path);
            var result = await str.ReadToEndAsync();
            var body = result.Replace("[link]",link);
            MailRequestDTO mailRequest = new MailRequestDTO()
            {
                ToEmail = forgotPasswordDTO.Email,
                Subject = "Reset Password",
                Body = body
            };
              await _mailService.SendEmail(mailRequest);

            return Ok("Please check your email");





        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            if (string.IsNullOrEmpty(resetPasswordDTO.email) || string.IsNullOrEmpty(resetPasswordDTO.token))
            {
                return BadRequest("Error");
            }
            
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.email);
            if(user is null)
            {
                throw new UserNotFoundByEmailException("User not found by email");
            }
           var result = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.token, resetPasswordDTO.Password);
            if (!result.Succeeded)
            {
                throw new ChangePasswordFailException(result.Errors);
            }

            return Ok("Password changed successefully");

        }
    }
}
