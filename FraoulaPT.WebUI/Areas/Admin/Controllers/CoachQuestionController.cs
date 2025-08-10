using FraoulaPT.DTOs.UserQuestionDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Koc,Admin")]
    public class CoachQuestionController : Controller
    {
        private readonly IUserQuestionService _userQuestionService;
        private readonly UserManager<AppUser> _userManager;

        public CoachQuestionController(IUserQuestionService userQuestionService, UserManager<AppUser> userManager)
        {
            _userQuestionService = userQuestionService;
            _userManager = userManager;
        }

        // Tüm öğrencilerden gelen sorular
        public async Task<IActionResult> Index()
        {
            var questions = await _userQuestionService.GetAllQuestionsAsync();
            return View(questions);
        }
        [HttpPost]
        public async Task<IActionResult> Answer(Guid questionId, string answerText)
        {
            var coach = await _userManager.GetUserAsync(User);
            if (coach == null)
                return Unauthorized("Giriş yapmanız gerekiyor.");

            if (string.IsNullOrWhiteSpace(answerText))
                return BadRequest("Cevap boş olamaz.");

            var success = await _userQuestionService.AnswerQuestionAsync(questionId, answerText, coach.Id);

            if (success)
                return Ok();
            else
                return BadRequest("Cevap kaydedilemedi.");
        }

        // 📌 Cevapla / Düzenle sayfası
        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var question = await _userQuestionService.GetByIdAnswerAsync(id);
            if (question == null)
                return NotFound();

            return View(question); // DTO dönecek
        }

        // 📌 Cevap kaydet
        [HttpPost]
        public async Task<IActionResult> Detail(UserQuestionAnswerDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var coach = await _userManager.GetUserAsync(User);
            if (coach == null)
                return Unauthorized();

            var success = await _userQuestionService.AnswerQuestionAsync(dto.QuestionId, dto.AnswerText, coach.Id);

            if (success)
            {
                TempData["message"] = "Cevap başarıyla kaydedildi.";
                TempData["messageType"] = "success";
                return RedirectToAction("Index");
            }

            TempData["message"] = "Cevap kaydedilirken bir hata oluştu.";
            TempData["messageType"] = "error";
            return View(dto);
        }
    }
}
