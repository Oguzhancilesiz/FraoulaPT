using FraoulaPT.DTOs.DashboardDTOs;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.DashboardViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Koc,Admin")]
    public class DashboardController : Controller
    {
        private readonly IUserPackageService _userPackageService;
        private readonly IAppUserService _appUserService;
        private readonly IUserQuestionService _userQuestionService;
        private readonly IChatMessageService _chatService;

        public DashboardController(
            IUserPackageService userPackageService,
            IAppUserService appUserService,
            IUserQuestionService userQuestionService,
            IChatMessageService chatService)
        {
            _userPackageService = userPackageService;
            _appUserService = appUserService;
            _userQuestionService = userQuestionService;
            _chatService = chatService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel
            {
                ActivePackageCount = await _userPackageService.GetActivePackageCountAsync(),
                ActiveStudentCount = await _appUserService.GetActiveStudentCountAsync(),
                PendingQuestionCount = await _userQuestionService.GetPendingCountAsync(),
                MonthlyRevenue = await _userPackageService.GetMonthlyRevenueAsync(),
                ExpiringPackagesCount = await _userPackageService.GetExpiringPackagesCountAsync(),
                UnapprovedPaymentsCount = await _userPackageService.GetUnapprovedPaymentsCountAsync(),
                ActiveChatsCount = await _chatService.GetActiveChatsCountAsync(),
                AvgResponseTime = await _chatService.GetAverageResponseTimeAsync(),

                // Bu 3 metodu UserPackageService veya AppUserService içine ekleyeceğiz
                TopCoaches = await _appUserService.GetTopCoachesAsync(5),
                TopStudents = await _appUserService.GetTopStudentsAsync(5),
                ExpiringPackages = await _userPackageService.GetExpiringPackagesAsync(5),
                LowMessageQuotaUsers = await _userPackageService.GetLowMessageQuotaUsersAsync(5),

                // Bu metodu ChatService içine ekleyeceğiz
                RecentActivities = await _chatService.GetRecentActivitiesAsync(10)
            };

            return View(vm);
        }
    }

}
