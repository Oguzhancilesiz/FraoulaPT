using FraoulaPT.Services.Abstracts;
using FraoulaPT.WebUI.Hubs;
using FraoulaPT.WebUI.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FraoulaPT.WebUI.Controllers
{
    [Authorize]
    public class ChatController : BaseController
    {
        private readonly IChatMessageService _chatService;
        private readonly IWebHostEnvironment _env;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IChatMessageService chatService, IWebHostEnvironment env, IHubContext<ChatHub> hubContext)
        {
            _chatService = chatService;
            _env = env;
            _hubContext = hubContext;
        }

        // Chat sayfası
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            // Tek bir koç olduğu için, koçun Id'sini config veya userService’den bulabilirsin:
            var coachId = await _chatService.GetCoachIdAsync(); // Bunu serviste implement et
            var messages = await _chatService.GetChatAsync(userId, coachId);
            ViewBag.CurrentUserId = userId;
            ViewBag.CoachId = coachId;
            return View(messages); // List<ChatMessageDTO> ViewModel ile gönderiyoruz
        }

        // Mesaj gönderme
        [HttpPost]
        public async Task<IActionResult> Send([FromForm] string message, [FromForm] List<IFormFile> mediaFiles)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var coachId = await _chatService.GetCoachIdAsync();

            try
            {
                var result = await _chatService.SendMessageAsync(userId, coachId, message, mediaFiles, _env.WebRootPath);

                // SignalR ile hem kullanıcıya hem koça anlık gönder
                await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveMessage", result, true);
                await _hubContext.Clients.User(coachId.ToString()).SendAsync("ReceiveMessage", result, false);

                return Ok(new { result, message = "Mesajınız gönderildi!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
