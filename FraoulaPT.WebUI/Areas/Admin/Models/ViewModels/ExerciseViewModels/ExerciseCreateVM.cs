using FraoulaPT.DTOs.ExerciseCategoryDTOs;
using FraoulaPT.DTOs.ExerciseDTOs;
using System.ComponentModel.DataAnnotations;

namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.ExerciseViewModels
{
    public class ExerciseCreateVM
    {
        public ExerciseCreateDTO Exercise { get; set; } = new();
        public List<ExerciseCategoryListDTO> Categories { get; set; } = new();
        //görsel yükleme icin gerekli
        [Required(ErrorMessage = "Egzersiz görseli zorunludur!")]
        public IFormFile? ImageFile { get; set; }
    }
}
