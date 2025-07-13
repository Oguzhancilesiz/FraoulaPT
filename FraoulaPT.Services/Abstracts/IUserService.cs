using FraoulaPT.DTOs.UserDTOs;
using FraoulaPT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IUserService
    {
        Task<AppUser> GetByIdAsync(Guid id);
        Task RegisterAsync(RegisterDTO dto);
        Task LoginAsync(LoginDTO dto);
        Task SignOutAsync();
        List<UserDTO> GetAllUser();
        UserDTO GetByUser(string userName);
    }
}
