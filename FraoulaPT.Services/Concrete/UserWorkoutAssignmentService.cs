using FraoulaPT.DTOs.UserProgramDTO;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using FraoulaPT.Core.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace FraoulaPT.Services.Concrete
{
    public class UserWorkoutAssignmentService : IUserWorkoutAssignmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserWorkoutAssignmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserWorkoutAssignmentDTO> AssignProgramAsync(Guid userPackageId, Guid workoutProgramId, string coachNote)
        {
            var repo = _unitOfWork.Repository<UserWorkoutAssignment>();

            var assignment = new UserWorkoutAssignment
            {
                Id = Guid.NewGuid(),
                UserPackageId = userPackageId,
                WorkoutProgramId = workoutProgramId,
                CoachNote = coachNote,
                AssignedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Status = Core.Enums.Status.Active
            };

            await repo.AddAsync(assignment);
            await _unitOfWork.SaveChangesAsync();

            // Manuel DTO dönüşümü
            return new UserWorkoutAssignmentDTO
            {
                Id = assignment.Id,
                UserPackageId = assignment.UserPackageId,
                WorkoutProgramId = assignment.WorkoutProgramId,
                CoachNote = assignment.CoachNote,
                AssignedDate = assignment.AssignedDate
            };
        }

        public async Task<List<UserWorkoutAssignmentDTO>> GetAssignmentsByUserIdAsync(Guid userId)
        {
            var repo = _unitOfWork.Repository<UserWorkoutAssignment>();

            var query = await repo.GetBy(x => x.UserPackage.AppUserId == userId);
            var assignments = await query
                .Include(x => x.WorkoutProgram)
                .ToListAsync();

            return assignments.Select(x => new UserWorkoutAssignmentDTO
            {
                Id = x.Id,
                UserPackageId = x.UserPackageId,
                WorkoutProgramId = x.WorkoutProgramId,
                AssignedDate = x.AssignedDate,
                CoachNote = x.CoachNote,
                // İstersen burada Program detayını da doldurabilirsin
            }).ToList();
        }
    }
}
