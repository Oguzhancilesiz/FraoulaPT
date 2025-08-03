using FraoulaPT.DTOs.AppUserDTOs;

namespace FraoulaPT.WebUI.Models.ViewModels.QuestionAnsverViewModel
{
    public class SupportBoxViewModel
    {
        public Guid CurrentUserId { get; set; }
        public Guid? UserPackageId { get; set; }
        public List<CoachListDTO> Coaches { get; set; }
    }
}
