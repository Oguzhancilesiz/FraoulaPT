namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.WorkoutProgramViewModels
{
    public class WorkoutDayCreateVM
    {
        public Guid ProgramId { get; set; } // WorkoutProgramId
        public int DayOfWeek { get; set; }  // 1 = Pazartesi ... 7 = Pazar
        public string Description { get; set; }
    }

}
