namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.WorkoutProgramViewModels
{
    public class WorkoutProgramCreateWithDaysVM
    {
        public Guid UserWeeklyFormId { get; set; }
        public Guid AppUserId { get; set; }
        public string ProgramTitle { get; set; }
        public string CoachNote { get; set; }
        public List<int> Days { get; set; } = new();
    }
}
