using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.AppUserDTOs;
using FraoulaPT.DTOs.DashboardDTOs;
using FraoulaPT.DTOs.WorkoutProgramDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<WorkoutProgramOverviewDTO>> GetWorkoutOverviewListAsync()
        {
            var users = await _unitOfWork.Repository<AppUser>().Query()
                .Include(u => u.WorkoutPrograms)
                    .ThenInclude(wp => wp.UserWeeklyForm)
                .Include(u => u.Profile)
                .Where(u => u.Status != Status.Deleted)
                .ToListAsync();

            var result = new List<WorkoutProgramOverviewDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Ogrenci"))
                    continue;

                var lastForm = user.WorkoutPrograms
                    .OrderByDescending(wp => wp.UserWeeklyForm.CreatedDate)
                    .Select(wp => wp.UserWeeklyForm)
                    .FirstOrDefault();

                if (lastForm == null)
                    continue;

                var existingProgram = user.WorkoutPrograms
                    .FirstOrDefault(wp => wp.UserWeeklyFormId == lastForm.Id && wp.Status != Status.Deleted);

                result.Add(new WorkoutProgramOverviewDTO
                {
                    UserId = user.Id,
                    UserFullName = user.FullName,
                    UserProfilePhoto = user.Profile?.ProfilePhotoUrl,
                    FormId = lastForm.Id,
                    FormCreatedDate = lastForm.CreatedDate,
                    HasWorkoutProgram = existingProgram != null,
                    WorkoutProgramId = existingProgram?.Id
                });
            }

            return result;
        }
        public async Task<List<CoachListDTO>> GetAllCoachesAsync()
        {
            var users = await _unitOfWork.Repository<AppUser>()
                .Query()
                .Include(u => u.Profile)
                .Where(u => u.Status == Status.Active)
                .ToListAsync();

            var result = new List<CoachListDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Koc"))
                {
                    result.Add(new CoachListDTO
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        ProfilePhotoUrl = user.Profile?.ProfilePhotoUrl ?? "/uploads/user-default.jpg"
                    });
                }
            }

            return result;
        }


        //dashboard için
        public async Task<int> GetActiveStudentCountAsync()
        {
            return await _userManager.Users
                .Where(u => u.Status == Status.Active)
                .CountAsync();
        }

        public async Task<List<UserActivityDTO>> GetRecentActivitiesAsync(int limit)
        {
            var questions = await _unitOfWork.Repository<UserQuestion>()
                       .Query()
                       .OrderByDescending(q => q.AskedAt)
                       .Take(limit)
                       .Select(q => new UserActivityDTO
                       {
                           UserName = q.AskedByUser.FullName,
                           ActionDescription = "Yeni soru sordu",
                           TimeAgo = EF.Functions.DateDiffMinute(q.AskedAt, DateTime.UtcNow) + " dk önce"
                       })
                       .ToListAsync();

            return questions;
        }

        public async Task<List<TopUserDTO>> GetTopCoachesAsync(int limit)
        {
            var coaches = await _userManager.GetUsersInRoleAsync("Koc");

            return coaches
                .Select(u => new TopUserDTO
                {
                    Name = u.FullName,
                    Count = u.AnsweredQuestions != null ? u.AnsweredQuestions.Count : 0
                })
                .OrderByDescending(x => x.Count)
                .Take(limit)
                .ToList();
        }

        public async Task<List<TopUserDTO>> GetTopStudentsAsync(int limit)
        {
            var students = await _userManager.GetUsersInRoleAsync("Ogrenci");

            return students
                .Select(u => new TopUserDTO
                {
                    Name = u.FullName,
                    Count = u.AnsweredQuestions != null ? u.AnsweredQuestions.Count : 0
                })
                .OrderByDescending(x => x.Count)
                .Take(limit)
                .ToList();
        }

    }

}