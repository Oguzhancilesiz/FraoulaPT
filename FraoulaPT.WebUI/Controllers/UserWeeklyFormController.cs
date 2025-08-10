using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.UserWeeklyFormDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace FraoulaPT.WebUI.Controllers
{
    [Authorize]
    public class UserWeeklyFormController : BaseController
    {
        private readonly IUserWeeklyFormService _service;
        private readonly IUserPackageService _userPackageservice;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public UserWeeklyFormController(IUserWeeklyFormService service, IWebHostEnvironment env, UserManager<AppUser> userManager, IUserPackageService userPackageservice)
        {
            _service = service;
            _env = env;
            _userManager = userManager;
            _userPackageservice = userPackageservice;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var forms = await _service.GetListByUserAsync(user.Id);
            return View(forms);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            var userIdString = _userManager.GetUserId(User);
            var userId = Guid.Parse(userIdString);
            var userPackage = await _userPackageservice.GetActivePackageStatusAsync(userId);

            if (userPackage == null)
            {
                ShowAlert("Uyarı", "Aktif Paletiniz Yok Lütfen Satın Alın", AlertType.warning);
                return RedirectToAction("Index", "Package");
            }

            var now = DateTime.UtcNow;

            if (userPackage.EndDate < now)
            {
                ShowAlert("Hata", "Paketinizin Süresi Doldu yeniden satın alma yapmalısınız", AlertType.error);
                return RedirectToAction("Index", "Package");
            }
            var lastForm = await _service.GetLastFormByUserIdAsync(userId);

            if (lastForm != null)
            {
                var daysSinceLastForm = (DateTime.UtcNow.Date - lastForm.FormDate.Date).Days;

                if (daysSinceLastForm < 27)
                {
                    TempData["message"] = $"Son formun üzerinden {daysSinceLastForm} gün geçmiş. Yeni form gönderebilmek için {27 - daysSinceLastForm} gün daha beklemelisin.";
                    return RedirectToAction("Index");
                }
            }

            var model = new UserWeeklyFormCreateDTO
            {
                FormDate = DateTime.Today
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserWeeklyFormCreateDTO dto, List<IFormFile> files)
        {
            var userIdString = _userManager.GetUserId(User);
            var userId = Guid.Parse(userIdString);
            var userPackage = await _userPackageservice.GetActivePackageStatusAsync(userId);

            if (userPackage == null)
            {
               ShowAlert("Uyarı","Aktif Paletiniz Yok Lütfen Satın Alın",AlertType.warning);
            }

            var now = DateTime.UtcNow;

            if (userPackage.EndDate < now)
            {
                ShowAlert("Hata","Paketinizin Süresi Doldu yeniden satın alma yapmalısınız",AlertType.error); 
            }

            dto.FormDate = DateTime.UtcNow;
            var rootPath = _env.WebRootPath;
            await _service.AddWithFilesAsync(dto, userId, files, rootPath);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                ShowAlert("Bilgi", "Kullanıcı bulunamadı", Core.Enums.AlertType.info);
            }

            var userPackage = await _userPackageservice.GetActivePackageStatusAsync(user.Id);

            if (userPackage == null)
            {
                ShowAlert("Uyarı", "Aktif Paletiniz Yok Lütfen Satın Alın", AlertType.warning);
            }

            var now = DateTime.UtcNow;

            if (userPackage.EndDate < now)
            {
                ShowAlert("Hata", "Paketinizin Süresi Doldu yeniden satın alma yapmalısınız", AlertType.error);
            }


            var form = await _service.GetDetailWithPhotosByIdAsync(id); // ← Yeni metot
            if (form == null)
                return NotFound();

            return View(form);
        }
    }
}
