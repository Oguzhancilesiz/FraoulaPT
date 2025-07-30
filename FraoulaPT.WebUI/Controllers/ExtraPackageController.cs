using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.ExtraRightDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Controllers
{
    [Authorize]
    public class ExtraPackageController : BaseController
    {
        private readonly IExtraPackageOptionService _extraPackageOptionService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IExtraRightService _extraRightService;

        public ExtraPackageController(IExtraPackageOptionService extraPackageOptionService, UserManager<AppUser> userManager, IExtraRightService extraRightService)
        {
            _extraPackageOptionService = extraPackageOptionService;
            _userManager = userManager;
            _extraRightService = extraRightService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var packages = await _extraPackageOptionService.GetPublicListAsync();
            return View(packages); // @model List<ExtraPackageOptionListDTO>
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PurchaseExtraPackage(Guid extraPackageOptionId)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);

            var option = await _extraPackageOptionService.GetByIdAsync(extraPackageOptionId);
            if (option == null || !option.IsActive || option.Status == Status.Deleted)
            {
                ShowAlert("Hata", "Ek paket bulunamadı veya aktif değil.", AlertType.error);
                return RedirectToAction("Index");
            }

            var dto = new ExtraRightAddDTO
            {
                AppUserId = userId,
                ExtraPackageOptionId = option.Id,
                RightType = option.Type switch
                {
                    ExtraUsageType.Question => ExtraRightType.Question,
                    ExtraUsageType.Message => ExtraRightType.Message,
                    _ => throw new Exception("Bilinmeyen tip")
                },
                Amount = option.Amount
            };

            var result = await _extraRightService.AddAsync(dto);

            if (result)
                ShowAlert("Başarılı", "Ek paket başarıyla satın alındı.", AlertType.success);
            else
                ShowAlert("Hata", "Ek paket satın alma başarısız oldu.", AlertType.error);

            return RedirectToAction("Index");
        }

    }
}
