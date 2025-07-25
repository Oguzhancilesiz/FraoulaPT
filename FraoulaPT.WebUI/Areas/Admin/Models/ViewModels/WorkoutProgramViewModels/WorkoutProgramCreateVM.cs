using FraoulaPT.Entity;

namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.WorkoutProgramViewModels
{
    public class WorkoutProgramCreateVM
    {
        public Guid UserWeeklyFormId { get; set; }

        public string ProgramName { get; set; }

        // Gün gün program
        public List<WorkoutDayVM> Days { get; set; } = new();
    }
}
