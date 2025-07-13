using FraoulaPT.Core.Tokens;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FraoulaPT.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PayTRPaymentService _paytrPaymentService;
        private readonly IPackageService _packageService;
        private readonly PayTROptions _options;
        private readonly IUserService _userService;
        // UserService/Repo'yu da ekle, ihtiyaca göre

        public PaymentController(PayTRPaymentService paytrPaymentService, IPackageService packageService, IUserService userService,
    IOptions<PayTROptions> options)
        {
            _paytrPaymentService = paytrPaymentService;
            _packageService = packageService;
            _userService = userService;
            _options = options.Value;
        }
        [Authorize]
        public async Task<IActionResult> Pay(Guid packageId)
        {
            var package = await _packageService.GetByIdAsync(packageId);
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // Email bazen Claim'de olmayabilir, Identity'den veya user entity'den çekilebilir
            var userEmail = User.Identity.Name;

            // Şimdi DB'den user'ı çek (AppUser, ApplicationUser veya ne kullandıysan)
            var appUser = await _userService.GetByIdAsync(userId); // Senin servis metodun

            string userName = appUser.FullName; // veya FirstName + LastName
            string userPhone = appUser.PhoneNumber;

            string token = await _paytrPaymentService.CreatePaymentToken(package, userEmail, userName, userPhone, userId);

            ViewBag.Token = token;
            return View("PayTRForm");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PayTRNotify()
        {
            var form = await Request.ReadFormAsync();
            string paytrHash = form["hash"];
            string status = form["status"];
            string totalAmount = form["total_amount"];
            string merchant_oid = form["merchant_oid"];
            string card_token = form["card_token"];

            string hashStr = merchant_oid + _options.MerchantSalt + status + totalAmount;

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_options.MerchantKey)))
            {
                var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(hashStr)));
                if (paytrHash != computedHash)
                {
                    // Hatalı istek! Bildirimi dikkate alma
                    return Content("FAILED");
                }
            }

            if (status == "success")
            {
                // merchant_oid ile UserPackage eşleştirme/kayıt
                // card_token geldiyse DB'de user ile sakla
            }

            return Content("OK");
        }
    }
}
