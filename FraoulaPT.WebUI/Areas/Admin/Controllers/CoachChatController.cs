using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using FraoulaPT.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Koc,Admin")]
    [Area("Admin")]
    public class CoachChatController : Controller
    {
        private readonly IChatMessageService _chatService;
        private readonly IAppUserService _userService;

        public CoachChatController(IChatMessageService chatService, IAppUserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var coachId = User.GetUserId(); // Extension metodu ile Identity'den al
            var students = await _chatService.GetStudentsWhoMessagedCoachAsync(coachId);

            return View(students); // @model List<StudentChatListDTO>
        }

        [HttpGet]
        public async Task<IActionResult> GetHistory(Guid studentId)
        {
            var coachIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(coachIdStr, out var coachId))
                return Unauthorized();

            var messages = await _chatService.GetChatHistoryAsync(coachId, studentId);

            var result = messages.Select(m => new
            {
                messageText = m.MessageText,
                sentAt = m.SentAt,
                isMine = m.SenderId == coachId
            });

            return Json(result);
        }
    }

}
