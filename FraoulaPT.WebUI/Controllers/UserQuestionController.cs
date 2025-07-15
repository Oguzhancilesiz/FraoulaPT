using FraoulaPT.Core.Enums;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FraoulaPT.WebUI.Controllers
{
    [Authorize]
    public class UserQuestionController : Controller
    {
        private readonly IUserQuestionService _userQuestionService;

        public UserQuestionController(IUserQuestionService userQuestionService)
        {
            _userQuestionService = userQuestionService;
        }

        // Kullanıcının tüm sorularını listeler
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var questions = await _userQuestionService.GetMyQuestionsAsync(userId);
            return View(questions);
        }

        // Soru gönderme (POST)
        [HttpPost]
        public async Task<IActionResult> Ask(string questionText)
        {
            if (string.IsNullOrWhiteSpace(questionText))
            {
                TempData["Error"] = "Soru boş olamaz!";
                return RedirectToAction("Index");
            }

            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var success = await _userQuestionService.AskQuestionAsync(userId, questionText);

            if (!success)
            {
                TempData["Error"] = "Soru hakkınız kalmamış veya aktif paketiniz yok!";
            }
            else
            {
                TempData["Success"] = "Sorunuz başarıyla gönderildi.";
            }
            return RedirectToAction("Index");
        }
    }

}
