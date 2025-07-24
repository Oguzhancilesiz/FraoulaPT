namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.WorkoutProgramViewModels
{
    public class WorkoutExerciseVM
    {
        public Guid ExerciseId { get; set; }
        public string ExerciseName { get; set; } // Autocomplete/Dropdown için
        public int SetCount { get; set; }
        public int RepetitionCount { get; set; }
        public decimal WeightKg { get; set; }
        public string Note { get; set; }
    }
}
