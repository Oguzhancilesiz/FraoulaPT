using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FraoulaPT.WebUI.Controllers
{
    [Route("Chat")]
    public class ChatController : Controller
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly UserManager<AppUser> _userManager;

        public ChatController(IChatMessageService chatMessageService, UserManager<AppUser> userManager)
        {
            _chatMessageService = chatMessageService;
            _userManager = userManager;
        }

        [HttpGet("GetHistory")]
        public async Task<IActionResult> GetHistory(Guid coachId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var userGuid = Guid.Parse(userId);
            var messages = await _chatMessageService.GetChatHistoryAsync(userGuid, coachId);

            var result = messages.Select(msg => new
            {
                messageText = msg.MessageText,
                sentAt = msg.SentAt,
                isMine = msg.SenderId == userGuid
            });

            return Json(result);
        }
    }
}
