using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.PackageDTOs;
using FraoulaPT.DTOs.UserPackageDTOs;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Coach")]
    public class PackageController : BaseController
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _packageService.GetAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PackageCreateDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            if (dto.ImageFile != null)
            {
                var fileName = $"{Guid.NewGuid()}_{dto.ImageFile.FileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "packages", fileName);

                // Klasör yoksa oluştur
                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                dto.ImageUrl = $"/uploads/packages/{fileName}";
            }

            await _packageService.AddAsync(dto);
            ShowAlert("Eklendi", "Paket Başarıyla Eklendi", AlertType.success);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var detailDto = await _packageService.GetByIdAsync(id);
            var updateDto = detailDto.Adapt<PackageUpdateDTO>(); // Mapster kullanıyorsan
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PackageUpdateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                ShowAlert("Hata", "Paket Güncellenirken bir hata oluştu lütfen tekrar deneyin", AlertType.error);
                return View(dto);
            }
                

            if (dto.ImageFile != null)
            {
                var fileName = $"{Guid.NewGuid()}_{dto.ImageFile.FileName}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "packages", fileName);

                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                dto.ImageUrl = $"/uploads/packages/{fileName}";
            }

            dto.Status = Status.Active;

            await _packageService.UpdateAsync(dto);

            ShowAlert("Güncellendi", "Paket Başarıyla Güncellendi", AlertType.success);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            await _packageService.SoftDeleteAsync(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> ToggleStatus([FromBody] ToggleStatusDTO model)
        {
            var existing = await _packageService.GetByIdAsync(model.Id);
            if (existing == null)
                return Json(new { success = false, message = "Paket bulunamadı." });

            var updateDto = existing.Adapt<PackageUpdateDTO>();

            // Sadece IsActive alanını değiştiriyoruz
            updateDto.IsActive = !existing.IsActive;

            var result = await _packageService.UpdateAsync(updateDto);
            if (!result)
                return Json(new { success = false, message = "Durum güncellenemedi." });

            return Json(new { success = true, newStatus = updateDto.IsActive ? "Aktif" : "Pasif" });
        }

    }

}
