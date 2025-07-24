using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.AppUserDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class AppUserService : BaseService<
    AppUser,
    AppUserListDTO,
    AppUserDetailDTO,
    AppUserCreateDTO,
    AppUserUpdateDTO>, IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUserService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager) : base(unitOfWork)
        {
            _userManager = userManager;
        }
        public async Task<List<string>> GetUserRolesAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return new List<string>();
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task UpdateUserRolesAsync(Guid userId, List<string> newRoles)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Eski rolleri kaldır, yenileri ekle
            await _userManager.RemoveFromRolesAsync(user, currentRoles.Except(newRoles));
            await _userManager.AddToRolesAsync(user, newRoles.Except(currentRoles));
        }
    }

}