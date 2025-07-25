namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.WorkoutProgramViewModels
{
    public class WorkoutExerciseVM
    {
        public Guid ExerciseId { get; set; }
        public string ExerciseName { get; set; } // UI'de görüntü için

        public int Sets { get; set; }
        public int Reps { get; set; }
        public decimal? WeightKg { get; set; }
        public int RestSeconds { get; set; }
        public string? Note { get; set; }
    }
}
