using FraoulaPT.Entity;

namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.WorkoutProgramViewModels
{
    public class WorkoutProgramCreateVM
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<WorkoutDayVM> Days { get; set; } = new();
    }
}
