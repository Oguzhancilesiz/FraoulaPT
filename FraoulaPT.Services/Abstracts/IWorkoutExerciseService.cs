using FraoulaPT.DTOs.WorkoutExerciseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IWorkoutExerciseService : IBaseService<
    WorkoutExerciseListDTO,
    WorkoutExerciseDetailDTO,
    WorkoutExerciseCreateDTO,
    WorkoutExerciseUpdateDTO>
    { }

}
