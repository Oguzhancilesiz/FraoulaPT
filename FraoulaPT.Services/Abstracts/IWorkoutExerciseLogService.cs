using FraoulaPT.DTOs.UserProgramDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IWorkoutExerciseLogService
    {
        Task<bool> LogExerciseAsync(WorkoutExerciseLogCreateDTO dto); // Kullanıcı bir hareketi kaydediyor
        Task<List<WorkoutExerciseLogDTO>> GetLogsByUserAndProgramAsync(Guid userId, Guid workoutProgramId);
        Task<bool> UpdateLogAsync(WorkoutExerciseLogUpdateDTO dto);
    }
}
