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
using System.Text.RegularExpressions;

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

        [RequestFormLimits(MultipartBodyLengthLimit = 6L * 1024 * 1024)] // 6MB
        [HttpGet]
        public async Task<IActionResult> CompleteProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Auth");

            var profile = await _userProfileService.GetByAppUserIdAsync(user.Id);
            if (profile != null) return RedirectToAction("Edit");

            return View(new UserProfileCreateDTO());
        }

        // --- Güvenli dosya kabulü için yardımcılar ---
        private static readonly string[] _permittedExt = [".jpg", ".jpeg", ".png", ".webp"];
        private const long _fileSizeLimit = 5L * 1024 * 1024; // 5MB

        private static bool LooksLikeImage(byte[] header)
        {
            // JPEG FF D8, PNG 89 50 4E 47, WEBP "RIFF....WEBP"
            if (header.Length >= 2 && header[0] == 0xFF && header[1] == 0xD8) return true; // JPEG
            if (header.Length >= 8 && header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47) return true; // PNG
            if (header.Length >= 12 && header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x46 &&
                header[8] == 0x57 && header[9] == 0x45 && header[10] == 0x42 && header[11] == 0x50) return true; // WEBP
            return false;
        }

        private async Task<(bool ok, string? msg, string url)> SaveProfilePhotoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (true, null, "/uploads/user-default.jpg");

            if (file.Length > _fileSizeLimit)
                return (false, "Dosya çok büyük (en fazla 5MB).", null!);

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !_permittedExt.Contains(ext))
                return (false, "İzin verilmeyen dosya türü. (jpg, jpeg, png, webp)", null!);

            if (!file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                return (false, "Geçersiz içerik türü.", null!);

            // magic byte kontrolü
            using var s = file.OpenReadStream();
            var header = new byte[12];
            var read = await s.ReadAsync(header.AsMemory(0, header.Length));
            if (read < 2 || !LooksLikeImage(header))
                return (false, "Dosya görüntü formatında görünmüyor.", null!);
            s.Position = 0;

            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploads);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var path = Path.Combine(uploads, fileName);

            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                await s.CopyToAsync(fs);

            return (true, null, "/uploads/" + fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteProfile(UserProfileCreateDTO dto, IFormFile profilePhoto, string action)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Auth");

            // Zaten profili varsa doğrudan Edit
            var exists = await _userProfileService.GetByAppUserIdAsync(user.Id);
            if (exists != null) return RedirectToAction("Edit", "Profile");

            // "Daha sonra" akışı: minimum güvenli default
            if (string.Equals(action, "skip", StringComparison.OrdinalIgnoreCase))
            {
                var defaultDto = new UserProfileCreateDTO
                {
                    AppUserId = user.Id,
                    ProfilePhotoUrl = "/uploads/user-default.jpg",
                    Gender = Gender.Other,
                    BirthDate = DateTime.UtcNow.Date,
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
                    Notes = "İsteğe Bağlı",
                    DietType = DietType.Custom
                };

                var id = await _userProfileService.AddAsync(defaultDto);
                ShowAlert("Bilgilendirme", "Profil daha sonra düzenlenmek üzere oluşturuldu. Lütfen bilgileri doldurun.", AlertType.warning);
                return RedirectToAction("Index", "Home");
            }

            // ---------- Sunucu tarafı doğrulamalar ----------
            // Enum doğrulamaları
            if (dto.Gender.HasValue && !Enum.IsDefined(typeof(Gender), dto.Gender.Value))
                ModelState.AddModelError(nameof(dto.Gender), "Geçersiz cinsiyet değeri.");
            if (dto.BodyType.HasValue && !Enum.IsDefined(typeof(BodyType), dto.BodyType.Value))
                ModelState.AddModelError(nameof(dto.BodyType), "Geçersiz vücut tipi.");
            if (dto.BloodType.HasValue && !Enum.IsDefined(typeof(BloodType), dto.BloodType.Value))
                ModelState.AddModelError(nameof(dto.BloodType), "Geçersiz kan grubu.");
            if (dto.ExperienceLevel.HasValue && !Enum.IsDefined(typeof(ExperienceLevel), dto.ExperienceLevel.Value))
                ModelState.AddModelError(nameof(dto.ExperienceLevel), "Geçersiz deneyim seviyesi.");
            if (dto.DietType.HasValue && !Enum.IsDefined(typeof(DietType), dto.DietType.Value))
                ModelState.AddModelError(nameof(dto.DietType), "Geçersiz beslenme tipi.");

            // Tarih: gelecekte olmasın
            if (dto.BirthDate.HasValue && dto.BirthDate.Value.Date > DateTime.UtcNow.Date)
                ModelState.AddModelError(nameof(dto.BirthDate), "Doğum tarihi gelecekte olamaz.");

            // Sayısal aralıklar (isteğe göre daraltılabilir)
            if (dto.HeightCm is < 50 or > 250)
                ModelState.AddModelError(nameof(dto.HeightCm), "Boy 50-250 cm aralığında olmalıdır.");
            if (dto.WeightKg is < 20 or > 400)
                ModelState.AddModelError(nameof(dto.WeightKg), "Kilo 20-400 kg aralığında olmalıdır.");

            // Instagram handle basit temizlik: sadece kullanıcı adı tut
            if (!string.IsNullOrWhiteSpace(dto.Instagram))
            {
                var handle = dto.Instagram.Trim().TrimStart('@', '/');
                // boşluk ve URL parçası temizle
                handle = Regex.Replace(handle, @"\s+", "");
                // isterseniz daha sıkı filtre: sadece harf/rakam/._ allow
                handle = Regex.Replace(handle, @"[^A-Za-z0-9._]", "");
                dto.Instagram = handle;
            }

            if (!ModelState.IsValid) return View(dto);

            // Overposting kapalı: AppUserId server'da set
            dto.AppUserId = user.Id;

            // --- Fotoğraf yükleme güvenli ---
            var (ok, msg, savedUrl) = await SaveProfilePhotoAsync(profilePhoto);
            if (!ok)
            {
                ModelState.AddModelError(nameof(profilePhoto), msg!);
                return View(dto);
            }
            dto.ProfilePhotoUrl = savedUrl;

            try
            {
                var newProfileId = await _userProfileService.AddAsync(dto);
                if (newProfileId == Guid.Empty)
                {
                    ShowAlert("Hata", "Profil oluşturulurken bir hata oluştu.", AlertType.error);
                    return View(dto);
                }

                ShowAlert("Başarılı", "Profil başarıyla oluşturuldu.", AlertType.success);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Log ex
                ShowAlert("Hata", "Beklenmeyen bir hata oluştu.", AlertType.error);
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
                ShowAlert("Başarılı","Profil başarıyla güncellendi.", AlertType.success);
                return RedirectToAction("Edit");
            }

            ShowAlert("Hata", "Güncelleme sırasında hata oluştu.", AlertType.error);
            return View(dto);
        }

        // GET: /Profile/Detail
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Detail()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Auth");

            var profile = await _userProfileService.GetByAppUserIdAsync(user.Id);
            if (profile == null)
            {
                ShowAlert("Uyarı", "Profil bilgisi bulunamadı. Lütfen profilinizi tamamlayın.", AlertType.warning);
                return RedirectToAction("CompleteProfile");
            }

            var dto = profile.Adapt<UserProfileDetailDTO>();
            // AppUser alanlarını ekle
            dto.FullName = user.FullName;           // projenizdeki alan adı buysa
            dto.UserName = user.UserName;
            dto.Email = user.Email;

            // Varsa oluşturulma tarihi (profile.CreatedDate) buraya set edilir
            dto.CreatedDate = profile.CreatedDate;

            return View(dto);
        }
    }
}
