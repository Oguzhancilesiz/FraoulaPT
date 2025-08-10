using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FraoulaPT.WebUI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IStudentDashboardService _svc;
        public DashboardController(IStudentDashboardService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var dto = await _svc.BuildAsync(userId);
            return View(dto);
        }
    }
}
