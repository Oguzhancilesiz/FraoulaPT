using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.WorkoutExerciseLogDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class WorkoutExerciseLogService : BaseService<
    WorkoutExerciseLog,
    WorkoutExerciseLogListDTO,
    WorkoutExerciseLogDetailDTO,
    WorkoutExerciseLogCreateDTO,
    WorkoutExerciseLogUpdateDTO>, IWorkoutExerciseLogService
    {
        public WorkoutExerciseLogService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
