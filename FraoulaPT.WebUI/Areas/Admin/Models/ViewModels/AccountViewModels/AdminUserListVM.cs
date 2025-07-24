namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.AccountViewModels
{
    public class AdminUserListVM
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Roles { get; set; } // Virgülle ayrılmış string (veya List<string>)
    }
}
