using FraoulaPT.Entity;

namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.WorkoutProgramViewModels
{

    public class WorkoutDayVM
    {
        public int DayOfWeek { get; set; } // 0 = Pazartesi

        public List<WorkoutExerciseVM> Exercises { get; set; } = new();
    }
}
