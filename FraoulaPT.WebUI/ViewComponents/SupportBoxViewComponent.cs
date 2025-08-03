using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using FraoulaPT.WebUI.Models.ViewModels.QuestionAnsverViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FraoulaPT.WebUI.ViewComponents
{
    public class SupportBoxViewComponent : ViewComponent
    {
        private readonly IAppUserService _appUserService;
        private readonly IUserPackageService _userPackageService;
        private readonly UserManager<AppUser> _userManager;

        public SupportBoxViewComponent(IAppUserService appUserService, IUserPackageService userPackageService,UserManager<AppUser> userManager)
        {
            _appUserService = appUserService;
            _userPackageService = userPackageService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = User as ClaimsPrincipal;
            var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return View(new SupportBoxViewModel()); // Boş view modeli döndür

            var guid = Guid.Parse(userId);
            var coaches = await _appUserService.GetAllCoachesAsync();
            var activePackage = await _userPackageService.GetActivePackageStatusAsync(guid);

            var vm = new SupportBoxViewModel
            {
                CurrentUserId = guid,
                UserPackageId = activePackage?.UserPackageId ?? Guid.Empty,
                Coaches = coaches
            };

            return View(vm);
        }

    }
}
