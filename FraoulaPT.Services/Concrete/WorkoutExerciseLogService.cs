using FraoulaPT.DTOs.UserProgramDTO;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Core.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace FraoulaPT.Services.Concrete
{
    public class WorkoutExerciseLogService : IWorkoutExerciseLogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutExerciseLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> LogExerciseAsync(WorkoutExerciseLogCreateDTO dto)
        {
            var repo = _unitOfWork.Repository<WorkoutExerciseLog>();
            var entity = new WorkoutExerciseLog
            {
                Id = Guid.NewGuid(),
                WorkoutExerciseId = dto.WorkoutExerciseId,
                AppUserId = dto.AppUserId,
                IsCompleted = dto.IsCompleted,
                ActualSetCount = dto.ActualSetCount,
                ActualRepetitionCount = dto.ActualRepetitionCount,
                ActualWeight = dto.ActualWeight,
                UserNote = dto.UserNote,
                CompletedAt = dto.CompletedAt,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Status = Core.Enums.Status.Active
            };

            await repo.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<WorkoutExerciseLogDTO>> GetLogsByUserAndProgramAsync(Guid userId, Guid workoutProgramId)
        {
            var repo = _unitOfWork.Repository<WorkoutExerciseLog>();

            // .Where + navigation kullanımı için IQueryable alıyoruz
            var query = await repo.GetBy(x =>
                x.AppUserId == userId &&
                x.WorkoutExercise.WorkoutDay.WorkoutProgramId == workoutProgramId);

            var logs = await query.ToListAsync();

            return logs.Select(x => new WorkoutExerciseLogDTO
            {
                Id = x.Id,
                WorkoutExerciseId = x.WorkoutExerciseId,
                AppUserId = x.AppUserId,
                IsCompleted = x.IsCompleted,
                ActualSetCount = x.ActualSetCount,
                ActualRepetitionCount = x.ActualRepetitionCount,
                ActualWeight = x.ActualWeight,
                UserNote = x.UserNote,
                CompletedAt = x.CompletedAt
            }).ToList();
        }

        public async Task<bool> UpdateLogAsync(WorkoutExerciseLogUpdateDTO dto)
        {
            var repo = _unitOfWork.Repository<WorkoutExerciseLog>();
            var query = await repo.GetBy(x => x.Id == dto.Id);
            var entity = await query.FirstOrDefaultAsync();
            if (entity == null) return false;

            entity.IsCompleted = dto.IsCompleted;
            entity.ActualSetCount = dto.ActualSetCount;
            entity.ActualRepetitionCount = dto.ActualRepetitionCount;
            entity.ActualWeight = dto.ActualWeight;
            entity.UserNote = dto.UserNote;
            entity.CompletedAt = dto.CompletedAt;
            entity.ModifiedDate = DateTime.Now;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
