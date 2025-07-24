using FraoulaPT.DTOs.ExerciseCategoryDTOs;
using FraoulaPT.DTOs.ExerciseDTOs;

namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.ExerciseViewModels
{
    public class ExerciseEditVM
    {
        public ExerciseUpdateDTO Exercise { get; set; } = new();
        public List<ExerciseCategoryListDTO> Categories { get; set; } = new();

        // yeni görsel
        public IFormFile? ImageFile { get; set; }

        // Eski görsel
        public string? ExistingImageUrl { get; set; }
    }
}
