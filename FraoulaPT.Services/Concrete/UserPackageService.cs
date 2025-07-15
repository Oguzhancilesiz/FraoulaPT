using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.ChatMessageDTO;
using FraoulaPT.DTOs.UserPackageDTO;
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
    public class UserPackageService : IUserPackageService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserPackageService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<MyCoachChatDTO> GetMyCoachInfoAsync(Guid userId)
        {
            // Sistemdeki ilk (ve tek) koçu bul
            var allUsers = _userManager.Users.ToList();
            var coach = allUsers.FirstOrDefault(u => _userManager.IsInRoleAsync(u, "KOC").Result);
            if (coach == null) throw new Exception("Koç bulunamadı!");

            // Kullanıcının aktif paketini bul
            var userPackageQuery = await _unitOfWork.Repository<UserPackage>()
                .GetBy(x => x.AppUserId == userId && x.Status == Status.Active && x.EndDate > DateTime.Now);

            var activePackage = await userPackageQuery.OrderByDescending(x => x.StartDate).FirstOrDefaultAsync();

            int maxMsg = activePackage?.Package?.MaxMessagesPerPeriod ?? 0;
            int usedMsg = activePackage?.UsedMessages ?? 0;

            return new MyCoachChatDTO
            {
                CoachId = coach.Id,
                CoachFullName = coach.FullName,
                //CoachAvatarUrl = coach.Profile?.AvatarUrl, // AppUser'da yoksa kaldır
                RemainingMessageCount = maxMsg - usedMsg
            };
        }
        public async Task<UserPackageListDTO> GetCurrentActivePackageAsync(Guid userId)
        {
            // Aktif ve tarihi geçmemiş paketi bul
            var query = await _unitOfWork.Repository<UserPackage>()
                .GetBy(x => x.AppUserId == userId && x.IsActive && x.EndDate > DateTime.Now);

            query = query.Include(x => x.Package);

            var userPackage = await query.OrderByDescending(x => x.StartDate).FirstOrDefaultAsync();

            if (userPackage == null)
                return null;

            // Manuel map
            return new UserPackageListDTO
            {
                Id = userPackage.Id,
                AppUserId = userPackage.AppUserId,
                PackageId = userPackage.PackageId,
                PackageName = userPackage.Package?.Name,
                PackageDescription = userPackage.Package?.Description,
                StartDate = userPackage.StartDate,
                EndDate = userPackage.EndDate,
                UsedQuestions = userPackage.UsedQuestions,
                UsedMessages = userPackage.UsedMessages,
                MaxQuestionsPerPeriod = userPackage.Package?.MaxQuestionsPerPeriod ?? 0,
                MaxMessagesPerPeriod = userPackage.Package?.MaxMessagesPerPeriod ?? 0,
                IsActive = userPackage.IsActive,
                HighlightColor = userPackage.Package?.HighlightColor,
                Price = userPackage.Package?.Price ?? 0
            };
        }

    }
}
