using FraoulaPT.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Coach")]
    public class StudentsController : Controller
    {
        private readonly IStudentReportService _reportService;
        public StudentsController(IStudentReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Report(Guid userId)
        {
            var dto = await _reportService.BuildAsync(userId);
            return View(dto); // View doğrudan DTO ile çalışıyor
        }
    }
}
