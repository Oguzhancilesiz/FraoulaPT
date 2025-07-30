using FraoulaPT.DTOs.WorkoutProgramDTOs;
using FraoulaPT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IWorkoutProgramService : IBaseService<
    WorkoutProgramListDTO,
    WorkoutProgramDetailDTO,
    WorkoutProgramCreateDTO,
    WorkoutProgramUpdateDTO>
    {
        Task<Guid> CreateAsync(WorkoutProgramCreateDTO dto, Guid coachId);
        Task<WorkoutProgramDetailDTO> GetWorkoutProgramDetailByIdAsync(Guid id);
        Task<WorkoutProgram> GetLastWorkoutProgramByUserAsync(Guid id);

    }

}
