using FraoulaPT.DTOs.UserProgramDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IUserWorkoutAssignmentService
    {
        Task<UserWorkoutAssignmentDTO> AssignProgramAsync(Guid userPackageId, Guid workoutProgramId, string coachNote);
        Task<List<UserWorkoutAssignmentDTO>> GetAssignmentsByUserIdAsync(Guid userId);
    }
}
