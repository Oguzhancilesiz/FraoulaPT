using FraoulaPT.Core.Abstracts;
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
