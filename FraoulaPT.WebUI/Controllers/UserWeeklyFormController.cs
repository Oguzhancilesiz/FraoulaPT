using FraoulaPT.DTOs.UserWeeklyFormDTO;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.WebUI.Extensions;
using FraoulaPT.WebUI.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Controllers
{
    [Authorize]
    public class UserWeeklyFormController : BaseController
    {
        private readonly IUserWeeklyFormService _weeklyFormService;
        private readonly IUserPackageService _userPackageService;
        private readonly IMediaService _mediaService;

        public UserWeeklyFormController(
            IUserWeeklyFormService weeklyFormService,
            IUserPackageService userPackageService,
            IMediaService mediaService)
        {
            _weeklyFormService = weeklyFormService;
            _userPackageService = userPackageService;
            _mediaService = mediaService;
        }

        // Kullanıcı formlarını listeler
        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserId(); // Kendi extension metodun varsa
            var userPackageId = await _userPackageService.GetActiveUserPackageIdAsync(userId);

            if (!userPackageId.HasValue)
                return View(new List<UserWeeklyFormDTO>());

            var forms = await _weeklyFormService.GetUserFormsAsync(userPackageId.Value);
            return View(forms);
        }

        // Yeni form ekranı
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni form gönderme
        [HttpPost]
        public async Task<IActionResult> Create(UserWeeklyFormCreateDTO dto)
        {
            // Progress photo dosyalarını kaydet
            var progressPhotos = new List<Media>();
            if (dto.ProgressPhotoFiles != null)
            {
                foreach (var file in dto.ProgressPhotoFiles)
                {
                    var media = await _mediaService.SaveMediaAsync(file);
                    progressPhotos.Add(media);
                }
            }

            // Kullanıcı Id'yi al (ör: HttpContext'ten)
            var userId = User.GetUserId(); // extension metodu ise, yoksa kendin bul
            if (dto.UserPackageId == Guid.Empty)
            {
                // Kullanıcıya ait aktif paketi bul
                var userPackage = await _userPackageService.GetActiveUserPackageIdAsync(userId);
                if (userPackage == null)
                {
                    // Hata mesajı döndür
                   ShowMessage("Aktif bir paket bulunamadı. Lütfen önce bir paket satın alın.", MessageType.Error);
                    // Gerekirse tekrar formu döndür
                    return View(dto);
                }
                dto.UserPackageId = userPackage.Value;
            }


            ShowMessage("Form eklendi en kısa sürede ilgili koç dönüş sağlayacaktır.", MessageType.Success);
            await _weeklyFormService.AddFormAsync(dto, progressPhotos);
            return RedirectToAction("Index");
        }

        // Hoca, feedback ekler (bu endpointi hocaya açık tut!)
        [HttpPost]
        [Authorize(Roles = "Coach,Admin")] // örnek role
        public async Task<IActionResult> AddCoachFeedback(UserWeeklyFormCoachFeedbackDTO dto)
        {
            await _weeklyFormService.AddCoachFeedbackAsync(dto);
            return RedirectToAction("Index"); // veya hocanın paneline yönlendirme
        }
    }
}
