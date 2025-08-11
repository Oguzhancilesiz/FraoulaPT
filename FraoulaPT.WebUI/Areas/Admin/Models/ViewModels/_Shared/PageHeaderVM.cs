namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels._Shared
{
    public class PageHeaderVM
    {
        public string Title { get; set; } = "";
        public string? Subtitle { get; set; }
        public List<BreadcrumbItemVM> Breadcrumbs { get; set; } = new();
        public List<ToolbarButtonVM> Buttons { get; set; } = new();
    }
}
