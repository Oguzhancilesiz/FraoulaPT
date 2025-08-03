using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DAL;
using FraoulaPT.DTOs.WorkoutFeedbackDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.WebUI.Models.Enums;
using FraoulaPT.WebUI.Models.ViewModels.WorkoutProgramViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FraoulaPT.WebUI.Controllers
{
    [Authorize]
    public class WorkoutController : BaseController
    {
        private readonly IWorkoutProgramService _workoutProgramService;
        private readonly UserManager<AppUser> _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutController(IWorkoutProgramService workoutProgramService, UserManager<AppUser> userContext , IUnitOfWork unitOfWork)
        {
            _workoutProgramService = workoutProgramService;
            _userContext = userContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> MyWorkout()
        {
            var ogrenci = await _userContext.GetUserAsync(User);

            if (ogrenci == null)
            {
                ShowAlert("Hata","Kayıtlı kullanıcı yok", AlertType.error);
                return RedirectToAction("Index", "Home");
            }
            var userid = ogrenci.Id;
            if (userid == Guid.Empty)
            {
                ShowAlert("Hata", "Kullanıcı ID'si bulunamadı", AlertType.error);
                return RedirectToAction("Index", "Home");
            }
            var program = await _workoutProgramService.GetLastWorkoutProgramByUserAsync(userid);

            if (program == null)
            {
                ShowAlert("Uyarı","Aktif bir antrenman programınız bulunmamaktadır.", AlertType.warning);
                return View(null);
            }

            var vm = new MyWorkoutProgramVM
            {
                ProgramTitle = program.ProgramTitle,
                CoachNote = program.CoachNote,
                Days = program.Days.OrderBy(d => d.DayOfWeek).Select(d => new WorkoutDayVM
                {
                    Id = d.Id,
                    DayOfWeek = d.DayOfWeek,
                    Description = d.Description,
                    Exercises = d.Exercises?.Select(e => new WorkoutExerciseVM
                    {
                        ExerciseName = e.Exercise?.Name,
                        SetCount = e.SetCount,
                        Repetition = e.Repetition
                    }).ToList() ?? new()
                }).ToList()
            };


            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> StartDay(Guid id)
        {
            var workoutDay = await _unitOfWork.Repository<WorkoutDay>()
                .Query()
                .Include(d => d.Exercises)
                    .ThenInclude(e => e.Exercise)
                        .ThenInclude(ex => ex.Category)
                .FirstOrDefaultAsync(d => d.Id == id && d.Status != Status.Deleted);

            if (workoutDay == null)
                return NotFound();

            return View(workoutDay);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitFeedback([FromForm] WorkoutFeedbackDTO dto)
        {
            if (dto.WorkoutExerciseId == Guid.Empty)
                return BadRequest("Geçersiz egzersiz.");
            var ogrenci = await _userContext.GetUserAsync(User);

            if (ogrenci == null)
            {
                ShowAlert("Hata", "Kayıtlı kullanıcı yok", AlertType.error);
                return RedirectToAction("Index", "Home");
            }
            var userid = ogrenci.Id;
            if (userid == Guid.Empty)
            {
                ShowAlert("Hata", "Kullanıcı ID'si bulunamadı", AlertType.error);
                return RedirectToAction("Index", "Home");
            }


            var userId = userid;
            var today = DateTime.UtcNow.Date;

            var existingFeedback = await _unitOfWork.Repository<WorkoutFeedback>()
                .Query()
                .FirstOrDefaultAsync(x =>
                    x.AppUserId == userId &&
                    x.WorkoutExerciseId == dto.WorkoutExerciseId &&
                    x.CreatedDate.Date == today &&
                    x.Status != Status.Deleted);

            if (existingFeedback != null)
            {
                // aynı gün aynı harekete feedback verdiyse -> güncelle
                existingFeedback.FeedbackText = dto.FeedbackText?.Trim();
                existingFeedback.ActualReps = dto.ActualReps;
                existingFeedback.ActualWeight = dto.ActualWeight;
                existingFeedback.RPE = dto.RPE;
                existingFeedback.FeedbackDate = DateTime.UtcNow;

                _unitOfWork.Repository<WorkoutFeedback>().Update(existingFeedback);
            }
            else
            {
                // ilk defa bu gün bu hareket için feedback veriyor -> yeni kayıt
                var feedback = new WorkoutFeedback
                {
                    WorkoutExerciseId = dto.WorkoutExerciseId,
                    AppUserId = userId,
                    FeedbackText = dto.FeedbackText?.Trim(),
                    ActualReps = dto.ActualReps,
                    ActualWeight = dto.ActualWeight,
                    RPE = dto.RPE,
                    FeedbackDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow, // varsa BaseEntity'den gelir
                    Status = Status.Active
                };

                await _unitOfWork.Repository<WorkoutFeedback>().AddAsync(feedback);
            }

            await _unitOfWork.SaveChangesAsync();
            return Ok(new { success = true });
        }

    }
}
