using FraoulaPT.DTOs.UserDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using FraoulaPT.WebUI.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FraoulaPT.WebUI.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IUserProfileService _userProfileService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserPackageService _userPackageService;

        public ProfileController(IUserProfileService userProfileService, UserManager<AppUser> userManager, IUserPackageService userPackageService)
        {
            _userProfileService = userProfileService;
            _userManager = userManager;
            _userPackageService = userPackageService;
        }


        // Kullanıcının tüm satın aldığı/aktif paketleri
        [Authorize]
        public async Task<IActionResult> MyPackage()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var model = await _userPackageService.GetCurrentActivePackageAsync(userId);

            if (model == null)
                return View("Aktif Paketiniz Yok");

            return View(model);
        }

        // Profil detay (görüntüleme)
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var profile = await _userProfileService.GetProfileAsync(user.Id);
            if (profile == null)
            {
                ShowMessage("Profil bulunamadı, lütfen tamamlayın.", MessageType.Warning);
                return RedirectToAction("Edit");
            }

            return View(profile);
        }

        // Profil düzenleme (GET)
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var dto = await _userProfileService.GetProfileForEditAsync(user.Id);
            if (dto == null)
                dto = new ProfileEditDTO(); // Yeni kullanıcı için boş model

            return View(dto);
        }

        // Profil düzenleme (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileEditDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            await _userProfileService.UpdateProfileAsync(user.Id, dto);

            ShowMessage("Profiliniz güncellendi.", MessageType.Success);
            return RedirectToAction("Index");
        }
    }
}
