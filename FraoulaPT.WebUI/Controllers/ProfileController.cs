using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.UserProfileDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.WebUI.Models.Enums;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FraoulaPT.WebUI.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserProfileService _userProfileService;
        private readonly IWebHostEnvironment _env;

        public ProfileController(
            UserManager<AppUser> userManager,
            IWebHostEnvironment env,
            IUserProfileService userProfileService,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _env = env;
            _userProfileService = userProfileService;
            _signInManager = signInManager;
        }

        // GET: /Profile/CompleteProfile
        [HttpGet]
        public async Task<IActionResult> CompleteProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _userProfileService.GetByAppUserIdAsync(user.Id);
            if (profile != null)
                return RedirectToAction("Edit"); // zaten profil oluşturduysa Edit'e yönlendir

            return View(new UserProfileCreateDTO());
        }

        // POST: /Profile/CompleteProfile
        [HttpPost]
        public async Task<IActionResult> CompleteProfile(UserProfileCreateDTO dto, IFormFile profilePhoto, string action)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var exists = await _userProfileService.GetByAppUserIdAsync(user.Id);
            if (exists != null)
                return RedirectToAction("Edit", "Profile");

            // Eğer "Daha Sonra" butonu seçildiyse:
            if (action == "skip")
            {
                var defaultDto = new UserProfileCreateDTO
                {
                    AppUserId = user.Id,
                    ProfilePhotoUrl = "/uploads/user-default.jpg",
                    Gender = Gender.Other,
                    BirthDate = DateTime.UtcNow,
                    HeightCm = null,
                    WeightKg = null,
                    BodyType = BodyType.None,
                    BloodType = BloodType.None,
                    Instagram = "İsteğe Bağlı",
                    PhoneNumber = "İsteğe Bağlı",
                    Address = "İsteğe Bağlı",
                    EmergencyContactName = "İsteğe Bağlı",
                    EmergencyContactPhone = "İsteğe Bağlı",
                    MedicalHistory = "İsteğe Bağlı",
                    ChronicDiseases = "İsteğe Bağlı",
                    CurrentMedications = "İsteğe Bağlı",
                    Allergies = "İsteğe Bağlı",
                    PastInjuries = "İsteğe Bağlı",
                    CurrentPain = "İsteğe Bağlı",
                    PregnancyStatus = false,
                    LastCheckResults = "İsteğe Bağlı",
                    SmokingAlcohol = "İsteğe Bağlı",
                    Occupation = "İsteğe Bağlı",
                    ExperienceLevel = ExperienceLevel.None,
                    FavoriteSports = "İsteğe Bağlı",
                    Notes = "İsteğe Bağlo",
                    DietType = DietType.Custom
                };
                await _userProfileService.AddAsync(defaultDto);
                ShowMessage("Profil daha sonra tamamlanmak üzere kaydedildi.", MessageType.Warning);
                return RedirectToAction("Index", "Home");
            }

            // Fotoğraf yükleme işlemi
            if (profilePhoto != null && profilePhoto.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = Guid.NewGuid() + Path.GetExtension(profilePhoto.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await profilePhoto.CopyToAsync(fs);
                }
                dto.ProfilePhotoUrl = "/uploads/" + fileName;
            }
            else
            {
                dto.ProfilePhotoUrl = "/uploads/user-default.jpg";
            }

            dto.AppUserId = user.Id;

            var newProfileId = await _userProfileService.AddAsync(dto);

            if (newProfileId != Guid.Empty)
            {
                ShowMessage("Profil başarıyla oluşturuldu.", MessageType.Success);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ShowMessage("Profil oluşturulurken bir hata oluştu.", MessageType.Error);
                return View(dto);
            }
        }

        // GET: /Profile/Edit
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _userProfileService.GetByAppUserIdAsync(user.Id);
            if (profile == null)
                return RedirectToAction("CompleteProfile");

            return View(profile.Adapt<UserProfileUpdateDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserProfileUpdateDTO dto, IFormFile profilePhoto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            // Kullanıcı bilgisini al
            var user = await _userManager.GetUserAsync(User);

            // Profil fotoğrafı güncellemesi
            if (profilePhoto != null && profilePhoto.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = Guid.NewGuid() + Path.GetExtension(profilePhoto.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await profilePhoto.CopyToAsync(fs);
                }
                dto.ProfilePhotoUrl = "/uploads/" + fileName;
            }
            else
            {
                // Eğer yeni fotoğraf yoksa, eski fotoğrafı koru
                var existingProfile = await _userProfileService.GetByAppUserIdAsync(user.Id);
                dto.ProfilePhotoUrl = existingProfile?.ProfilePhotoUrl ?? "/uploads/user-default.jpg";
            }
            dto.AppUserId = user.Id; 
            
            var result = await _userProfileService.UpdateAsync(dto);

            var claims = new List<Claim>
                {
                    new Claim("FullName", user.FullName ?? user.UserName),
                    new Claim("ProfilePhotoUrl", dto.ProfilePhotoUrl ?? "/uploads/user-default.jpg") // Yeni fotoğraf URL

                };

            await _signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);

            if (result)
            {
                ShowMessage("Profil başarıyla güncellendi.", MessageType.Success);
                return RedirectToAction("Edit");
            }

            ShowMessage("Güncelleme sırasında hata oluştu.", MessageType.Error);
            return View(dto);
        }

        // GET: /Profile/Detail
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Detail()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                RedirectToAction("Login", "Auth");
            var profile = await _userProfileService.GetByAppUserIdAsync(user.Id);

            if (profile == null)
            {
                ShowMessage("Profil bilgisi bulunamadı. Lütfen profilinizi tamamlayın.", MessageType.Warning);
                return RedirectToAction("CompleteProfile");
            }

            // ProfileDetailDTO dönecek şekilde maplediğini varsayıyorum
            return View(profile.Adapt<UserProfileDetailDTO>());
        }
    }
}
