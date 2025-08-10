using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.UserPackageDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.WebUI.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FraoulaPT.WebUI.Controllers
{
    public class PackageController : BaseController
    {
        private readonly IPackageService _packageService;
        UserManager<AppUser> _userManager;
        private readonly IUserPackageService _userPackageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PackageController(IPackageService packageService, IUserPackageService userPackageService, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _packageService = packageService;
            _userPackageService = userPackageService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) {
                ShowAlert("Hata", "Önce Oturum Açmalısın.", AlertType.error);
                return RedirectToAction("Login", "Auth");
            }
            var guid = Guid.Parse(userId);

            var hasActive = await _userPackageService.HasActivePackageAsync(guid);
            var packages = await _packageService.GetAllAsync();

            var vm = new PackageIndexVM
            {
                Packages = packages,
                HasActivePackage = hasActive
            };

            return View(vm);
        }

        // Gelecekte detay veya satın alma eklenecekse:
        // [HttpGet("Package/Detail/{id}")]
        // public async Task<IActionResult> Detail(Guid id)
        // {
        //     var package = await _packageService.GetByIdAsync(id);
        //     if (package == null) return NotFound();
        //     return View(package);
        // }

        [HttpPost]
        public async Task<IActionResult> Purchase(Guid packageId)
        {

            var userId = Guid.Parse(_userManager.GetUserId(User)!);

            var hasActive = await _userPackageService.HasActivePackageAsync(userId);
            if (hasActive)
            {
                ShowAlert("Uyarı", "Zaten aktif bir paketiniz var!", AlertType.warning);
                return RedirectToAction("Index");
            }


            var package = await _packageService.GetByIdAsync(packageId);
            if (package == null || !package.IsActive || package.Status == Status.Deleted)
            {
               ShowAlert("Hata","Bu paket bulunamadı veya aktif değil.", AlertType.error);
                return RedirectToAction("Index");
            }

            var dto = new UserPackageCreateDTO
            {
                AppUserId = userId,
                PackageId = packageId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1),
                UsedMessages = 0,
                UsedQuestions = 0,
                IsActive = true,
                IsRenewable = false,
                Status = Status.Active
            };

            var result = await _userPackageService.CreateAsync(dto);
            if (result)
            {
                // 📌 2. İlk defa paket alıyorsa roller ekle
                var user = await _userManager.FindByIdAsync(userId.ToString());
                var currentRoles = await _userManager.GetRolesAsync(user);

                var rolesToAdd = new List<string>();

                if (!currentRoles.Contains("User"))
                    rolesToAdd.Add("User");

                if (!currentRoles.Contains("Ogrenci"))
                    rolesToAdd.Add("Ogrenci");

                if (rolesToAdd.Any())
                    await _userManager.AddToRolesAsync(user, rolesToAdd);

                ShowAlert("Başarılı", "Paket satın alma işlemi başarılı.", AlertType.success);
            }
            else
            {
                ShowAlert("Hata", "Paket satın alma işlemi başarısız oldu.", AlertType.error);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MyPackages()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var packages = await _userPackageService.GetPackagesByUserAsync(userId);
            return View(packages); // @model List<UserPackageDetailDTO>
        }

    }
}
