using FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.WorkoutProgramViewModels;

namespace FraoulaPT.WebUI.Models.ViewModels.WorkoutProgramViewModel
{
    public class WorkoutDayVM
    {
        public Guid Id { get; set; }
        public int DayOfWeek { get; set; }
        public string Description { get; set; }
        public List<WorkoutExerciseVM> Exercises { get; set; } = new();
    }
}
