using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.WorkoutDayDTOs;
using FraoulaPT.DTOs.WorkoutExerciseDTOs;
using FraoulaPT.DTOs.WorkoutProgramDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class WorkoutProgramService : BaseService<
        WorkoutProgram,
        WorkoutProgramListDTO,
        WorkoutProgramDetailDTO,
        WorkoutProgramCreateDTO,
        WorkoutProgramUpdateDTO>, IWorkoutProgramService
    {
        public WorkoutProgramService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        public async Task<WorkoutProgramDetailDTO> GetWorkoutProgramDetailByIdAsync(Guid id)
        {
            var query = await _unitOfWork.Repository<WorkoutProgram>().GetBy(x => x.Id == id && x.Status != Status.Deleted);

            var entity = await query
                .Include(x => x.Days)
                    .ThenInclude(d => d.Exercises)
                        .ThenInclude(e => e.Exercise)
                .FirstOrDefaultAsync();
              
            if (entity == null)
                return null;

            var dto = new WorkoutProgramDetailDTO
            {
                Id = entity.Id,
                ProgramName = entity.ProgramTitle,
                CoachNote = entity.CoachNote,
                Days = entity.Days?.Select(day => new WorkoutDayDetailDTO
                {
                    DayOfWeek = day.DayOfWeek,
                    Description = day.Description,
                    Exercises = day.Exercises?.Select(ex => new WorkoutExerciseDetailDTO
                    {
                        ExerciseName = ex.Exercise?.Name,
                        SetCount = ex.SetCount,
                        Repetition = ex.Repetition,
                        Weight = ex.Weight,
                        RestDurationInSeconds = ex.RestDurationInSeconds,
                        Note = ex.Note
                    }).ToList()
                }).ToList()
            };

            return dto;
        }
        public async Task<Guid> CreateAsync(WorkoutProgramCreateDTO dto, Guid coachId)
        {
            // 1. WorkoutProgram oluştur
            var program = new WorkoutProgram
            {
                ProgramTitle = dto.ProgramName,
                CoachNote = dto.CoachNote,
                UserWeeklyFormId = dto.UserWeeklyFormId,
                CreatedByUserId = coachId,
                UpdatedByUserId = coachId,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                Status = Status.Active,
                Days = new List<WorkoutDay>() // önlem olarak boş init edebilirsin ama gerekmez
            };

            await _unitOfWork.Repository<WorkoutProgram>().AddAsync(program);
            await _unitOfWork.SaveChangesAsync(); // ID almak için zorunlu

            // 2. Her WorkoutDay için
            foreach (var day in dto.Days)
            {
                var workoutDay = new WorkoutDay
                {
                    WorkoutProgramId = program.Id, // FK zorunlu!
                    DayOfWeek = day.DayOfWeek,
                    Description = "Acıklama",
                    CreatedByUserId = coachId,
                    UpdatedByUserId = coachId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    Status = Status.Active
                };

                await _unitOfWork.Repository<WorkoutDay>().AddAsync(workoutDay);
                await _unitOfWork.SaveChangesAsync(); // Save çağır ki Day ID gelsin

                // 3. Her WorkoutExercise için
                foreach (var ex in day.Exercises)
                {
                    var workoutExercise = new WorkoutExercise
                    {
                        WorkoutDayId = workoutDay.Id,
                        ExerciseId = ex.ExerciseId,
                        SetCount = ex.Sets,
                        Repetition = ex.Reps,
                        Weight = ex.WeightKg,
                        RestDurationInSeconds = ex.RestSeconds,
                        Note = ex.Note,
                        CreatedByUserId = coachId,
                        UpdatedByUserId = coachId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        Status = Status.Active
                    };

                    await _unitOfWork.Repository<WorkoutExercise>().AddAsync(workoutExercise);
                }
                await _unitOfWork.SaveChangesAsync(); // o günün tüm egzersizleri için
            }

            return program.Id;
        }
    }
}
