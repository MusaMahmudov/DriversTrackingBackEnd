using AbelloLLC.Business.DTOs.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.Business.Services.Inerfaces
{
    public interface IUserService
    {
        public Task<List<GetUserDTO>> GetAllUsersAsync();
        public Task<GetUserDTO> GetUserByIdAsync(string Id);
        public Task<GetUserForUpdateDTO> GetUserByIdForUpdateAsync(string Id);
        public Task CreateUserAsync(PostUserDTO postUserDTO);

        public Task UpdateUserAsync(string Id,PutUserDTO putUserDTO);
        public Task DeleteUserAsync(string Id);
        public Task<bool> CheckUserIsActiveAsync(string Id);
        public Task ChangePasswordAsync(string id,ChangePasswordDTO changePasswordDTO);
    }
}
