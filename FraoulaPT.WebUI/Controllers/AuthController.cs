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

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailService mailService, IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _memoryCache = memoryCache;
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

                ShowMessage("Kayıt başarılı! E-posta adresine doğrulama bağlantısı gönderildi. Onayladıktan sonra giriş yapabilirsin.", MessageType.Success);
                return RedirectToAction("Login", "Auth");
            }

            ShowMessage("Kayıt başarısız! Lütfen bilgilerinizi kontrol edin.", MessageType.Error);
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
                ShowMessage("Geçersiz doğrulama isteği.", MessageType.Error);
                return RedirectToAction("Login");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ShowMessage("Kullanıcı bulunamadı.", MessageType.Error);
                return RedirectToAction("Login");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                ShowMessage("E-posta doğrulama başarılı! Artık giriş yapabilirsin.", MessageType.Success);
                return RedirectToAction("Login");
            }
            else
            {
                ShowMessage("E-posta doğrulama başarısız veya süresi dolmuş. Lütfen tekrar deneyin.", MessageType.Error);
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
                ShowMessage("Kullanıcı bulunamadı veya aktif değil.", MessageType.Error);
                return View(dto);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ShowMessage("E-posta adresinizi doğrulamanız gerekiyor. Lütfen e-posta kutunuzu kontrol edin.", MessageType.Error);
                return View(dto);
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, dto.Password, false, false);

            if (result.Succeeded)
            {
                // Kendi claim'lerini oluştur
                var claims = new List<Claim>
                {

                    new Claim("FullName", user.FullName ?? user.UserName),
                    new Claim("ProfilePhotoUrl", user.Profile?.ProfilePhotoUrl ?? "/uploads/user-default.jpg")
                };
                // Mevcut authentication'ı güncelle!
                await _signInManager.SignOutAsync();
                await _signInManager.SignInWithClaimsAsync(user, false, claims);

                ShowMessage("Giriş başarılı!", MessageType.Success);
                return RedirectToAction("Index", "Home");
            }

            ShowMessage("Giriş başarısız! Lütfen kullanıcı adı ve şifrenizi kontrol edin.", MessageType.Error);
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
                ShowMessage("Son şifre yenileme talebiniz hâlâ geçerli. Lütfen birkaç dakika sonra tekrar deneyin.", MessageType.Warning);
                return View(dto);
            }

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                ShowMessage("Kullanıcı bulunamadı.", MessageType.Error);
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
            ShowMessage("Şifre yenileme bağlantısı e-posta adresine gönderildi.", MessageType.Success);
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
    }
}
