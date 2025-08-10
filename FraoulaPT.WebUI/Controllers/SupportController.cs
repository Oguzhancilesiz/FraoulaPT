using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.UserQuestionDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Controllers
{
    [Authorize(Roles = "Ogrenci")]
    public class SupportController : BaseController
    {
        private readonly IUserQuestionService _userQuestionService;
        private readonly IUserPackageService _userPackageService;
        private readonly UserManager<AppUser> _userManager;

        public SupportController(IUserQuestionService userQuestionService, IUserPackageService userPackageService, UserManager<AppUser> userManager)
        {
            _userQuestionService = userQuestionService;
            _userPackageService = userPackageService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion(UserQuestionCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var success = await _userQuestionService.AskQuestionAsync(dto);
            return success ? Ok() :BadRequest("Soru gönderilemedi lütfen paket haklarınızı kontrol edin!");
        }

        [HttpGet]
        public async Task<IActionResult> MyQuestions()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Eğer giriş yapılmamışsa boş partial döndür veya hata mesajı ver
                return PartialView("_QuestionHistoryPartial", new List<UserQuestionDTO>());
                // veya: return Unauthorized();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return PartialView("_QuestionHistoryPartial", new List<UserQuestionDTO>());
            }
            var questions = await _userQuestionService.GetQuestionsByUserAsync(user.Id);
            return PartialView("_QuestionHistoryPartial", questions); // Partial olarak render et
        }
        [HttpGet]
        public async Task<IActionResult> QuestionDetail(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var question = await _userQuestionService.GetByIdAsync(id);
            if (question == null || question.UserId != user.Id)
                return NotFound(); // Kullanıcıya ait değilse 404

            return View(question);
        }

        [HttpGet]
        [Authorize(Roles = "Ogrenci")]
        public async Task<IActionResult> AllMyQuestions()
        {
            var user = await _userManager.GetUserAsync(User);
            var questions = await _userQuestionService.GetQuestionsByUserAsync(user.Id);
            return View(questions); // Tüm soruları listeleyecek view
        }
    }
}
