using FraoulaPT.DTOs.ExerciseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IExerciseService : IBaseService<
    ExerciseListDTO,
    ExerciseDetailDTO,
    ExerciseCreateDTO,
    ExerciseUpdateDTO>
    { }

}
