using FraoulaPT.DTOs.ExerciseCategoryDTOs;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.WebUI.Models.Enums;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Coach")]
    public class ExerciseCategoryController : BaseController
    {
        private readonly IExerciseCategoryService _categoryService;

        public ExerciseCategoryController(IExerciseCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Liste
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // Ekle (GET)
        public IActionResult Create() => View();

        // Ekle (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseCategoryCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _categoryService.AddAsync(dto);
            ShowMessage("Kategori eklendi!", MessageType.Success);
            return RedirectToAction(nameof(Index));
        }

        // Düzenle (GET)
        public async Task<IActionResult> Edit(Guid id)
        {
            var detailDto = await _categoryService.GetByIdAsync(id);
            if (detailDto == null)
            {
                TempData["message"] = "Kategori bulunamadı!";
                return RedirectToAction(nameof(Index));
            }

            var updateDto = detailDto.Adapt<ExerciseCategoryUpdateDTO>();

            return View(updateDto);
        }


        // Düzenle (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExerciseCategoryUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _categoryService.UpdateAsync(dto);
            ShowMessage("Kategori güncellendi!", MessageType.Success);
            return RedirectToAction(nameof(Index));
        }

        // Sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.SoftDeleteAsync(id); // Soft delete
            ShowMessage("Kategori silindi!", MessageType.Success);
            return RedirectToAction(nameof(Index));
        }
    }
}
