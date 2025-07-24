using FraoulaPT.DTOs.ExerciseDTOs;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.ExerciseViewModels;
using FraoulaPT.WebUI.Models.Enums;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExerciseController : BaseController
    {
        private readonly IExerciseService _exerciseService;
        private readonly IExerciseCategoryService _categoryService;

        public ExerciseController(IExerciseService exerciseService, IExerciseCategoryService categoryService)
        {
            _exerciseService = exerciseService;
            _categoryService = categoryService;
        }

        // LİSTELEME

        public async Task<IActionResult> Index()
        {
            var exercises = await _exerciseService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();

            var vm = new ExerciseListVM
            {
                Exercises = exercises,
                Categories = categories
            };

            return View(vm); // @model ExerciseListVM
        }
        // Areas/Admin/Controllers/ExerciseController.cs

        [HttpGet]
        public async Task<IActionResult> AjaxFilter(string search, Guid? categoryId, int? status)
        {
            var exercises = await _exerciseService.GetAllAsync();

            if (!string.IsNullOrEmpty(search))
                exercises = exercises.Where(x => x.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            if (categoryId.HasValue && categoryId != Guid.Empty)
                exercises = exercises.Where(x => x.CategoryId == categoryId).ToList();

            if (status.HasValue)
                exercises = exercises.Where(x => (int)x.Status == status).ToList();

            // Table Row döndür (Partial gerek yok, string dön)
            var sb = new System.Text.StringBuilder();
            int row = 1;
            foreach (var egzersiz in exercises)
            {
                sb.AppendLine($@"<tr>
            <td>{row}</td>
            <td>{(string.IsNullOrEmpty(egzersiz.ImageUrl) ? "<span class='text-muted'>-</span>" : $"<img src='{egzersiz.ImageUrl}' style='width:48px;height:48px;object-fit:cover;border-radius:7px;box-shadow:0 2px 10px #292e4922;'>")}</td>
            <td class='fw-bold'>{egzersiz.Name}</td>
            <td>{egzersiz.CategoryName}</td>
            <td>{egzersiz.Description}</td>
            <td>{(!string.IsNullOrEmpty(egzersiz.VideoUrl) ? $"<a href='{egzersiz.VideoUrl}' class='btn btn-outline-primary btn-sm' target='_blank'><i class='bi bi-play-btn'></i> İzle</a>" : "<span class='text-muted'>Yok</span>")}</td>
            <td>{(egzersiz.Status == FraoulaPT.Core.Enums.Status.Active ? "<span class='badge bg-success'>Aktif</span>" :
                            egzersiz.Status == FraoulaPT.Core.Enums.Status.DeActive ? "<span class='badge bg-secondary'>Pasif</span>" :
                            egzersiz.Status == FraoulaPT.Core.Enums.Status.Deleted ? "<span class='badge bg-danger'>Silindi</span>" :
                            egzersiz.Status == FraoulaPT.Core.Enums.Status.Updated ? "<span class='badge bg-warning text-dark'>Güncellendi</span>" :
                            "<span class='badge bg-warning text-dark'>Bilinmiyor</span>")}</td>
            <td>
                <a href='/Admin/Exercise/Edit/{egzersiz.Id}' class='btn btn-warning btn-sm me-1'>
                    <i class='bi bi-pencil'></i> Düzenle
                </a>
                <form action='/Admin/Exercise/Delete/{egzersiz.Id}' method='post' class='d-inline' onsubmit='return confirm(""Silmek istediğine emin misin?"");'>
                    <button type='submit' class='btn btn-danger btn-sm'>
                        <i class='bi bi-trash'></i> Sil
                    </button>
                </form>
            </td>
        </tr>");
                row++;
            }

            if (exercises.Count == 0)
                sb.AppendLine("<tr><td colspan='8' class='text-center text-muted'>Sonuç bulunamadı.</td></tr>");

            return Content(sb.ToString(), "text/html");
        }


        // EKLEME (GET)
        public async Task<IActionResult> Create()
        {
            var vm = new ExerciseCreateVM
            {
                Categories = await _categoryService.GetAllAsync()
            };
            return View(vm);
        }

        // EKLEME (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseCreateVM vm)
        {
            // Görsel zorunluluğunu kontrol et
            if (vm.ImageFile == null)
            {
                ModelState.AddModelError(nameof(vm.ImageFile), "Görsel yüklemelisiniz!");
            }
            else
            {
                // DOSYAYI ÖNCE KAYDET ve ImageUrl'yi ATA!
                var imageUrl = await FileHelper.SaveExerciseImageAsync(vm.ImageFile);
                vm.Exercise.ImageUrl = imageUrl;
            }

            // ŞİMDİ ModelState VALIDATION YAP!
            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllAsync();
                return View(vm);
            }

            await _exerciseService.AddAsync(vm.Exercise);
            ShowMessage("Egzersiz başarıyla eklendi!", MessageType.Success);
            return RedirectToAction("Index");
        }


        // GÜNCELLEME (GET)
        public async Task<IActionResult> Edit(Guid id)
        {
            var detail = await _exerciseService.GetByIdAsync(id);
            if (detail == null)
            {
                ShowMessage("Egzersiz bulunamadı!", MessageType.Error);
                return RedirectToAction("Index");
            }

            var vm = new ExerciseEditVM
            {
                Exercise = detail.Adapt<ExerciseUpdateDTO>(),
                Categories = await _categoryService.GetAllAsync(),
                ExistingImageUrl = detail.ImageUrl
            };
            return View(vm);
        }

        // GÜNCELLEME (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExerciseEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = await _categoryService.GetAllAsync();
                return View(vm);
            }

            if (vm.ImageFile != null)
            {
                var imageUrl = await FileHelper.SaveExerciseImageAsync(vm.ImageFile);
                vm.Exercise.ImageUrl = imageUrl;
            }
            else
            {
                // Eski url’yi DİREKT olarak DTO’ya ata:
                vm.Exercise.ImageUrl = vm.ExistingImageUrl;
            }
            vm.Exercise.Status = Core.Enums.Status.Updated;
            await _exerciseService.UpdateAsync(vm.Exercise);
            ShowMessage("Egzersiz başarıyla güncellendi!", MessageType.Success);
            return RedirectToAction("Index");
        }


        // SİLME (SOFT DELETE)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _exerciseService.SoftDeleteAsync(id);
            ShowMessage("Egzersiz başarıyla silindi!", MessageType.Success);
            return RedirectToAction("Index");
        }
    }
    public static class FileHelper
    {
        public static async Task<string> SaveExerciseImageAsync(IFormFile imageFile, string rootFolder = "wwwroot/uploads/exercises")
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            // Klasör yoksa oluştur
            if (!Directory.Exists(rootFolder))
                Directory.CreateDirectory(rootFolder);

            // Benzersiz dosya adı oluştur
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
            var path = Path.Combine(rootFolder, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // DB'de ve linkte kullanılacak path
            return "/uploads/exercises/" + fileName;
        }
    }
}
