namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels
{
    public class WorkoutAssignmentListViewModel
    {
        public Guid AssignmentId { get; set; }
        public string UserFullName { get; set; }
        public string FormName { get; set; }
        public string ProgramName { get; set; }
        public string CoachNote { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string StatusText { get; set; }
    }
}
