using FraoulaPT.DTOs.AppUserDTOs;
using FraoulaPT.DTOs.DashboardDTOs;
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
        Task<List<CoachListDTO>> GetAllCoachesAsync();

        //dashboard için
        Task<int> GetActiveStudentCountAsync();
        Task<List<UserActivityDTO>> GetRecentActivitiesAsync(int limit);
        Task<List<TopUserDTO>> GetTopCoachesAsync(int limit);
        Task<List<TopUserDTO>> GetTopStudentsAsync(int limit);

    }
}
