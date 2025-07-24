using FraoulaPT.Core.Abstracts;
using FraoulaPT.DTOs.ExerciseCategoryDTOs;
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
    public class ExerciseCategoryService : BaseService<
     ExerciseCategory,
     ExerciseCategoryListDTO,
     ExerciseCategoryDetailDTO,
     ExerciseCategoryCreateDTO,
     ExerciseCategoryUpdateDTO>, IExerciseCategoryService
    {
        public ExerciseCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
