using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.ExtraPackageOptionDTOs;
using FraoulaPT.DTOs.UserPackageDTOs;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Coach")]
    public class ExtraPackageOptionController : BaseController
    {
        private readonly IExtraPackageOptionService _extraPackageOptionService;

        public ExtraPackageOptionController(IExtraPackageOptionService extraPackageOptionService)
        {
            _extraPackageOptionService = extraPackageOptionService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _extraPackageOptionService.GetAllAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExtraPackageOptionCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _extraPackageOptionService.CreateAsync(dto);
            if (!result)
            {
               ShowAlert("Hata!","Paket ekleme işlemi başarısız oldu.", AlertType.error);
                return View(dto);
            }

           ShowAlert("Başarılı!", "Paket başarıyla eklendi.", AlertType.success);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var dto = await _extraPackageOptionService.GetByIdAsync(id);
            if (dto == null)
                return NotFound();

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExtraPackageOptionUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _extraPackageOptionService.UpdateAsync(dto);
            if (!result)
            {
                ShowAlert("Hata!", "Paket güncelleme işlemi başarısız oldu.", AlertType.error);
                return View(dto);
            }

            ShowAlert("Başarılı!", "Paket başarıyla güncellendi.", AlertType.success);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _extraPackageOptionService.SoftDeleteAsync(id);
            if (!result)
            {
                ShowAlert("Hata!", "Paket silme işlemi başarısız oldu.", AlertType.error);
            }
            else
            {
                ShowAlert("Başarılı!", "Paket başarıyla silindi.", AlertType.success);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleActive([FromBody] ToggleStatusDTO model)
        {
            if (model == null || model.Id == Guid.Empty)
                return Json(new { success = false, message = "Geçersiz istek." });
            var result = await _extraPackageOptionService.ToggleActiveAsync(model.Id);
            if (!result)
                return Json(new { success = false, message = "Durum güncellenemedi." });

            return Json(new { success = true });
        }
    }
}
