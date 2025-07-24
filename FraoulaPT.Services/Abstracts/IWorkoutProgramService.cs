using FraoulaPT.DTOs.WorkoutProgramDTOs;
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
    { }

}
