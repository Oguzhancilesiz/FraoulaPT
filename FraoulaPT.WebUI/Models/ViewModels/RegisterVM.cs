using System.ComponentModel.DataAnnotations;

namespace FraoulaPT.WebUI.Models.ViewModels
{
    public class RegisterVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Şifre ile Şifre Tekrar Eşleşmiyor")]
        public string ConfrimPassword { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


    }
}
