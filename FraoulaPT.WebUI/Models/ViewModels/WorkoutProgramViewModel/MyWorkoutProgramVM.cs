namespace FraoulaPT.WebUI.Models.ViewModels.WorkoutProgramViewModel
{
    public class MyWorkoutProgramVM
    {
        public string ProgramTitle { get; set; }
        public string CoachNote { get; set; }
        public List<WorkoutDayVM> Days { get; set; }
    }
}
