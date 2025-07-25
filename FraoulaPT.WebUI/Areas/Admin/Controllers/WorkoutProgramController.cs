using FraoulaPT.DTOs.WorkoutDayDTOs;
using FraoulaPT.DTOs.WorkoutExerciseDTOs;
using FraoulaPT.DTOs.WorkoutProgramDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.WorkoutProgramViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Coach")]
    public class WorkoutProgramController : Controller
    {
        private readonly IWorkoutProgramService _workoutProgramService;
        private readonly IExerciseService _exerciseService;
        private readonly IUserWeeklyFormService _formService;
        private readonly UserManager<AppUser> _userManager;

        public WorkoutProgramController(IWorkoutProgramService workoutProgramService, UserManager<AppUser> userManager, IExerciseService exerciseService, IUserWeeklyFormService formService)
        {
            _workoutProgramService = workoutProgramService;
            _userManager = userManager;
            _exerciseService = exerciseService;
            _formService = formService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid formId)
        {
            var form = await _formService.GetByIdAsync(formId);
            if (form == null) return NotFound();

            var dto = new WorkoutProgramCreateDTO
            {
                UserWeeklyFormId = formId,
                Days = new List<WorkoutDayCreateDTO>()
            };

            // Egzersizleri çek
            var exercises = await _exerciseService.GetAllAsync();
            ViewBag.Exercises = exercises.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutProgramCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var coach = await _userManager.GetUserAsync(User);
            if (coach == null)
                return Unauthorized();

            var exercises = await _exerciseService.GetAllAsync(); // ya da sadece aktifler
            ViewBag.Exercises = exercises.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            try
            {
                var programId = await _workoutProgramService.CreateAsync(dto, coach.Id);
                TempData["message"] = "Program başarıyla oluşturuldu.";
                TempData["messageType"] = "success";
                return RedirectToAction("Detail", "UserWeeklyForm", new { area = "Admin", id = dto.UserWeeklyFormId });
            }
            catch (Exception ex)
            {
                TempData["message"] = "Hata: " + ex.Message;
                TempData["messageType"] = "error";
                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var program = await _workoutProgramService.GetWorkoutProgramDetailByIdAsync(id);
            if (program == null)
                return NotFound();

            return View(program);
        }
    }
}
