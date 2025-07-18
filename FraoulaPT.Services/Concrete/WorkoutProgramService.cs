using FraoulaPT.DTOs.UserProgramDTO;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Core.Abstracts;
using Microsoft.EntityFrameworkCore;
using FraoulaPT.Core.Enums;

public class WorkoutProgramService : IWorkoutProgramService
{
    private readonly IUnitOfWork _unitOfWork;

    public WorkoutProgramService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Programı detaylı getir
    public async Task<WorkoutProgramDTO> GetByIdAsync(Guid id,Guid currentUserId)
    {
        var repo = _unitOfWork.Repository<WorkoutProgram>();
        var query = await repo.GetBy(p => p.Id == id);

        var entity = await query
                  .Include(p => p.Days)
                      .ThenInclude(d => d.Exercises)
                          .ThenInclude(e => e.Exercise)
                  .Include(p => p.Days)
                      .ThenInclude(d => d.Exercises)
                          .ThenInclude(e => e.Logs) // <-- burada logları da yüklüyoruz!
                  .FirstOrDefaultAsync();

        if (entity == null) return null;

        // Manuel DTO dönüşümü
        return new WorkoutProgramDTO
        {
            Id = entity.Id,
            UserWeeklyFormId = entity.UserWeeklyFormId,
            Title = entity.Title,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CoachNote = entity.CoachNote,
            Days = entity.Days?.Select(d => new WorkoutDayDTO
            {
                Id = d.Id,
                WorkoutProgramId = d.WorkoutProgramId,
                DayOfWeek = d.DayOfWeek,
                Exercises = d.Exercises?.Select(e => new WorkoutExerciseDTO
                {
                    Id = e.Id,
                    WorkoutDayId = e.WorkoutDayId,
                    ExerciseId = e.ExerciseId,
                    ExerciseName = e.Exercise?.Name,
                    SetCount = e.SetCount,
                    RepetitionCount = e.RepetitionCount,
                    Weight = e.Weight,
                    CoachNote = e.CoachNote,
                    ImageUrl = e.Exercise?.ImageUrl,
                    VideoUrl = e.Exercise?.VideoUrl,

                    LastLog = e.Logs?
                        .Where(log => log.AppUserId == currentUserId)
                        .OrderByDescending(log => log.CompletedAt)
                        .Select(log => new WorkoutExerciseLogDTO
                        {
                            Id = log.Id,
                            WorkoutExerciseId = log.WorkoutExerciseId,
                            AppUserId = log.AppUserId,
                            IsCompleted = log.IsCompleted,
                            ActualSetCount = log.ActualSetCount,
                            ActualRepetitionCount = log.ActualRepetitionCount,
                            ActualWeight = log.ActualWeight,
                            UserNote = log.UserNote,
                            CompletedAt = log.CompletedAt
                        }).FirstOrDefault()
                                }).ToList()
            }).ToList()
        };
    }

    // Kullanıcının tüm atanmış programları
    public async Task<List<WorkoutProgramDTO>> GetAllByUserIdAsync(Guid userId)
    {
        // 1. UserWorkoutAssignment üzerinden programları bul
        var assignmentRepo = _unitOfWork.Repository<UserWorkoutAssignment>();
        var assignmentsQuery = await assignmentRepo.GetBy(x => x.UserPackage.AppUserId == userId);
        var assignments = await assignmentsQuery.ToListAsync();

        if (!assignments.Any()) return new List<WorkoutProgramDTO>();

        var programIds = assignments.Select(a => a.WorkoutProgramId).Distinct().ToList();

        // 2. Programları yükle
        var programRepo = _unitOfWork.Repository<WorkoutProgram>();
        var programsQuery = await programRepo.GetBy(p => programIds.Contains(p.Id));
        var programs = await programsQuery
            .Include(p => p.Days)
                .ThenInclude(d => d.Exercises)
                .ThenInclude(e => e.Exercise)
            .ToListAsync();

        // 3. DTO map
        return programs.Select(entity => new WorkoutProgramDTO
        {
            Id = entity.Id,
            UserWeeklyFormId = entity.UserWeeklyFormId,
            Title = entity.Title,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CoachNote = entity.CoachNote,
            Days = entity.Days?.Select(d => new WorkoutDayDTO
            {
                Id = d.Id,
                WorkoutProgramId = d.WorkoutProgramId,
                DayOfWeek = d.DayOfWeek,
                Exercises = d.Exercises?.Select(e => new WorkoutExerciseDTO
                {
                    Id = e.Id,
                    WorkoutDayId = e.WorkoutDayId,
                    ExerciseId = e.ExerciseId,
                    ExerciseName = e.Exercise?.Name,
                    SetCount = e.SetCount,
                    RepetitionCount = e.RepetitionCount,
                    Weight = e.Weight,
                    CoachNote = e.CoachNote
                }).ToList()
            }).ToList()
        }).ToList();
    }

    // Program oluşturma
    public async Task<Guid> CreateAsync(WorkoutProgramDTO dto)
    {
        var repo = _unitOfWork.Repository<WorkoutProgram>();

        var program = new WorkoutProgram
        {
            Id = Guid.NewGuid(),
            UserWeeklyFormId = dto.UserWeeklyFormId,
            Title = dto.Title,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            CoachNote = dto.CoachNote,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            Status = Status.Active,
            Days = dto.Days?.Select(day => new WorkoutDay
            {
                Id = Guid.NewGuid(),
                DayOfWeek = day.DayOfWeek,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Status = Status.Active,
                Exercises = day.Exercises?.Select(ex => new WorkoutExercise
                {
                    Id = Guid.NewGuid(),
                    ExerciseId = ex.ExerciseId,
                    SetCount = ex.SetCount,
                    RepetitionCount = ex.RepetitionCount,
                    Weight = ex.Weight,
                    CoachNote = ex.CoachNote,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.Active
                }).ToList()
            }).ToList()
        };

        await repo.AddAsync(program);
        await _unitOfWork.SaveChangesAsync();

        return program.Id;
    }

    // Gelişmiş update ve silme için sadece iskelet bırakıldı, ister açalım ister geçelim
    public async Task<bool> UpdateAsync(WorkoutProgramDTO dto)
    {
        // Gelişmiş update işlemlerini burada ekle (isteğe göre açarız)
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var repo = _unitOfWork.Repository<WorkoutProgram>();
        var program = await repo.GetById(id);

        if (program == null) return false;

        await Task.Run(() => repo.Delete(program));
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
