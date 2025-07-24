namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.AccountViewModels
{
    public class AdminUserRoleAssignVM
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<string> AllRoles { get; set; }
        public List<string> AssignedRoles { get; set; }
    }
}
