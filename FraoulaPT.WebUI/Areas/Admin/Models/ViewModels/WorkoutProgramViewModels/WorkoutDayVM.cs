using FraoulaPT.Entity;

namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.WorkoutProgramViewModels
{

    public class WorkoutDayVM
    {
        public int DayOfWeek { get; set; } // 1 = Pazartesi vs.
        public DateTime? Date { get; set; } // İstersen günü tarihle de tutabilirsin.
        public List<WorkoutExerciseVM> Exercises { get; set; } = new();
    }
}
