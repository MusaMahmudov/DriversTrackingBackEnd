using AbelloLLC.Business.DTOs.Common;
using AbelloLLC.Business.DTOs.RolesDTO;
using AbelloLLC.Business.DTOs.UserDTO;
using AbelloLLC.Business.Exceptions.UserExceptions;
using AbelloLLC.Business.Exceptions.UserRoleExceptions;
using AbelloLLC.Business.HelperServices.Interfaces;
using AbelloLLC.Business.Services.Inerfaces;
using AbelloLLC.Core.Entities.Identity;
using AbelloLLC.DataAccess.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailService;
        public UserService(IMailService mailService,RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager,IMapper mapper)
        {
            _mailService = mailService;
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<GetUserDTO>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersDTO = new List<GetUserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var getUserDTO = new GetUserDTO()
                {
                    RoleName = roles.ToList()
                };
                getUserDTO = _mapper.Map(user, getUserDTO);
                usersDTO.Add(getUserDTO);
            }

            return usersDTO;
        }
        public async Task<GetUserDTO> GetUserByIdAsync(string Id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (user is null)
            {
                throw new UserNotFoundByIdException("User not found");
            }
            var role = await _userManager.GetRolesAsync(user);
            var userDTO = _mapper.Map<GetUserDTO>(user);
            userDTO.RoleName = role.ToList();
            return userDTO;
        }
        public async Task<GetUserForUpdateDTO> GetUserByIdForUpdateAsync(string Id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u=>u.Id == Id);
            if(user is null)
            {
                throw new UserNotFoundByIdException("User not found");
            }
            var userDTO = _mapper.Map<GetUserForUpdateDTO>(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            List<string> rolesForUsers = new List<string>();
            foreach (var userRole in userRoles)
            {
                var role  = await _roleManager.Roles.FirstOrDefaultAsync(r=>r.Name == userRole);
                rolesForUsers.Add(role.Id);
            }
            userDTO.RoleId = rolesForUsers;



            
            return userDTO;


        }
        public async Task CreateUserAsync(PostUserDTO postUserDTO)
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if(postUserDTO.RoleId.Count == 0)
            {
                throw new UserCannotBeWithoutRoleException("User cannot be without role");
            }

            foreach (var roleId in postUserDTO.RoleId)
            {
                if(!roles.Any(r=>r.Id == roleId))
                {
                    throw new UserRoleNotFoundException("Role Not found");

                }
                if(roles.Any(r=>r.Id == roleId && r.Name == Roles.Admin.ToString()))
                {
                    throw new ThereIsCanBeOnlyOneAdminException("Only One Admin Allowed");
                }

            }

            var newUser = _mapper.Map<AppUser>(postUserDTO);
            newUser.IsActive = true;
            
            var result =   await _userManager.CreateAsync(newUser,postUserDTO.Password);
            if(!result.Succeeded)
            {
                throw new CreateUserFailException(result.Errors);
            }

            foreach (var roleId in postUserDTO.RoleId )
            {
                var role = roles.FirstOrDefault(r => r.Id == roleId);
                
                 await  _userManager.AddToRoleAsync(newUser, role.Name);

            }
        }

        public async Task UpdateUserAsync(string Id,PutUserDTO putUserDTO)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u=>u.Id == Id);
            if(user is null)
            {
                throw new UserNotFoundByIdException(Id);
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            if(userRoles.Any(r=>r == Roles.Admin.ToString())) 
            {
                var rolesList = new List<IdentityRole>();
                foreach(var roleId in putUserDTO.RoleId)
                {
                    var role =await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
                    if(role is null)
                    {
                        throw new UserRoleNotFoundException("Role not found");
                    }
                    rolesList.Add(role);


                }
                if (!rolesList.Any(r=>r.Name == Roles.Admin.ToString()))
                {
                    throw new ThereIsCanBeOnlyOneAdminException("Admin Required") ;
                }
            }
            else
            {
                var rolesList = new List<IdentityRole>();
                foreach (var roleId in putUserDTO.RoleId)
                {
                    var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
                    if (role is null)
                    {
                        throw new UserRoleNotFoundException("Role not found");
                    }
                    rolesList.Add(role);


                }
                if (rolesList.Any(r => r.Name == Roles.Admin.ToString()))
                {
                    throw new ThereIsCanBeOnlyOneAdminException("Only One Admin Allowed");
                }
               

            }


            if(putUserDTO.RoleId.Count() == 0)
            {
                throw new RolesAreRequiredException("Roles are required");
            }
            if((putUserDTO.Password is not null && putUserDTO.ConfirmPassword is null)|| (putUserDTO.Password is null && putUserDTO.ConfirmPassword is not null)) 
            {
                throw new PasswordAndConfirmPasswordMustBeInputedTogetherException("Confirm Password or Password is null");
            }

            if (putUserDTO.Password is not null && putUserDTO.ConfirmPassword is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result =  await _userManager.ResetPasswordAsync(user,token,putUserDTO.Password);
                if(!result.Succeeded)
                {
                    throw new ResetUserPasswordException(result.Errors);
                }
            }
            if(putUserDTO.RoleId is not null)
            {
                if (putUserDTO.RoleId.Count() > 0)
                {
                    var newRoles = new List<string>();
                    foreach(var roleId in putUserDTO.RoleId)
                    {
                        var role = await _roleManager.Roles.FirstOrDefaultAsync(r=>r.Id == roleId);
                        if(role is null)
                        {
                            throw new UserRoleNotFoundException("Role not found");
                        }
                        var roles = await _userManager.GetRolesAsync(user);

                    
                        
                        newRoles.Add(role.Name);
                       

                        var removeRoles = roles.Except(newRoles);
                        await _userManager.RemoveFromRolesAsync(user, removeRoles);

                        var rolesToAdd = newRoles.Except(roles).ToList();
                        await _userManager.AddToRolesAsync(user, rolesToAdd);
                    }

                }
            }

            user = _mapper.Map(putUserDTO,user);

           var updateResult = await _userManager.UpdateAsync(user);
            if(!updateResult.Succeeded) 
            {
                throw new UpdateUserFailException(updateResult.Errors);

            }


        }

        public async Task DeleteUserAsync(string Id)
        {
            var user =await _userManager.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if(user is null)
            {
                throw new UserNotFoundByIdException("User Not Found");
            }
            if(user.UserName == "Admin")
            {
                throw new AdminCannotBeDeletedException("Admin cannot be deleted");
            }
            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                if(role== Roles.Admin.ToString())
                {
                    throw new AdminCannotBeDeletedException("Admin cannot be deleted");
                }
            }

           await _userManager.DeleteAsync(user);
        }

        public async Task ChangePasswordAsync(string id,ChangePasswordDTO changePasswordDTO)
        {
            var user  =  await _userManager.FindByIdAsync(id);
            if(user is null)
            {
                throw new UserNotFoundByIdException("User not found");
            }


            var passwordCheckResult = await _userManager.CheckPasswordAsync(user,changePasswordDTO.oldPassword);
            if (!passwordCheckResult)
            {
                throw new OldPasswordIsIncorrectException("Old password is incorrect");
            }
            if(changePasswordDTO.newPassword == changePasswordDTO.oldPassword) 
            {
               throw new OldPasswordEqualsToNewPasswordException("New password   cannot be equal to Old password");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result =  await  _userManager.ChangePasswordAsync(user,changePasswordDTO.oldPassword,changePasswordDTO.newPassword);
            if(!result.Succeeded)
            {
                throw new ChangePasswordFailException(result.Errors);
            }
        }

        public async Task<bool> CheckUserIsActiveAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if(user is null )
            {
                throw new UserNotFoundByIdException("User not found");
            }
            if(user.IsActive == false)
            {
                return false;
            }
            return true;
        }

  

    }
}
