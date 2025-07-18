using FraoulaPT.DTOs.UserProgramDTO;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Controllers
{
    [Route("[controller]")]
    public class WorkoutProgramController : Controller
    {
        private readonly IWorkoutProgramService _programService;

        public WorkoutProgramController(IWorkoutProgramService programService)
        {
            _programService = programService;
        }
        // WorkoutProgramController.cs

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> Detail(Guid id)
        {
            var currentUserId = User.GetUserId();
            ViewBag.CurrentUserId = currentUserId;

            var program = await _programService.GetByIdAsync(id, currentUserId);
            if (program == null)
                return NotFound();

            return View(program);
        }

        [HttpGet("my-programs")]
        public async Task<IActionResult> MyPrograms()
        {
            var userId = User.GetUserId(); // Bunu kendi user sistemine göre implemente et
            var programs = await _programService.GetAllByUserIdAsync(userId);

            // En güncel, halen devam eden programı bul (bugün EndDate >= bugün ve StartDate <= bugün)
            var now = DateTime.Now.Date;
            var activeProgram = programs
                .Where(x => x.StartDate <= now && x.EndDate >= now)
                .OrderByDescending(x => x.StartDate)
                .FirstOrDefault();

            // En son atanmış program (tarihe göre en günceli)
            var latestProgram = programs.OrderByDescending(x => x.StartDate).FirstOrDefault();

            ViewBag.ActiveProgramId = activeProgram?.Id;
            ViewBag.LatestProgramId = latestProgram?.Id;

            return View(programs);
        }
    }

}
