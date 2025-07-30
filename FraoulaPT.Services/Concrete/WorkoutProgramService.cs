using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using FraoulaPT.DTOs.AppUserDTOs;
using FraoulaPT.DTOs.ExerciseDTOs;
using FraoulaPT.DTOs.UserProfileDTOs;
using FraoulaPT.DTOs.WorkoutDayDTOs;
using FraoulaPT.DTOs.WorkoutExerciseDTOs;
using FraoulaPT.DTOs.WorkoutProgramDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using Mapster;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        public WorkoutProgramService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager) : base(unitOfWork) {
            _userManager = userManager;
        }
        public async Task<WorkoutProgramDetailDTO> GetWorkoutProgramDetailByIdAsync(Guid id)
        {
            var query = await _unitOfWork.Repository<WorkoutProgram>().GetBy(x => x.Id == id && x.Status != Status.Deleted);

            var entity = await query
                  .Include(x => x.AppUser)              // 👈 Kullanıcıyı dahil et
                    .ThenInclude(u => u.Profile)
                  .Include(x => x.Days)
                    .ThenInclude(d => d.Exercises)
                        .ThenInclude(e => e.Exercise)
                  .FirstOrDefaultAsync();
              
            if (entity == null)
                return null;
            var allExercises = await _unitOfWork.Repository<Exercise>().GetAll();
            var dto = new WorkoutProgramDetailDTO
            {
                Id = entity.Id,
                ProgramName = entity.ProgramTitle,
                CoachNote = entity.CoachNote,
                AssignedDate = entity.CreatedDate,
                UpdatedDate = entity.ModifiedDate,
                Days = entity.Days?.Select(day => new WorkoutDayDetailDTO
                {
                    Id = day.Id,
                    DayOfWeek = day.DayOfWeek,
                    Description = day.Description,
                    Exercises = day.Exercises?.Select(ex => new WorkoutExerciseDetailDTO
                    {
                        Id = ex.Id,
                        ExerciseName = ex.Exercise?.Name,
                        SetCount = ex.SetCount,
                        Repetition = ex.Repetition,
                        Weight = ex.Weight,
                        RestDurationInSeconds = ex.RestDurationInSeconds,
                        Note = ex.Note,
                        VideoUrl = ex.Exercise.VideoUrl,
                        ImageUrls = ex.Exercise.ImageUrl,
                        Status = ex.Status
                    }).ToList()
                }).ToList(),
                Exercises = allExercises.Select(e => new ExerciseListDTO
                {
                    Id = e.Id,
                    Name = e.Name
                }).ToList(),
                User = new AppUserDetailDTO
                {
                    Id = entity.AppUser.Id,
                    FullName = entity.AppUser.FullName,
                    UserName = entity.AppUser.UserName,
                    Email = entity.AppUser.Email,
                    PhoneNumber = entity.AppUser.PhoneNumber,
                    Profile = entity.AppUser.Profile == null ? null : new UserProfileDTO
                    {
                        ProfilePhotoUrl = entity.AppUser.Profile.ProfilePhotoUrl,
                        Instagram = entity.AppUser.Profile.Instagram,
                        Gender = entity.AppUser.Profile.Gender,
                        BirthDate = entity.AppUser.Profile.BirthDate,
                        HeightCm = entity.AppUser.Profile.HeightCm,
                        WeightKg = entity.AppUser.Profile.WeightKg,
                        BodyType = entity.AppUser.Profile.BodyType,
                        BloodType = entity.AppUser.Profile.BloodType,
                        Address = entity.AppUser.Profile.Address,
                        EmergencyContactName = entity.AppUser.Profile.EmergencyContactName,
                        EmergencyContactPhone = entity.AppUser.Profile.EmergencyContactPhone,
                        MedicalHistory = entity.AppUser.Profile.MedicalHistory,
                        ChronicDiseases = entity.AppUser.Profile.ChronicDiseases,
                        CurrentMedications = entity.AppUser.Profile.CurrentMedications,
                        Allergies = entity.AppUser.Profile.Allergies,
                        PastInjuries = entity.AppUser.Profile.PastInjuries,
                        CurrentPain = entity.AppUser.Profile.CurrentPain,
                        PregnancyStatus = entity.AppUser.Profile.PregnancyStatus,
                        LastCheckResults = entity.AppUser.Profile.LastCheckResults,
                        SmokingAlcohol = entity.AppUser.Profile.SmokingAlcohol,
                        Occupation = entity.AppUser.Profile.Occupation,
                        ExperienceLevel = entity.AppUser.Profile.ExperienceLevel,
                        FavoriteSports = entity.AppUser.Profile.FavoriteSports,
                        Notes = entity.AppUser.Profile.Notes,
                        DietType = entity.AppUser.Profile.DietType
                    }
                }
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
                        SetCount = ex.SetCount,
                        Repetition = ex.Repetition,
                        Weight = ex.Weight,
                        RestDurationInSeconds = ex.RestDurationInSeconds,
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
        public async Task<WorkoutProgram> GetLastWorkoutProgramByUserAsync(Guid userId)
        {
            var lastProgram = await _unitOfWork.Repository<WorkoutProgram>()
                .Query()
                .Include(x => x.Days)
                    .ThenInclude(d => d.Exercises)
                        .ThenInclude(e => e.Exercise)
                .Include(x => x.UserWeeklyForm)
                .Where(x => x.AppUserId == userId && x.Status != Status.Deleted)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            return lastProgram;
        }
    }
}
