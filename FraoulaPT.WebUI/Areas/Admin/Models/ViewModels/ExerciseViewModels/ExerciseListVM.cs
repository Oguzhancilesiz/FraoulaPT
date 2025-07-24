using FraoulaPT.DTOs.ExerciseCategoryDTOs;
using FraoulaPT.DTOs.ExerciseDTOs;

namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.ExerciseViewModels
{
    public class ExerciseListVM
    {
        public List<ExerciseListDTO> Exercises { get; set; } = new();
        public List<ExerciseCategoryListDTO> Categories { get; set; } = new();
    }

}
