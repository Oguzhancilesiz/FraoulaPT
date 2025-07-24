using FraoulaPT.DTOs.ExerciseCategoryDTOs;
using FraoulaPT.Entity;
using FraoulaPT.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IExerciseCategoryService : IBaseService<
      ExerciseCategoryListDTO,
      ExerciseCategoryDetailDTO,
      ExerciseCategoryCreateDTO,
      ExerciseCategoryUpdateDTO>
    { }
}
