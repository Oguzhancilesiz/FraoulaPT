using FraoulaPT.DTOs.WorkoutExerciseLogDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IWorkoutExerciseLogService : IBaseService<
    WorkoutExerciseLogListDTO,
    WorkoutExerciseLogDetailDTO,
    WorkoutExerciseLogCreateDTO,
    WorkoutExerciseLogUpdateDTO>
    { }

}
