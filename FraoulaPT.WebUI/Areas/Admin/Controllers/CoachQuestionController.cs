using FraoulaPT.DTOs.UserQuestionDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
// Hubların namespace'i sende farklıysa bunu düzelt
using FraoulaPT.WebUI.Hubs;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Koc,Admin")]
    public class CoachQuestionController : Controller
    {
        private readonly IUserQuestionService _userQuestionService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<NotificationHub> _hub; // 🔔 eklendi

        public CoachQuestionController(
            IUserQuestionService userQuestionService,
            UserManager<AppUser> userManager,
            IHubContext<NotificationHub> hub // 🔔 eklendi
        )
        {
            _userQuestionService = userQuestionService;
            _userManager = userManager;
            _hub = hub;
        }

        // Tüm öğrencilerden gelen sorular
        public async Task<IActionResult> Index()
        {
            var questions = await _userQuestionService.GetAllQuestionsAsync();
            return View(questions);
        }

        // AJAX: /Admin/CoachQuestion/Answer
        [HttpPost]
        [ValidateAntiForgeryToken] // ✅ antiforgery
        public async Task<IActionResult> Answer(Guid questionId, string answerText)
        {
            var coach = await _userManager.GetUserAsync(User);
            if (coach == null)
                return Unauthorized("Giriş yapmanız gerekiyor.");

            if (string.IsNullOrWhiteSpace(answerText))
                return BadRequest("Cevap boş olamaz.");

            var success = await _userQuestionService.AnswerQuestionAsync(questionId, answerText, coach.Id);
            if (!success)
                return BadRequest("Cevap kaydedilemedi.");

            // 🔔 Admin zilini güncelle (layouttaki script 'QuestionAnswered' eventini dinliyor)
            await _hub.Clients.All.SendAsync("QuestionAnswered");

            // (Opsiyonel) Öğrenciye de haber ver:
            // var q = await _userQuestionService.GetByIdAnswerAsync(questionId); // DTO’da AskedByUserId varsa
            // if (q?.AskedByUserId is Guid userId)
            //     await _hub.Clients.User(userId.ToString()).SendAsync("QuestionAnswered", new { questionId, preview = answerText.Length > 80 ? answerText[..80] + "…" : answerText });

            return Ok();
        }

        // Cevapla / Düzenle sayfası
        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var question = await _userQuestionService.GetByIdAnswerAsync(id);
            if (question == null)
                return NotFound();

            return View(question); // UserQuestionAnswerDTO
        }

        // Cevap kaydet (form post)
        [HttpPost]
        [ValidateAntiForgeryToken] // ✅ antiforgery
        public async Task<IActionResult> Detail(UserQuestionAnswerDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var coach = await _userManager.GetUserAsync(User);
            if (coach == null)
                return Unauthorized();

            var success = await _userQuestionService.AnswerQuestionAsync(dto.QuestionId, dto.AnswerText, coach.Id);
            if (!success)
            {
                TempData["message"] = "Cevap kaydedilirken bir hata oluştu.";
                TempData["messageType"] = "error";
                return View(dto);
            }

            // 🔔 Zili tetikle
            await _hub.Clients.All.SendAsync("QuestionAnswered");

            TempData["message"] = "Cevap başarıyla kaydedildi.";
            TempData["messageType"] = "success";
            return RedirectToAction("Index");
        }
    }
}
