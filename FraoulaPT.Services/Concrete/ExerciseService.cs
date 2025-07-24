using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.ExerciseDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Concrete
{
    public class ExerciseService : BaseService<
     Exercise,
     ExerciseListDTO,
     ExerciseDetailDTO,
     ExerciseCreateDTO,
     ExerciseUpdateDTO>, IExerciseService
    {
        public ExerciseService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
