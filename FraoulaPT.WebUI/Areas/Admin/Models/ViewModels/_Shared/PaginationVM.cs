namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels._Shared
{
    public class PaginationVM
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; } = 0;

        // Listeyi çizecek rota
        public string Action { get; set; } = "Index";
        public string Controller { get; set; } = "";

        // TagHelper için string,string? dictionary kullan
        public IDictionary<string, string?> RouteData { get; set; } = new Dictionary<string, string?>();

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / Math.Max(PageSize, 1));
    }
}
