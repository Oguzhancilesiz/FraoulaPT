using FraoulaPT.DTOs.PackageDTOs;

namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels
{
    public class PackageIndexVM
    {
        public List<PackageListDTO> Packages { get; set; }
        public bool HasActivePackage { get; set; }
    }

}
