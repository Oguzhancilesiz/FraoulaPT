using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.WorkoutExerciseDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class WorkoutExerciseService : BaseService<
    WorkoutExercise,
    WorkoutExerciseListDTO,
    WorkoutExerciseDetailDTO,
    WorkoutExerciseCreateDTO,
    WorkoutExerciseUpdateDTO>, IWorkoutExerciseService
    {
        public WorkoutExerciseService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
