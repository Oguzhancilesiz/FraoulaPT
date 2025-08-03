using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using FraoulaPT.WebUI.Models.ViewModels.QuestionAnsverViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FraoulaPT.WebUI.ViewComponents
{
    public class SupportChatViewComponent : ViewComponent
    {
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserPackageService _userPackageService;

        public SupportChatViewComponent(IAppUserService appUserService, UserManager<AppUser> userManager, IUserPackageService userPackageService)
        {
            _appUserService = appUserService;
            _userManager = userManager;
            _userPackageService = userPackageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = User as ClaimsPrincipal;
            var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return View(new SupportBoxViewModel()); // boş view modeli

            var guid = Guid.Parse(userId);
            var coaches = await _appUserService.GetAllCoachesAsync();
            var activePackage = await _userPackageService.GetActivePackageStatusAsync(guid);

            var vm = new SupportBoxViewModel
            {
                CurrentUserId = guid,
                Coaches = coaches,
                UserPackageId = activePackage?.UserPackageId ?? Guid.Empty
            };

            return View(vm);
        }
    }
}
