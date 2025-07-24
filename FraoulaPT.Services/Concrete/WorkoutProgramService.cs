using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.WorkoutProgramDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class WorkoutProgramService : BaseService<
    WorkoutProgram,
    WorkoutProgramListDTO,
    WorkoutProgramDetailDTO,
    WorkoutProgramCreateDTO,
    WorkoutProgramUpdateDTO>, IWorkoutProgramService
    {
        public WorkoutProgramService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }

}
