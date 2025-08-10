using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.AuthDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using FraoulaPT.WebUI.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace FraoulaPT.WebUI.Controllers
{
    public class AuthController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailService _mailService;
        private readonly IMemoryCache _memoryCache;
        private readonly IUserPackageService _userPackageService;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailService mailService, IMemoryCache memoryCache, IUserPackageService userPackageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _memoryCache = memoryCache;
            _userPackageService = userPackageService;
        }

        // GET: /Auth/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FullName = dto.FullName,
                Status = FraoulaPT.Core.Enums.Status.Active,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                // ✨ 1. Doğrulama token'ı üret
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // ✨ 2. Doğrulama linki oluştur
                var confirmationLink = Url.Action("ConfirmEmail", "Auth",
                    new { userId = user.Id, token = token }, Request.Scheme);

                // ✨ 3. Mail gönder (kendi mail servis kodunu kullan)
                var subject = "FraoulaPT - E-posta Doğrulama";
                var body = $@"
                                <h3>Merhaba {user.FullName},</h3>
                                <p>FraoulaPT'ye kaydolduğun için teşekkürler. Hesabını aktifleştirmek için aşağıdaki bağlantıya tıkla:</p>
                                <p><a href='{confirmationLink}'>E-postamı Doğrula</a></p>
                                <p>Bu bağlantı kısa süre için geçerlidir.</p>
                                <p>Teşekkürler,<br/>FraoulaPT Ekibi</p>
                            ";
                await _mailService.SendAsync(user.Email, subject, body);

                ShowAlert("Bilgi", "Kayıt başarılı! E-posta adresinize doğrulama linki gönderildi.", Core.Enums.AlertType.info);
                return RedirectToAction("Login", "Auth");
            }

            ShowAlert("Hata", "Kayıt başarısız. Lütfen bilgilerinizi kontrol edin.", Core.Enums.AlertType.error);
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(dto);
        }

        //eposta dogrulama
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                ShowAlert("Hata", "Geçersiz doğrulama isteği.", Core.Enums.AlertType.error);
                return RedirectToAction("Login");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ShowAlert("Hata", "Kullanıcı bulunamadı.", Core.Enums.AlertType.error);
                return RedirectToAction("Login");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                ShowAlert("Başarılı", "E-posta adresiniz başarıyla doğrulandı. Giriş yapabilirsiniz.", Core.Enums.AlertType.success);
                return RedirectToAction("Login");
            }
            else
            {
                ShowAlert("Hata", "E-posta doğrulama başarısız. Lütfen tekrar deneyin.", Core.Enums.AlertType.error);
                return RedirectToAction("Login");
            }
        }

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userManager.FindByEmailAsync(dto.UserNameOrEmail) ?? await _userManager.FindByNameAsync(dto.UserNameOrEmail);



            if (user == null || user.Status != FraoulaPT.Core.Enums.Status.Active)
            {
                ShowAlert("Uyarı", "Kullanıcı bulunamadı veya aktif değil", Core.Enums.AlertType.warning);
                return View(dto);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ShowAlert("Uyarı", "E-posta adresiniz doğrulanmamış. Lütfen e-postanızı kontrol edin.", Core.Enums.AlertType.warning);
                return View(dto);
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, dto.Password, false, false);

            if (result.Succeeded)
            {
                var userId = Guid.Parse(user.Id.ToString());
                var remainingDays = await _userPackageService.GetRemainingDaysAsync(userId);

                // ✅ Aktif paketi varsa kontrol et
                if (remainingDays.HasValue)
                {
                    if (remainingDays.Value <= 3)
                    {
                        ShowAlert(
                            "Uyarı",
                            $"Paketinizin bitmesine {remainingDays.Value} gün kaldı. Yeni paket almayı unutmayın!",
                            Core.Enums.AlertType.warning
                        );
                    }
                }

                // Kendi claim'lerini oluştur
                var claims = new List<Claim>
                {
                    new Claim("FullName", user.FullName ?? user.UserName),
                    new Claim("ProfilePhotoUrl", user.Profile?.ProfilePhotoUrl ?? "/uploads/user-default.jpg")
                };

                // Mevcut authentication'ı güncelle
                await _signInManager.SignOutAsync();
                await _signInManager.SignInWithClaimsAsync(user, false, claims);

                ShowAlert("Başarılı", $"Hoşgeldin {user.FullName}", Core.Enums.AlertType.success);
                return RedirectToAction("Index", "Home");
            }


            ShowAlert("Uyarı", "Kullanıcı adı veya şifre hatalı", Core.Enums.AlertType.warning);
            return View(dto);
        }


        // GET: /Auth/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Auth/ForgotPassword
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var cacheKey = $"ForgotPwd_{dto.Email}";
            if (_memoryCache.TryGetValue(cacheKey, out _))
            {
                ShowAlert("Uyarı", "Şifre yenileme isteği için lütfen 2 dakika bekleyin.", Core.Enums.AlertType.warning);
                return View(dto);
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                ShowAlert("Hata", "Kullanıcı bulunamadı.", Core.Enums.AlertType.error);
                return View(dto);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = Url.Action("ResetPassword", "Auth", new { email = user.Email, token = token }, Request.Scheme);

            var subject = "FraoulaPT - Şifre Sıfırlama";
            var body = $@"
                            <p>Merhaba,</p>
                            <p>Şifrenizi yenilemek için aşağıdaki bağlantıya tıklayın:</p>
                            <p><a href='{resetLink}'>{resetLink}</a></p>
                            <p>Bu bağlantı kısa süre sonra geçersiz olacaktır.</p>
                            <br>
                            <p>FraoulaPT Ekibi</p>
                        ";

            await _mailService.SendAsync(user.Email, subject, body);

            // Limit Anahtarı setleniyor
            _memoryCache.Set(cacheKey, true, TimeSpan.FromMinutes(2));
            ViewBag.ResetPasswordWaitSeconds = 120; // 2 dakika
            ShowAlert("Bilgi", "Şifre yenileme bağlantısı e-posta adresinize gönderildi. Lütfen e-postanızı kontrol edin.", Core.Enums.AlertType.info);
            return View();
        }


        // GET: /Auth/ResetPassword?email=xx&token=yy
        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            var model = new UserResetPasswordDTO
            {
                Email = email,
                Token = token
            };
            return View(model);
        }

        // POST: /Auth/ResetPassword
        [HttpPost]
        public async Task<IActionResult> ResetPassword(UserResetPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(dto);
            }

            if (dto.NewPassword != dto.ConfirmPassword)
            {
                ModelState.AddModelError("", "Şifreler eşleşmiyor.");
                return View(dto);
            }

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

            if (result.Succeeded)
            {
                ViewBag.Info = "Şifre sıfırlama başarılı! Giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(dto);
        }

        // /Auth/Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            ShowAlert("Bilgi", "Çıkış Başarılı", Core.Enums.AlertType.info);
            return RedirectToAction("Login", "Auth");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword() => View(new ChangePasswordDTO());

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                ShowAlert("Başarılı", "Şifreniz güncellendi.", AlertType.success);
                return RedirectToAction("Detail", "Profile");
            }

            foreach (var e in result.Errors) ModelState.AddModelError("", e.Description);
            ShowAlert("Hata", "Şifre güncellenemedi.", AlertType.error);
            return View(dto);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangeEmail() => View(new ChangeEmailDTO());

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login");

            if (string.Equals(user.Email, dto.NewEmail, StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("", "Yeni e-posta, mevcut e-postanızla aynı.");
                return View(dto);
            }

            // E-posta zaten kullanılıyor mu?
            var exists = await _userManager.FindByEmailAsync(dto.NewEmail);
            if (exists != null && exists.Id != user.Id)
            {
                ModelState.AddModelError("", "Bu e-posta başka bir hesap tarafından kullanılıyor.");
                return View(dto);
            }

            // Onay bağlantısı için token üret
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, dto.NewEmail);
            var link = Url.Action("ConfirmChangeEmail", "Auth",
                new { userId = user.Id, newEmail = dto.NewEmail, token },
                Request.Scheme);

            var subject = "FraoulaPT - E-posta Değiştirme Onayı";
            var body = $@"
        <p>Merhaba {user.FullName ?? user.UserName},</p>
        <p>E-posta adresinizi <b>{dto.NewEmail}</b> olarak değiştirmek için bağlantıya tıklayın:</p>
        <p><a href='{link}'>E-postamı Değiştir</a></p>
        <p>Bağlantı kısa süreli geçerlidir.</p>";

            await _mailService.SendAsync(dto.NewEmail, subject, body);

            ShowAlert("Bilgi", "Onay bağlantısı yeni e-posta adresinize gönderildi.", AlertType.info);
            return RedirectToAction("Detail", "Profile");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ConfirmChangeEmail(string userId, string newEmail, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(newEmail) || string.IsNullOrWhiteSpace(token))
            {
                ShowAlert("Hata", "Geçersiz istek.", AlertType.error);
                return RedirectToAction("Login");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ShowAlert("Hata", "Kullanıcı bulunamadı.", AlertType.error);
                return RedirectToAction("Login");
            }

            // E-posta değişimini tamamla
            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
            if (!result.Succeeded)
            {
                ShowAlert("Hata", "E-posta değiştirilemedi. Bağlantının süresi dolmuş olabilir.", AlertType.error);
                return RedirectToAction("Login");
            }

            // (Opsiyonel) E-posta = kullanıcı adı kuralın varsa:
            // await _userManager.SetUserNameAsync(user, newEmail);

            await _signInManager.RefreshSignInAsync(user);
            ShowAlert("Başarılı", "E-posta adresiniz güncellendi.", AlertType.success);
            return RedirectToAction("Detail", "Profile");
        }
    }
}
