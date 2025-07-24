using FraoulaPT.DTOs.AppUserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IAppUserService : IBaseService<
     AppUserListDTO,
     AppUserDetailDTO,
     AppUserCreateDTO,
     AppUserUpdateDTO>
    {
        Task<List<string>> GetUserRolesAsync(Guid userId);
        Task UpdateUserRolesAsync(Guid userId, List<string> newRoles);
    }
}
