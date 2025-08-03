using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.UserWeeklyFormDTOs;
using FraoulaPT.DTOs.WorkoutDayDTOs;
using FraoulaPT.DTOs.WorkoutExerciseDTOs;
using FraoulaPT.DTOs.WorkoutProgramDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Services.Concrete;
using FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.WorkoutProgramViewModels;
using FraoulaPT.WebUI.Models.Enums;

using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FraoulaPT.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Coach")]
    public class WorkoutProgramController : BaseController
    {
        private readonly IWorkoutProgramService _workoutProgramService;
        private readonly IExerciseService _exerciseService;
        private readonly IUserWeeklyFormService _formService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkoutExerciseService _workoutExerciseService;


        public WorkoutProgramController(IWorkoutProgramService workoutProgramService, UserManager<AppUser> userManager, IExerciseService exerciseService, IUserWeeklyFormService formService, IUnitOfWork unitOfWork, IWorkoutExerciseService workoutExerciseService, IAppUserService appUserService)
        {
            _workoutProgramService = workoutProgramService;
            _userManager = userManager;
            _exerciseService = exerciseService;
            _formService = formService;
            _unitOfWork = unitOfWork;
            _workoutExerciseService = workoutExerciseService;
            _appUserService = appUserService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCoachNote(Guid programId, string note)
        {
            var form = await _formService.GetByIdAsync(programId);
            if (form == null)
                return NotFound();


            var updateDto = form.Adapt<UserWeeklyFormUpdateDTO>();

            updateDto.CoachFeedback = note;
            await _formService.UpdateAsync(updateDto);
            await _unitOfWork.SaveChangesAsync();
            ShowMessage("Not Güncellendi", FraoulaPT.WebUI.Models.Enums.MessageType.Success);
            return Ok();
        }



        [HttpPost]
        public async Task<IActionResult> CreateWithDays([FromBody] WorkoutProgramCreateWithDaysVM model)
        {
            var coach = await _userManager.GetUserAsync(User);
            var coachId = coach?.Id;
            if (coachId == null)
                return Unauthorized();

            // 1. WorkoutProgram'ı oluştur
            var program = new WorkoutProgram
            {
                UserWeeklyFormId = model.UserWeeklyFormId,
                AppUserId = model.AppUserId,
                ProgramTitle = model.ProgramTitle,
                CoachNote = model.CoachNote,
                CreatedByUserId = coachId,
                UpdatedByUserId = coachId,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Status = Status.Active,
                Days = new List<WorkoutDay>() // Şimdilik boş
            };

            await _unitOfWork.Repository<WorkoutProgram>().AddAsync(program);
            await _unitOfWork.SaveChangesAsync();

            // 2. Günleri tek tek ekle
            foreach (var day in model.Days)
            {
                var workoutDay = new WorkoutDay
                {
                    WorkoutProgramId = program.Id,
                    DayOfWeek = day,
                    Description = "", // İlk başta boş olabilir
                    CreatedByUserId = coachId,
                    UpdatedByUserId = coachId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Status = Status.Active
                };

                await _unitOfWork.Repository<WorkoutDay>().AddAsync(workoutDay);
            }

            await _unitOfWork.SaveChangesAsync();

            return Ok(new { success = true, programId = program.Id });
        }
        [HttpPost]
        public async Task<IActionResult> AddDay([FromBody] WorkoutDayCreateVM model)
        {
            var coach = await _userManager.GetUserAsync(User);
            var coachId = coach.Id;

            if (model.ProgramId == Guid.Empty || model.DayOfWeek < 1 || model.DayOfWeek > 7)
                return BadRequest("Geçersiz veri.");

            var day = new WorkoutDay
            {
                WorkoutProgramId = model.ProgramId,
                DayOfWeek = model.DayOfWeek,
                Description = model.Description ?? "",
                CreatedByUserId = coachId,
                UpdatedByUserId = coachId,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Status = Status.Active
            };

            await _unitOfWork.Repository<WorkoutDay>().AddAsync(day);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> AddExercise([FromForm] WorkoutExerciseCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Geçersiz veri.");

            var coach = await _userManager.GetUserAsync(User);
            var coachId = coach?.Id;

            if (coachId == null)
                return Unauthorized();

            var newExercise = new WorkoutExercise
            {
                WorkoutDayId = dto.WorkoutDayId,
                ExerciseId = dto.ExerciseId,
                SetCount = dto.SetCount,
                Repetition = dto.Repetition,
                Weight = dto.Weight,
                RestDurationInSeconds = dto.RestDurationInSeconds,
                Note = dto.Note,
                Status = Status.Active,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedByUserId = coachId,
                UpdatedByUserId = coachId,
            };

            try
            {
                await _unitOfWork.Repository<WorkoutExercise>().AddAsync(newExercise);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Loglama yapabilirsin
                return BadRequest("Egzersiz eklenirken hata oluştu: " + ex.Message);
            }
            ShowMessage("Egzersiz başarıyla eklendi.", FraoulaPT.WebUI.Models.Enums.MessageType.Success);
            return Ok();
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
                ShowMessage("Program Oluşturuldu", FraoulaPT.WebUI.Models.Enums.MessageType.Success);
                return RedirectToAction("Detail", "UserWeeklyForm", new { area = "Admin", id = dto.UserWeeklyFormId });
            }
            catch (Exception ex)
            {
                ShowMessage("Beklenmeyen bir hata meydana geldi. Daha sonra tekrar deneyin", FraoulaPT.WebUI.Models.Enums.MessageType.Error);
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
        [HttpPost]
        public async Task<IActionResult> UpdateExercise(WorkoutExerciseUpdateDTO dto)
        {
            try
            {
                await _workoutExerciseService.UpdateAsync(dto);
                ShowMessage("Hareket Başarıyla güncellendi", FraoulaPT.WebUI.Models.Enums.MessageType.Success);
                return Ok();

            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, FraoulaPT.WebUI.Models.Enums.MessageType.Error);
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteExercise([FromForm] Guid id)
        {
            await _workoutExerciseService.SoftDeleteAsync(id);
            ShowMessage("Kaldırıldı", FraoulaPT.WebUI.Models.Enums.MessageType.Warning);
            return Ok();
        }
        public async Task<IActionResult> Index()
        {

            var overviewList = new List<WorkoutProgramOverviewDTO>();
            var users = await _unitOfWork.Repository<AppUser>()
                                        .Query()
                                        .Include(u => u.Profile)
                                        .Include(u => u.UserWeeklyForms)
                                        .Include(u => u.WorkoutPrograms)
                                        .ToListAsync();

            foreach (var user in users)
            {
                var roles = await _appUserService.GetUserRolesAsync(user.Id);
                if (!roles.Contains("Ogrenci")) continue;

                var lastForm = user.UserWeeklyForms?
                    .Where(f => f.Status != Core.Enums.Status.Deleted)
                    .OrderByDescending(f => f.CreatedDate)
                    .FirstOrDefault();

                if (lastForm == null)
                    continue;

                var program = user.WorkoutPrograms?
                    .FirstOrDefault(p => p.UserWeeklyFormId == lastForm.Id && p.Status != Core.Enums.Status.Deleted);

                overviewList.Add(new WorkoutProgramOverviewDTO
                {
                    UserId = user.Id,
                    UserFullName = user.FullName,
                    UserProfilePhoto = user.Profile?.ProfilePhotoUrl,
                    FormId = lastForm.Id,
                    FormCreatedDate = lastForm.CreatedDate,
                    HasWorkoutProgram = program != null,
                    WorkoutProgramId = program?.Id,
                    CoachFeedback = lastForm.CoachFeedback
                });
            }

            return View(overviewList);
        }
    }
}
