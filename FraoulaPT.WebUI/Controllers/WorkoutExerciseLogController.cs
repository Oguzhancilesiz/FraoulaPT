using FraoulaPT.DTOs.UserProgramDTO;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.WebUI.Extensions;
using FraoulaPT.WebUI.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FraoulaPT.WebUI.Controllers
{
    public class WorkoutExerciseLogController : BaseController
    {
        private readonly IWorkoutExerciseLogService _logService;

        public WorkoutExerciseLogController(IWorkoutExerciseLogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax(WorkoutExerciseLogCreateDTO dto)
        {
            dto.CompletedAt = DateTime.Now;
            dto.IsCompleted = true;
            dto.AppUserId = User.GetUserId(); // güvenlik için!

            await _logService.LogExerciseAsync(dto);

            ShowMessage("Başarıyla kaydedildi!", MessageType.Success);
            return Json(new { success = true });
        }
    }
}
