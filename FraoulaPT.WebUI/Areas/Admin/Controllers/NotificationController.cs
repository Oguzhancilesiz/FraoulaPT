using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Koc,SuperAdmin")]
    [Route("Admin/[controller]/[action]")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notifs;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(INotificationService notifs, UserManager<AppUser> userManager, ILogger<NotificationController> logger)
        {
            _notifs = notifs; _userManager = userManager; _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Unauthorized();

                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin")
                           || await _userManager.IsInRoleAsync(user, "SuperAdmin");

                var dto = isAdmin
                    ? await _notifs.GetAdminSummaryAsync()
                    : await _notifs.GetCoachSummaryAsync(user.Id);

                return Json(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Notification/Summary error");
                return StatusCode(500, ex.Message); // geçici: hatayı gör
            }
        }
    }

}
