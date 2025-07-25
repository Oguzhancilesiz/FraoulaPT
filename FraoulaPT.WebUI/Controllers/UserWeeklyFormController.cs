using FraoulaPT.DTOs.UserWeeklyFormDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Controllers
{
    public class UserWeeklyFormController : Controller
    {
        private readonly IUserWeeklyFormService _service;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public UserWeeklyFormController(IUserWeeklyFormService service, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _service = service;
            _env = env;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var forms = await _service.GetListByUserAsync(user.Id);
            return View(forms);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userIdString = _userManager.GetUserId(User);
            var userId = Guid.Parse(userIdString);

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


            dto.FormDate = DateTime.UtcNow;
            var rootPath = _env.WebRootPath;
            await _service.AddWithFilesAsync(dto, userId, files, rootPath);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var form = await _service.GetDetailWithPhotosByIdAsync(id); // ← Yeni metot
            if (form == null)
                return NotFound();

            return View(form);
        }
    }
}
