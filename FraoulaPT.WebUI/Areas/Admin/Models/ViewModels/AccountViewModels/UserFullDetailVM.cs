using FraoulaPT.DTOs.AppUserDTOs;
using FraoulaPT.DTOs.UserProfileDTOs;

namespace FraoulaPT.WebUI.Areas.Admin.Models.ViewModels.AccountViewModels
{
    public class UserFullDetailVM
    {
        public AppUserDetailDTO User { get; set; }
        public UserProfileDetailDTO Profile { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
